using Applier.Services;
using AppUdemy.Models;
using AppUdemy.Services;
using AppUdemy.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppUdemy.ViewModels
{
    public class CursosDetailViewModel : BaseViewModel
    {
        // Constructores
        public CursosDetailViewModel(CursoModel cursoSelected)
        {
            CursoSelected = cursoSelected;
        }

        public CursosDetailViewModel()
        {
            CursoSelected = new CursoModel();
        }
        //Comandos
        private Command _CancelCommand;
        public Command CancelCommand => _CancelCommand ?? (_CancelCommand = new Command(CancelAction));

        private Command _SaveCommand;
        public Command SaveCommand => _SaveCommand ?? (_SaveCommand = new Command(SaveAction));

        private Command _DeleteCommand;
        public Command DeleteCommand => _DeleteCommand ?? (_DeleteCommand = new Command(DeleteAction));

        private Command _OpenMapsCommand;
        public Command OpenMapsCommand => _OpenMapsCommand ?? (_OpenMapsCommand = new Command(OpenMapsAction));


        private Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));


        private Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));


        private Command _GetLocationCommand;
        public Command GetLocationCommand => _GetLocationCommand ?? (_GetLocationCommand = new Command(GetLocationAction));



        //Propiedades
        private CursoModel _CursoSelected;
        public CursoModel CursoSelected
        {
            get => _CursoSelected;
            set => SetProperty(ref _CursoSelected, value);
        }

        private string _Picture;
        public string Picture
        {
            get => _Picture;
            set => SetProperty(ref _Picture, value);
        }

        private double _Latitude;
        public double Latitude
        {
            get => _Latitude;
            set => SetProperty(ref _Latitude, value);
        }

        private double _Longitude;
        public double Longitude
        {
            get => _Longitude;
            set => SetProperty(ref _Longitude, value);
        }

        //Métodos
        private async void SaveAction()
        {

            if (CursoSelected.ID != 0)
            {
                await new ApiService().PutDataAsync("Curso", CursoSelected);

                //Refrescamos el listado
                new CursosListViewModel().RefreshProducts();

                //Cerrar la página
                CancelAction();
            }
            else 
            {
                await new ApiService().PostDataAsync("Curso", CursoSelected);

                //Refrescamos el listado
                new CursosListViewModel().RefreshProducts();

                //Cerrar la página
                CancelAction();
            }

        }

        private async void DeleteAction()
        {
            //Eliminar en SQLite
            await new ApiService().DeleteDataAsync("Curso", CursoSelected.ID);

            //regresar el listado
            new CursosListViewModel().RefreshProducts();

            //cerramos la página
            CancelAction();
        }

        private async void CancelAction()
        {
            //Regresa el listado
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void OpenMapsAction(object obj)
        {
            Application.Current.MainPage.Navigation.PushAsync(new MapsPage(this));
        }

        //Seleccionar foto

        private async void SelectPictureAction(object obj)
        {
            // Inicializa la cámara
            await CrossMedia.Current.Initialize();

            // Si el seleccionar fotografía no está disponible o no está soportada lanza un mensaje y termina este método
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Pick Photo", ":( No pick photo available.", "OK");
                return;
            }

            // Selecciona la fotografía del carrete y la regresa en el objeto file
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            // Si el objeto file es null termina este método
            if (file == null) return;

            // Asignamos la ruta de la fotografía en la propiedad de la imagen
            CursoModel Contenedor = new CursoModel();
            Contenedor = CursoSelected;
            Contenedor.FotoDelCursoBase64 = await new ImageService().ConvertImageFileToBase64(file.Path);  //file.Path;
            CursoSelected = null;
            CursoSelected = Contenedor;

            /*image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });*/
        }

        //Tomar foto
        private async void TakePictureAction(object obj)
        {
            // Inicializa la cámara
            await CrossMedia.Current.Initialize();

            // Si la cámara no está disponible o no está soportada lanza un mensaje y termina este método
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            // Toma la fotografía y la regresa en el objeto file
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "AppSQLite",
                Name = "cam_picture.jpg"
            });

            // Si el objeto file es null termina este método
            if (file == null) return;

            // Asignamos la ruta de la fotografía en la propiedad de la imagen
            CursoModel Contenedor = new CursoModel();
            Contenedor = CursoSelected;
            Contenedor.FotoDelCursoBase64 = Picture = await new ImageService().ConvertImageFileToBase64(file.Path); //file.Path;
            CursoSelected = null;
            CursoSelected = Contenedor;

            /*image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });*/
        }

        private async void GetLocationAction()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    // Duda: Por que no actualiza la vista cuando se bindea directamente la propiedad del objeto a la vista? pero si la actualiza cuando es una propiedad independiente. Hipotesis: no se realiza un correcto set property. Esto por lo visto a la hora de depurar el codigo.
                    // CursoSelected.Latitude = Latitude = location.Latitude;
                    // CursoSelected.Longitude = Longitude = location.Longitude;

                    // Alternativa funcional ya que no se activaba el set property al cambiar las propiedades del objeto cursoSelected y al no activarse no actualizaba a la vista
                    CursoModel Contenedor = new CursoModel();
                    Contenedor = CursoSelected;

                    Contenedor.Latitude = location.Latitude;
                    Contenedor.Longitude = location.Longitude;

                    CursoSelected = null;
                    CursoSelected = Contenedor;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}

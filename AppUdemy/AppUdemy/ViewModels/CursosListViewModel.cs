using AppUdemy.Models;
using AppUdemy.Services;
using AppUdemy.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppUdemy.ViewModels
{
    public class CursosListViewModel : BaseViewModel
    { 
        // Constructores
        public CursosListViewModel()
        {
            LoadProducts();
        }

        //Comandos
        private Command _NewCommand;
        public Command NewCommand => _NewCommand ?? (_NewCommand = new Command(NewAction));

        private Command _SelectedCommand;
        public Command SelectedCommand => _SelectedCommand ?? (_SelectedCommand = new Command(SetCursoAction));

        
        private Command _RefreshCommand;
        public Command RefreshCommand => _RefreshCommand ?? (_RefreshCommand = new Command(RefreshListAction));



        //Propiedades
        private List<CursoModel> _CursosList;
        public List<CursoModel> CursosList
        {
            get => _CursosList;
            set => SetProperty(ref _CursosList, value);
        }

        private CursoModel _CursoSelected;
        public CursoModel CursoSelected
        {
            get => _CursoSelected;
            set 
            {
                if (SetProperty(ref _CursoSelected, value))
                {
                    SetCursoAction();
                }
            } 
        }

        //Metodos
        private async void LoadProducts()
        {
            IsBusy = true;
            CursosList = null;
            ApiResponse response = await new ApiService().GetDataAsync("Curso");
            if (response == null || !response.IsSuccess)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Cursos", $"Error al cargar los cursos: {response.Message}", "Ok");
                return;
            }

            CursosList = JsonConvert.DeserializeObject<List<CursoModel>>(response.Result.ToString());
            IsBusy = false;
        }

        private void NewAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new CursosDetailPage());
        }

        public void RefreshProducts()
        {
            LoadProducts();
        }

        private void SetCursoAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new CursosDetailPage(CursoSelected));
        }
        private void RefreshListAction()
        {
            LoadProducts();
        }
    }
}

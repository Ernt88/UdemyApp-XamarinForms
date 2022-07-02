using AppUdemy.Models;
using AppUdemy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AppUdemy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapsPage : ContentPage
    {
        public MapsPage(CursosDetailViewModel cursosDetailViewModel)
        {
            InitializeComponent();

            //Constructor

            //Crear una mascota de ejemplo
            CursoModel curso = cursosDetailViewModel.CursoSelected;

            // Centrar el mapa con las coordenadas de la mascota
            MapControl.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(
                        curso.Latitude,
                        curso.Longitude
                    ), Distance.FromMiles(.5)
                )
            );

            //Agregar un pin al mapa con las coordenadas de la mascota
            MapControl.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = curso.Nombre,
                    Position = new Position
                    (
                        curso.Latitude,
                        curso.Longitude
                    )
                }
            );

            //Ponemos los datos de la mascota en el cuadro blanco (BoxView)
            NombreDelCursoLabel.Text = curso.Nombre;
            NombreDelProfesorLabel.Text = curso.NombreDelProfesor;
        }
    }
}
using AppUdemy.Models;
using AppUdemy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppUdemy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CursosDetailPage : ContentPage
    {
        public CursosDetailPage()
        {
            InitializeComponent();

            BindingContext = new CursosDetailViewModel();
        }

        public CursosDetailPage(CursoModel CursoSelected)
        {
            InitializeComponent();

            BindingContext = new CursosDetailViewModel(CursoSelected);
        }
    }
}
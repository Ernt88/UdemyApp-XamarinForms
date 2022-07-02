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
    public partial class CursosListPage : ContentPage
    {
        public CursosListPage()
        {
            InitializeComponent();

            BindingContext = new CursosListViewModel();
        }
    }
}
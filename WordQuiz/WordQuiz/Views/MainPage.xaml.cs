using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            if (!Xamarin.Essentials.Preferences.Get("registered", false))
            {
                Shell.Current.GoToAsync("//RegisterPage");
            }
            
        }

        private void NewGame_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//GamePage");
        }

        private void Continue_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//GamePage");
        }
    }
}
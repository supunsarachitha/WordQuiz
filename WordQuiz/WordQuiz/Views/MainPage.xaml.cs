using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordQuiz.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
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
            Navigation.PushAsync(new GemaPage());
        }

        private void Continue_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GemaPage());
        }


        protected override void OnAppearing()
        {
            updateScore();
            base.OnAppearing();
        }

        private async void updateScore()
        {
            string u = Xamarin.Essentials.Preferences.Get("UserName", "");
            int s = Xamarin.Essentials.Preferences.Get("Score", 0);


            await firebaseHelper.UpdateScore(s, u);
        }
    }
}
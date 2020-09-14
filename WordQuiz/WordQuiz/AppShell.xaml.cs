using System;
using System.Collections.Generic;
using WordQuiz.Helpers;
using WordQuiz.Interfaces;

using WordQuiz.Views;
using Xamarin.Forms;

namespace WordQuiz
{
    public partial class AppShell : Xamarin.Forms.Shell
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public AppShell()
        {
            InitializeComponent();


            
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            var closer = DependencyService.Get<ICloseApplication>();
                closer?.closeApplication();
        }

        private async void OnMenuItemRegisterClicked(object sender, EventArgs e)
        {
            var a = await DisplayAlert("","Are you sure?","Procceed", "Cancel");
            if (a)
            {

                await firebaseHelper.Deactivate(Xamarin.Essentials.Preferences.Get("UserName", ""));
                Xamarin.Essentials.Preferences.Set("registered", false);
                await Shell.Current.GoToAsync("//RegisterPage");
            }
            
            
        }

        private void AboutPg_Clicked(object sender, EventArgs e)
        {
            Dispatcher.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushModalAsync(new AboutPage());

            });
        }
    }
}

using System;
using System.Collections.Generic;
using WordQuiz.Interfaces;

using WordQuiz.Views;
using Xamarin.Forms;

namespace WordQuiz
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();


            
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            var closer = DependencyService.Get<ICloseApplication>();
                closer?.closeApplication();
        }

        private void OnMenuItemRegisterClicked(object sender, EventArgs e)
        {
            Xamarin.Essentials.Preferences.Set("registered", false);
                Shell.Current.GoToAsync("//RegisterPage");
            
        }
    }
}

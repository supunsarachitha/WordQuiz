using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WordQuiz.Views;
using MediaManager;

namespace WordQuiz
{



    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Xamarin.Forms.DataGrid.DataGridComponent.Init();




            CrossMediaManager.Current.Init();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            CrossMediaManager.Current.Stop();
        }

        protected override void OnResume()
        {
        }
    }
}

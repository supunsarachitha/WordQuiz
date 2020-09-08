using MediaManager;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordQuiz.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {


        public List<string> randomAnimation =new List<string>();
        Random rnd = new Random();




        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public MainPage()
        {
            InitializeComponent();
            CrossMediaManager.Current.Notification.Enabled = false;
            try
            {
                

            if (!Xamarin.Essentials.Preferences.Get("registered", false))
            {
                Shell.Current.GoToAsync("//RegisterPage");
            }

            
                randomAnimation.Add("18045-teamwork-is-all-we-need.json");
                randomAnimation.Add("12038-people-illustration-for-humanmanager.json");
                randomAnimation.Add("31548-robot-says-hello.json");
                randomAnimation.Add("31669-rubiks-cube.json");
            
            

            callanim();

            DateTime time = DateTime.Now;
            if (time.Hour < 12)
                Title= "Good morning "+ " "+Xamarin.Essentials.Preferences.Get("UserName", "");
            else if (time.Hour < 18)
                Title = "Good afternoon " + " " + Xamarin.Essentials.Preferences.Get("UserName", "");
            else
                Title = "Good Evening " + " " + Xamarin.Essentials.Preferences.Get("UserName", "");

            }
            catch (Exception)
            {

                return;
            }


        }

        private void callanim() 
        {
            try
            {
                if (randomAnimation.Count > 0)
                {
                    AnimationView.Animation = randomAnimation[rnd.Next(randomAnimation.Count)];
                }
            }
            catch (Exception)
            {

                return;
            }

            
        }

        bool clicked = false;
        private async void NewGame_Clicked(object sender, EventArgs e)
        {
            CrossMediaManager.Current.Stop();
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Navigation.PushPopupAsync(new Popup("12955-no-internet-connection-empty-state.json"));
                return;
            }

            try
            {
                if (!clicked)
                {
                    Indicator.IsVisible = true;
                    clicked = true;
                    Dispatcher.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PushModalAsync(new GemaPage());
                    });
                    

                }
            }
            catch (Exception)
            {

                return;
            }
            //await Navigation.PushPopupAsync(new Popup("31570-mouse-error.json"));
        }


        
        private async void Continue_Clicked(object sender, EventArgs e)
        {
            
            
        }


        protected async override void OnAppearing()
        {
            if (!CrossMediaManager.Current.IsPlaying())
            {
                await CrossMediaManager.Current.PlayFromAssembly("perception.amr", null);
            }
            

            clicked = false;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Navigation.PushPopupAsync(new Popup("12955-no-internet-connection-empty-state.json"));
                return;
            }
            updateScore();

            callanim();
            Indicator.IsVisible = false;
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
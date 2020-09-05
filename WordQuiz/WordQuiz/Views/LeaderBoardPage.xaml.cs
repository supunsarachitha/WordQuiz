using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordQuiz.Helpers;
using WordQuiz.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeaderBoardPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public LeaderBoardPage()
        {
            InitializeComponent();

        }

        private async void GetUsers()
        {
            Indicator.IsVisible = true;

                var AllUsers = await firebaseHelper.GetAllPersons();
                List<Users> res = AllUsers.OrderByDescending(X => X.Score).ToList();
            int R = 0;
            foreach (var item in res)
            {
                item.Rank = R + 1;
                R++;

            }
            



            CollectionLeaderBoard.ItemsSource = res;

            Indicator.IsVisible = false;

        }

        protected override void OnAppearing()
        {
            GetUsers();
            base.OnAppearing();
        }
    }
}
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
    public partial class GameLevelPopup : PopupPage
    {

        string SelectedType = "";
        bool dictionary = false;
        public GameLevelPopup()
        {
            InitializeComponent();
            SelectedType = "charades_easy";
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopPopupAsync();
            return base.OnBackButtonPressed();
        }


        bool check=false;
        private void RdWords_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

            SelectedType = "";
            if (RdWords.IsChecked)
            {
                levelSelector.IsVisible = true;
                SelectedType = "charades_easy";
            }
            else
            {
                levelSelector.IsVisible = false;
            }

            //if (RdIdioms.IsChecked)
            //{
            //    SelectedType = "idioms_popular";
            //}

            if (RdAnimals.IsChecked)
            {
                SelectedType = "animal_names";
                
            }

            //if (RdTV.IsChecked)
            //{
            //    SelectedType = "tv_show_names_popular";
            //}

            //if (RdBooks.IsChecked)
            //{
            //    SelectedType = "book_names_popular";
            //}
            //if (RdMovies.IsChecked)
            //{
            //    SelectedType = "movie_names_popular";
            //}

            //if (RdCharacter.IsChecked)
            //{
            //    SelectedType = "people_character_names_popular";
            //}

            //=============
            if (RdWords.IsChecked)
            {
                
                if (rdEasy.IsChecked)
                {
                    SelectedType = "charades_easy";
                }
                if (rdModerate.IsChecked)
                {
                    SelectedType = "rdModerate";
                }
                if (rdHard.IsChecked)
                {
                    SelectedType = "charades_hard";
                }
                if (rdVeryHard.IsChecked)
                {
                    SelectedType = "charades_very_hard";
                }
            }
           



        }


        bool clicked = false;
        private async void btnGo_Clicked(object sender, EventArgs e)
        {

                if (!clicked)
                {
                    clicked = true;
                    
                        dictionary = true;
                        await Navigation.PushAsync(new GemaPage(SelectedType, dictionary));
                        await Navigation.PopPopupAsync();
                    


                }

        }
    }
}
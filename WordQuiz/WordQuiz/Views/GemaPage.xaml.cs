using MediaManager;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WordQuiz.API;
using WordQuiz.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GemaPage : ContentPage
    {

        public List<string> WordList;
        int wordCount = 0;
        string Orginalword = "";
        int RandomPosition = 0;
        DictionaryAPI dictionaryAPI = new DictionaryAPI();


        public GemaPage()
        {
            InitializeComponent();

            Indicator.IsVisible = true;
            board.IsVisible = false;

            lblScore.Text = "0";
            _ = GetWordList();

            int score = Xamarin.Essentials.Preferences.Get("Score", 0);
            lblScore.Text = Convert.ToString((score));

        }

        protected override void OnDisappearing()
        {
            Indicator.IsVisible = false;
            board.IsVisible = true;
            txtInput.Unfocus();
            base.OnDisappearing();
        }

        private async void ShowNextWord()
        {
            try
            {
                Indicator.IsVisible = true;
                board.IsVisible = false;
                if (WordList.Count > 0)
                {
                    Orginalword = "";
                    Orginalword = WordList.ElementAt(wordCount);
                    wordCount++;
                    if (wordCount > 48)
                    {
                        await GetWordList();
                        return;
                    }

                    Random R = new Random();
                    RandomPosition = 0;
                    RandomPosition = R.Next(Orginalword.Length);

                    char[] ch = Orginalword.ToCharArray(); //convert to character array
                    ch[RandomPosition] = '_'; //replace random charactor
                    
                    
                    await getDictionaryAPI(Orginalword);
                    txtWord.Text = new string(ch);
                }

            }
            catch (Exception ex)
            {

                return;
            }

        }

        private async Task  getDictionaryAPI( string word)
        {
            try
            {
                lblDefinition.Text = "";

                var meaning = await dictionaryAPI.getDictionaryMeaning(word);


                if (meaning != null)
                {
                    var def = meaning.Meanings.ElementAt(0).Definitions;
                    if (!string.IsNullOrEmpty(def.ElementAt(0).DefinitionDefinition))
                    {
                        lblDefinition.Text = def.ElementAt(0).DefinitionDefinition;
                    }
                    else if(!string.IsNullOrEmpty(def.ElementAt(1).DefinitionDefinition))
                    {
                        lblDefinition.Text = def.ElementAt(1).DefinitionDefinition;
                    }
                    else
                    {
                        lblDefinition.Text = "";
                        
                    }

                    if (string.IsNullOrEmpty(lblDefinition.Text))
                    {
                        ShowNextWord();
                    }

                    try
                    {
                        var a = meaning.Phonetics.ElementAt(0).Audio.AbsoluteUri;
                        if (a != null)
                        {

                            await CrossMediaManager.Current.Play(a.ToString());
                        }
                    }
                    catch (Exception)
                    {
                        Indicator.IsVisible = false;
                        board.IsVisible = true;
                        return;
                    }


                }
                else
                {
                    ShowNextWord();
                }

                Indicator.IsVisible = false;
                board.IsVisible = true;
            }
            catch (Exception ex)
            {
                ShowNextWord();
                
                
                return;
            }
        }

        private async Task GetWordList()
        {

            Dispatcher.BeginInvokeOnMainThread(async() =>
            {
                if (WordList != null)
                {
                    if (WordList.Count > 0)
                    {
                        WordList.Clear();
                    }
                }
                
                WebClient client = new WebClient();
                string RandomWords = client.DownloadString("https://www.wordgenerator.net/application/p.php?id=" + "charades_easy" + "&type=1&spaceflag=false");

                WordList = RandomWords.Split(',').ToList();

                wordCount = 0;
                ShowNextWord();
            });
            
        }

        private async void btnNext_Clicked(object sender, EventArgs e)
        {

            //await Shell.Current.GoToAsync("//MainPage");
            txtInput.Text = "";
            ShowNextWord();
            txtInput.Focus();

        }

        private async void check()
        {
            if (!string.IsNullOrEmpty(txtWord.Text))
            {
                if (Orginalword.ToLower() == txtWord.Text.ToLower())
                {
                    int score = Convert.ToInt32(lblScore.Text);
                    Xamarin.Essentials.Preferences.Set("Score", (Convert.ToInt32(lblScore.Text) + 10));
                    lblScore.Text = Convert.ToString((score + 10));

                    await Navigation.PushPopupAsync(new Popup("31640-success-mouse.json"));

                   
                }
                else
                {


                    await Navigation.PushPopupAsync(new Popup("31570-mouse-error.json"));
                    char[] ch = Orginalword.ToCharArray(); //convert to character array
                    ch[RandomPosition] = '_'; //replace random charactor
                    txtWord.Text = new string(ch);
                    txtInput.Text = "";
                    txtInput.Focus();
                }
            }
            else
            {
                txtInput.Text = "";
                ShowNextWord();
            }
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtInput.Text))
                {
                    txtWord.Text = txtWord.Text.Replace("_", txtInput.Text);
                    txtInput.Unfocus();
                    check();

                }
                else
                {
                    char[] ch = Orginalword.ToCharArray(); //convert to character array
                    ch[RandomPosition] = '_'; //replace random charactor
                    txtWord.Text = new string(ch);
                }
            }
            catch (Exception)
            {

                return;
            }

            


        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
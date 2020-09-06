using MediaManager;
using Newtonsoft.Json;
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
            lblScore.Text = "0";
            _ = GetWordList();

            int score = Xamarin.Essentials.Preferences.Get("Score", 0);
            lblScore.Text = Convert.ToString((score));

        }

        private async void ShowNextWord()
        {
            try
            {
               
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
                    txtWord.Text = new string(ch);


                    getDictionaryAPI(Orginalword);
                }

            }
            catch (Exception ex)
            {

                return;
            }

        }

        private async void getDictionaryAPI( string word)
        {
            try
            {
                Dictionary meaning = await dictionaryAPI.getDictionaryMeaning("clap");

                if (meaning != null)
                {
                    var def = meaning.meaning.noun.ElementAt(0);
                    if (!string.IsNullOrEmpty(def.definition))
                    {
                        lblDefinition.Text = def.definition;
                    }



                    try
                    {
                        var a = meaning.phonetics.ElementAt(0);
                        if (a != null)
                        {

                            await CrossMediaManager.Current.Play(a.audio);
                        }
                    }
                    catch (Exception)
                    {

                        return;
                    }


                }
            }
            catch (Exception)
            {

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

        private void btnNext_Clicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtWord.Text))
            {
                if (Orginalword.ToLower() == txtWord.Text.ToLower())
                {
                    int score = Convert.ToInt32(lblScore.Text);
                    Xamarin.Essentials.Preferences.Set("Score", (Convert.ToInt32(lblScore.Text)+ 10));
                    lblScore.Text = Convert.ToString((score + 10));
                    txtInput.Text = "";
                    ShowNextWord();
                }
                else
                {


                    DisplayAlert("", "Try again", "Ok");
                    char[] ch = Orginalword.ToCharArray(); //convert to character array
                    ch[RandomPosition] = '_'; //replace random charactor
                    txtWord.Text = new string(ch);
                    txtInput.Text = "";

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
            if (!string.IsNullOrEmpty(txtInput.Text))
            {
                txtWord.Text = txtWord.Text.Replace("_", txtInput.Text);
            }
            else
            {
                char[] ch = Orginalword.ToCharArray(); //convert to character array
                ch[RandomPosition] = '_'; //replace random charactor
                txtWord.Text = new string(ch);
            }
            
        }


    }
}
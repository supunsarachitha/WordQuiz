using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WordQuiz.Helpers;
using WordQuiz.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();
        List<Users> AllUsers;
        public RegisterPage()
        {
            InitializeComponent();


            

        }

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool valid = await VaidateRegister();

                if (valid)
                {
                    bool res2 = await RegisterNewUser();
                    if (res2)
                    {

                        Xamarin.Essentials.Preferences.Set("registered", true);
                        Xamarin.Essentials.Preferences.Set("UserName", txtUserName.Text);
                        Xamarin.Essentials.Preferences.Set("Score", 0);
                        await Shell.Current.GoToAsync("//MainPage");
                    }
                }
            }
            catch (Exception ex)
            {

                return;
            }
        }

        private async Task<bool> VaidateRegister()
        {
            if (pickCountry.SelectedItem != null)
            {
                if (pickCountry.SelectedIndex > 0)
                {
                    if (!string.IsNullOrEmpty(txtUserName.Text))
                    {
                        //CHECK USERNAME EXIST IN DB
                        bool res = await CheckExist();
                        if (res)
                        {
                            await DisplayAlert("", "User name already exist", "Ok");
                            return false;
                        }

                        return true;
                    }
                    else
                    {
                        await DisplayAlert("", "Enter user name", "Ok");
                        return false;
                    }

                }
                else
                {
                    await DisplayAlert("", "Select your country", "Ok");
                    return false;
                }

            }
            else
            {
                await DisplayAlert("", "Select your country", "Ok");
                return false;
            }

            return false;
        }

        private async Task<bool> RegisterNewUser()
        {
            try
            {
                int Id = 0;
                Id = AllUsers.Count == 0 ? 0 : AllUsers.Max(X => X.UserId);
                var Country = (Country)pickCountry.SelectedItem;
                bool res = await firebaseHelper.AddNewUser(Id + 1, txtUserName.Text, Country.country, Country.abbreviation, 0);
                if (res)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
            

        }

        private async Task<bool> CheckExist()
        {
            try
            {
                AllUsers = await firebaseHelper.GetAllPersons();
                List<Users> res = AllUsers.Where(x => x.UserName.ToLower() == txtUserName.Text.ToLower() && x.Active==true).ToList();

                if (res.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex )
            {

                return false;
            }

        }


        protected override void OnAppearing()
        {

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(RegisterPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("WordQuiz.Models.Countries.json");

            using (var reader = new System.IO.StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                List<Country> ro = JsonConvert.DeserializeObject<List<Country>>(json);
                pickCountry.ItemsSource = ro;

            }
            txtUserName.Text = "";
            pickCountry.SelectedIndex = -1;
            base.OnAppearing();
        }
    }
}
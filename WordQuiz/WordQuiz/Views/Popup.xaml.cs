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
    public partial class Popup : PopupPage
    {
        public Popup(string animation)
        {
            InitializeComponent();

            AnimationView.Animation = animation;
            
        }

        private async void Handle_OnFinish(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopPopupAsync();
            return base.OnBackButtonPressed();
        }
    }
}
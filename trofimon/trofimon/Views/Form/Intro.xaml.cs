using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trofimon.Views.Form
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Intro : CarouselPage
    {
        public Intro()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //https://forums.xamarin.com/discussion/101146/button-that-navigates-to-another-page-can-be-clicked-more-than-once
            await Navigation.PushAsync(new FormTab()); 
        }
    }
}
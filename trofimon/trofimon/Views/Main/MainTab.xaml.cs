using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using trofimon.ViewModel;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTab : Xamarin.Forms.TabbedPage
    {

        // MainTab mainTab;string email

        WelcomePageVM welcomePageVM;

        public MainTab(string email)
        {
            InitializeComponent();

            BarTextColor = Color.FromHex("#737373");
            SelectedTabColor = Color.FromHex("#24BE35");
            SelectedItem = Color.FromHex("#24BE35");

            //disabilita o swipe
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);

            welcomePageVM = new WelcomePageVM(email);
            BindingContext = welcomePageVM;
        }
    }

    /*private void CurrentPageHasChanged(object sender, EventArgs e)
    {
        var tabbedPage = (TabbedPage)sender;
        Title = tabbedPage.CurrentPage.Title;
    }*/
}
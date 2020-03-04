using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using trofimon.ViewModel;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTab : TabbedPage
    {

        // MainTab mainTab;string email

        WelcomePageVM welcomePageVM;
        public MainTab(string email)
        {
            InitializeComponent();
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
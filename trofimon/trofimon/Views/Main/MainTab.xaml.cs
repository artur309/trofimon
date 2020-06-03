using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mime;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using trofimon.ViewModel;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTab : Xamarin.Forms.TabbedPage
    {
        WelcomePageVM welcomePageVM;

        public MainTab(string email)
        {
            InitializeComponent();

            BarTextColor = Color.FromHex("#737373");
            SelectedTabColor = Color.FromHex("#24BE35");
            SelectedItem = Color.FromHex("#24BE35");

            //disabilita o swipe
            On<Android>().SetIsSwipePagingEnabled(false);

            //adicionar os icones no UWP
            On<Windows>().SetHeaderIconsSize(new Size(20, 20));
            On<Windows>().SetHeaderIconsEnabled(true);

            welcomePageVM = new WelcomePageVM(email);
            BindingContext = welcomePageVM;
        }
    }
}
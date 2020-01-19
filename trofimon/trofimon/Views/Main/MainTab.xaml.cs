using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTab : TabbedPage
    {
        public MainTab()
        {/*
          var navigationPage = new NavigationPage(new Feed());
            navigationPage.IconImageSource = "chef.png";
            navigationPage.Title = "Schedule";

            Children.Add(new Feed());
            Children.Add(navigationPage);
            Children.Add(new Feed());*/

            //CurrentPageChanged += CurrentPageHasChanged;
            InitializeComponent();
         } 

        private void CurrentPageHasChanged(object sender, EventArgs e)
        {
            var tabbedPage = (TabbedPage)sender;
            Title = tabbedPage.CurrentPage.Title;
        }

    }
}
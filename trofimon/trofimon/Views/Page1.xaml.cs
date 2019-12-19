using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trofimon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        Page1 page1;
        public Page1(string email)
        {
            InitializeComponent();
            page1 = new Page1(email);
            BindingContext = page1;
        }
    }
}
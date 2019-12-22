using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using trofimon.ViewModel;

namespace trofimon.Views.Form
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        //SignUp signUp;
        public SignUp()
        {
            InitializeComponent();
            //signUp = new SignUp();
            //set binding
            //BindingContext = signUp;
        }
    }
}
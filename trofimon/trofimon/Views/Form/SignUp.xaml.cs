using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trofimon.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trofimon.Views.Form
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        SignUpViewModel SignUpViewModel;
        public SignUp()
        {
            InitializeComponent();
            SignUpViewModel = new SignUpViewModel();
            //set binding
            BindingContext = SignUpViewModel;
        }
    }
}
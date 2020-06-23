using trofimon.ViewModel;
using trofimon.Views.Form;
using trofimon.Views.Main;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace trofimon
{
    public partial class App : Application
    {
        LoginViewModel loginViewModel = new LoginViewModel();
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage();
            //MainPage = new NavigationPage(new Views.Main.MainTab("41"));
            if (loginViewModel.lembrarSessao == true)
            {
                //verifficar se tem sessao variabel
                if (Current.Properties.ContainsKey("Email"))
                {
                    //App.Current.MainPage.Navigation.PopAsync();
                    //string userId = ;
                    //Current.MainPage.Navigation.PopAsync();
                    MainPage = new MainTab(Preferences.Get(loginViewModel.Email, loginViewModel.Email));
                }
                //MainPage = new NavigationPage(new FormTab());
            }
            else
                MainPage = new NavigationPage(new Intro());

        }

        public interface ICloseApplication
        {
            void closeApplication();
        }

        // Handle when your app starts
        protected async override void OnStart()
        {

           /* if (loginViewModel.lembrarSessao == true)
            {
                //verifficar se tem sessao variabel
                if (Current.Properties.ContainsKey("Email"))
                {
                    //App.Current.MainPage.Navigation.PopAsync();
                    //string userId = ;
                    App.Current.MainPage.Navigation.PopAsync();
                    Current.MainPage.Navigation.PushModalAsync(new MainTab(Preferences.Get(loginViewModel.Email, loginViewModel.Email)));
                }
                // Current.MainPage.DisplayAlert("Ligado", "", "Ok");
            }
            else
            {
                //MainPage = new NavigationPage(new FormTab());
                //Current.MainPage.DisplayAlert("Desligado", "", "Ok");
            }

            /*
             if (Connectivity.NetworkAccess == NetworkAccess.Internet){}
            else
            {
                //App.Current.MainPage.Navigation.PopAsync();Application.Current.Quit();
                Current.MainPage.DisplayAlert("Sem acesso Internet", "", "Fechar");

                /*bool trueChecker = true;
                if (trueChecker)
                {
                    Process.GetCurrentProcess().CloseMainWindow();
                    Process.GetCurrentProcess().Close();
                }

                var closer = DependencyService.Get<ICloseApplication>();
                if (closer != null)
                {
                    closer.closeApplication();
                }
            }*/
        }

        // Handle when your app sleeps
        protected override void OnSleep()
        {
        }

        // Handle when your app resumes
        protected override void OnResume()
        {
            //Current.MainPage.DisplayAlert("PUNK where did u go????", "", "Ok");
        }
    }
}

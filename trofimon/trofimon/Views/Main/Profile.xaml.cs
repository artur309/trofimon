using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.ObjectModel;
using trofimon.Models;
using trofimon.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public static readonly FirebaseClient firebase = new FirebaseClient("https://trofimon-pap.firebaseio.com/");
        public ObservableCollection<string> ReceitaStringList { get; set; } = new ObservableCollection<string>();
        private LoginViewModel loginViewModel = new LoginViewModel();
        private int NumReceitas { get; }

        public Profile()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            int countReceitas = 0;
            base.OnAppearing();

            var receitas = await firebase
                     .Child("Receitas")
                     .OrderByKey()
                     .OnceAsync<Receitas>();

            foreach (var receita in receitas)
            {
                if (receita.Object.UserReceita == Preferences.Get(loginViewModel.Email, loginViewModel.Email))
                {
                    ReceitaStringList.Add(receita.Object.NomeReceita);
                    countReceitas++;
                }
                labelNameProfile.Text = receita.Object.UserReceita;
            }

            listaViewReceitas.ItemsSource = null;
            listaViewReceitas.ItemsSource = ReceitaStringList;

            receitasLabel.Text = ""+countReceitas;
            BindingContext = this;
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            ReceitaStringList.Clear();
            listaViewReceitas.ItemsSource = ReceitaStringList;
        }

        public void LogoutBtn(object sender, EventArgs e)
        {
            LoginViewModel loginViewModel = new LoginViewModel
            {
                lembrarSessao = false
            };
            Application.Current.SavePropertiesAsync();
            App.Current.MainPage.Navigation.PopModalAsync();
        }

        private async void listaViewReceitas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ReceitaDetailedView(e.Item.ToString()));
        }
    }
}
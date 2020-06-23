using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.ObjectModel;
using trofimon.Models;
using trofimon.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public static readonly FirebaseClient firebase = new FirebaseClient("https://trofimon-pap.firebaseio.com/");
        private ObservableCollection<string> ReceitaStringList { get; set; } = new ObservableCollection<string>();
        private ObservableCollection<string> ReceitaImgStringList { get; set; } = new ObservableCollection<string>();
        private LoginViewModel loginViewModel = new LoginViewModel();

        public Home()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var receitas = await firebase
                     .Child("Receitas")
                     .OrderByKey()
                     .OnceAsync<Receitas>();

            foreach (var receita in receitas)
            {
                if (receita.Object.Privacidade == false)
                {
                    ReceitaStringList.Add(receita.Object.NomeReceita);
                    ReceitaImgStringList.Add(receita.Object.Imagem);
                }
            }

            listaViewReceitas.ItemsSource = null;
            listaViewReceitas.ItemsSource = ReceitaStringList;

            //imgReceita.Source = "https://via.placeholder.com/300";
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            ReceitaStringList.Clear();
            listaViewReceitas.ItemsSource = ReceitaStringList;
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new searchView());
        }

        private async void listaViewReceitas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ReceitaDetailedView(e.Item.ToString()));
        }
    }
}
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using trofimon.Models;
using trofimon.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class searchView : ContentPage
    {
        public static readonly FirebaseClient firebase = new FirebaseClient("https://trofimon-pap.firebaseio.com/");
        private ObservableCollection<string> ReceitaStringList { get; set; } = new ObservableCollection<string>();
        private LoginViewModel loginViewModel = new LoginViewModel();

        public searchView()
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
                    ReceitaStringList.Add(receita.Object.NomeReceita);
            }
            listaReceitas.ItemsSource = ReceitaStringList;
        }
        
        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            ReceitaStringList.Clear();
            listaReceitas.ItemsSource = ReceitaStringList;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var texto = searchBar.Text;
            listaReceitas.ItemsSource = ReceitaStringList.Where(x => x.ToLower().Contains(texto.ToLower()));

        }
    }
}

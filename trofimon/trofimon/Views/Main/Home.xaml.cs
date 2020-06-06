using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using trofimon.Models;
using trofimon.ViewModel;
using System.Collections.ObjectModel;
using Firebase.Database;
using trofimon.ViewModels.Main;
using Firebase.Database.Query;
using Xamarin.Essentials;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public static readonly FirebaseClient firebase = new FirebaseClient("https://trofimon-pap.firebaseio.com/");
        private ObservableCollection<string> ReceitaStringList { get; set; } = new ObservableCollection<string>();
        LoginViewModel loginViewModel = new LoginViewModel();

        public Home()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var receitas = await firebase
                     .Child("Receitas")
                     .Child(Preferences.Get(loginViewModel.Email, loginViewModel.Email))
                     .OrderByKey()
                     .OnceAsync<Receitas>();

            foreach (var receita in receitas)
                ReceitaStringList.Add(receita.Object.NomeReceita);

            listaViewReceitas.ItemsSource = null;
            listaViewReceitas.ItemsSource = ReceitaStringList;
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
    }
}
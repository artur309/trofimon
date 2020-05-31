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

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public static readonly FirebaseClient firebase = new FirebaseClient("https://trofimon-pap.firebaseio.com/"); 
        private ObservableCollection<string> ReceitaStringList { get; set; } = new ObservableCollection<string>(); 

        public Home()
        {
            InitializeComponent();

            Task.Run(async () =>
            {

                LoginViewModel loginViewModel = new LoginViewModel();
                ReceitaViewModel receitaViewModel = new ReceitaViewModel();

                var receitas = await firebase
                         .Child("Receitas")
                         .Child("12")
                         .OrderByKey()
                         .OnceAsync<Receitas>();

                foreach (var receita in receitas)
                {
                    ReceitaStringList.Add(receita.Object.NomeReceita);
                }
                //Console.WriteLine($"{receita.Key} is {receita.Object.NomeReceita}m high.");

                BindingContext = this;
 
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listaViewReceitas.ItemsSource = ReceitaStringList;
        }

        void ProcurarTexto(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            searchResults.ItemsSource = "OH";
        }

        /*
         *<SearchBar Placeholder="Procurar..." TextChanged="ProcurarTexto"
                             SearchCommand="{Binding PerformSearch}"
                    SearchCommandParameter="{Binding Text, Source={x:Reference searchBar}}"/>
                 <ListView  x:Name="searchResults" Margin="0,-100000" ItemsSource="{Binding SearchResults}" />
         * 
         * 
         * 
         * 
         * public class SearchViewModel : INotifyPropertyChanged
         {
             public event PropertyChangedEventHandler PropertyChanged;

             protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
             {
                 PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
             }

             public ICommand PerformSearch => new Command<string>((string query) =>
             {
                 SearchResults = FirebaseHelper.GetReceita("12");
             });

             private List<string> searchResults = Receitas.Fruits;
             public List<string> SearchResults
             {
                 get
                 {
                     return searchResults;
                 }
                 set
                 {
                     searchResults = value;
                     NotifyPropertyChanged();
                 }
             }
         } */
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using trofimon.Models;
using trofimon.ViewModel;
using trofimon.ViewModels.Main;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Database.Query;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public static readonly FirebaseClient firebase = new FirebaseClient("https://trofimon-pap.firebaseio.com/");
        private ObservableCollection<Receitas> ReceitaList { get; set; } = new ObservableCollection<Receitas>();
        //private List<Receitas> ReceitaList;

        public ICommand AddReceitas { get; set; }

        public Profile()
        {
            InitializeComponent();

            Task.Run(async () =>
            {

                LoginViewModel loginViewModel = new LoginViewModel();
                ReceitaViewModel receitaViewModel = new ReceitaViewModel();

                //var user = await FirebaseHelper.GetReceita(labelNameProfile.Text);
                ReceitaList = await FirebaseHelper.GetReceita(loginViewModel.Email);

                Console.WriteLine(ReceitaList);

                BindingContext = this;


                /*
                                var receitas = await firebase
                                        .Child("Receitas")
                                        .Child(Preferences.Get("12", "12"))
                                        .OrderByKey()
                                        .OnceAsync<Receitas>();

                                ObservableCollection<Receitas> receitasList = new ObservableCollection<Receitas>();
                                foreach (var receita in receitas)
                                {
                                    receitasList
                                        .Add(new Receitas
                                        {
                                            NomeReceita = Convert.ToString(receita.Object.NomeReceita),
                                        });
                                }*/
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            listaViewReceitas.ItemsSource = ReceitaList;
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
    }
}
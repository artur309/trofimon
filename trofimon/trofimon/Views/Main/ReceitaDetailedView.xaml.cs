using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trofimon.ViewModel;
using trofimon.ViewModels.Main;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Firebase.Database.Query;
using trofimon.Models;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceitaDetailedView : ContentPage
    {
        public static readonly FirebaseClient firebase = new FirebaseClient("https://trofimon-pap.firebaseio.com/");
        public ObservableCollection<string> ReceitaStringList { get; set; } = new ObservableCollection<string>();
        LoginViewModel loginViewModel = new LoginViewModel();
        Receitas receitaDetalhes = new Receitas();
        string userReceita;

        public ReceitaDetailedView(string receitaNome, string user)
        {
            InitializeComponent();
            Task.Run(async () =>
            {

            });

            receitaDetalhes.NomeReceita = receitaNome;
            userReceita = user;

            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            ReceitaViewModel receitaViewModel = new ReceitaViewModel();

            var idCount = await firebase
                     .Child("Receitas")
                     .Child(userReceita)
                     .OrderByKey()
                     .OnceAsync<Receitas>();

            foreach (var id in idCount)
            {
                if (id.Object.NomeReceita == receitaDetalhes.NomeReceita)
                {
                    receitaDetalhes.Ingredientes = id.Object.Ingredientes;
                    receitaDetalhes.Preparacao = id.Object.Preparacao;
                    break;
                }
            }

            //BindingContext = receitaDetalhes;

            //nome receita
            this.NomeReceita.Text = receitaDetalhes.NomeReceita;
            NomeReceita.BindingContext = receitaDetalhes.NomeReceita;

            //ingredientes receita
            this.Ingredientes.Text = receitaDetalhes.Ingredientes;
            Ingredientes.BindingContext = receitaDetalhes.Ingredientes;

            //preaparacao receita
            this.Preparacao.Text = receitaDetalhes.Preparacao;
            Preparacao.BindingContext = receitaDetalhes.Preparacao;
            //ReceitaNome.SetBinding(Label.text);
            BindingContext = this;
        }
    }
}
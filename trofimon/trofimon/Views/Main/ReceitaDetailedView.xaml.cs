using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using trofimon.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceitaDetailedView : ContentPage
    {
        private static readonly FirebaseClient firebase = new FirebaseClient("https://trofimon-pap.firebaseio.com/");
        private FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();
        public ObservableCollection<string> ReceitaStringList { get; set; } = new ObservableCollection<string>();
        private Receitas receitaDetalhes = new Receitas();

        public ReceitaDetailedView(string receitaNome)
        {
            InitializeComponent();
            receitaDetalhes.NomeReceita = receitaNome;
            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var idCount = await firebase
                     .Child("Receitas")
                     .OrderByKey()
                     .OnceAsync<Receitas>();

            foreach (var id in idCount)
            {
                if (id.Object.NomeReceita == receitaDetalhes.NomeReceita)
                {
                    receitaDetalhes.Ingredientes = id.Object.Ingredientes;
                    receitaDetalhes.Preparacao = id.Object.Preparacao;
                    receitaDetalhes.UserReceita = id.Object.UserReceita;
                    receitaDetalhes.Imagem = id.Object.Imagem;
                    break;
                }
            }

            string path = await firebaseStorageHelper.GetFile(receitaDetalhes.Imagem, receitaDetalhes.UserReceita);

            //this.Imagem.Text = receitaDetalhes.Imagem;
            //Imagem.BindingContext = path;
            imgReceita.Source = path;

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

            //preaparacao receita
            this.UserReceita.Text = receitaDetalhes.UserReceita;
            UserReceita.BindingContext = receitaDetalhes.UserReceita;

            BindingContext = this;
        }
    }
}
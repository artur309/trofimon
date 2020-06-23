using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Diagnostics;
using trofimon.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public static readonly FirebaseClient firebase = new FirebaseClient("https://trofimon-pap.firebaseio.com/");
        private FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();

        public Home()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Button buttonSearch = new Button
            {
                Text = "Procurar 🔎",
                BackgroundColor = Color.FromHex("#B5BE53"),
            };
            buttonSearch.Clicked += async (sender, args) => await Navigation.PushAsync(new searchView());

            var receitas = await firebase
                     .Child("Receitas")
                     .OrderByKey()
                     .OnceAsync<Receitas>();

            var section = new TableSection();
            foreach (var receita in receitas)
            {
                if (receita.Object.Privacidade == false)
                {
                    string path = await firebaseStorageHelper.GetFile(receita.Object.Imagem, receita.Object.UserReceita);
                    var cell = new ImageCell
                    {
                        TextColor = Color.Black,
                        Text = receita.Object.NomeReceita,
                        ImageSource = path
                    };
                    section.Add(cell);
                }
            }

            TableView tableView = new TableView
            {
                Intent = TableIntent.Data,
                Root = new TableRoot { }
            };
            tableView.Root.Add(section);

            this.Content = new StackLayout
            {
                Children =
                {
                    buttonSearch,
                    tableView
                }
            };

            BindingContext = this;
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void listaViewReceitas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ReceitaDetailedView(e.Item.ToString()));
        }
    }
}
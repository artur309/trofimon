using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class searchView : ContentPage
    {
        public searchView()
        {
            InitializeComponent();
            listaReceitas.ItemsSource = receitas;
        }

        List<String> receitas = new List<String>
        {
            "Francesinha", "Sopa de feijao","ai"
        };


        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var texto = searchBar.Text;
            listaReceitas.ItemsSource = receitas.Where(x => x.ToLower().Contains(texto.ToLower()));

        }
    }
}

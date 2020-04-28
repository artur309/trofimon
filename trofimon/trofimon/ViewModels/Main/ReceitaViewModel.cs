using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using trofimon.ViewModel;
using Xamarin.Forms;
using trofimon.Views;
using trofimon.Utils;
using EncryptStringSample;

namespace trofimon.ViewModels.Main
{
    public class ReceitaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string nomeReceita;
        public string NomeReceita
        {
            get => nomeReceita;
            set
            {
                nomeReceita = value;
                PropertyChanged(this, new PropertyChangedEventArgs("NomeReceita"));
            }
        }

        private string ingredientes;
        public string Ingredientes
        {
            get => ingredientes;
            set
            {
                ingredientes = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Ingredientes"));
            }
        }

        private string preparacao;
        public string Preparacao
        {
            get => preparacao;
            set
            {
                preparacao = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Preparacao"));
            }
        }

        private string imagem;
        public string Imagem
        {
            get => imagem;
            set
            {
                imagem = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Imagem"));
            }
        }

        // private bool privacidade;
        /*public bool Privacidade
        {
            get => privacidade;
            set
            {
                privacidade = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Privacidade"));
            }
        }*/
        public Command GuardarReceitaCommand => new Command(GuardarReceita);
        private async void GuardarReceita()
        {
            //validação, email ou password não é preenchida

            var receita = await FirebaseHelper.AddReceita(NomeReceita, Ingredientes, Preparacao, "s");
            //Adiciona User 
            if (receita)
            {
                await App.Current.MainPage.DisplayAlert("Receita guardada com Sucesso", "", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Erro ao guardar receita", "OK");
            }

        }

    }
}
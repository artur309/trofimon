using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using trofimon.ViewModel;
using Xamarin.Forms;
using trofimon.Views;
using trofimon.ViewModels.Main;
using Xamarin.Essentials;
using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;

namespace trofimon.ViewModels.Main
{
    public class ReceitaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        LoginViewModel loginViewModel = new LoginViewModel();

        private string userReceita;
        public string UserReceita
        {
            get => Preferences.Get(loginViewModel.Email, loginViewModel.Email);
            set
            {
                userReceita = Preferences.Get(loginViewModel.Email, loginViewModel.Email);
                PropertyChanged(this, new PropertyChangedEventArgs("UserReceita"));
            }
        }

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
        public string ImagemPath
        {
            get => imagem;
            set
            {
                imagem = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImagemPath"));
            }
        }

        private bool privacidade;
        public bool Privacidade
        {
            get => privacidade;
            set
            {
                privacidade = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Privacidade"));
            }
        }

        private ImageSource imgChoosed;
        public ImageSource ImageSource
        {
            get => imgChoosed;
            set
            {
                imgChoosed = value;
                PropertyChanged(this, new PropertyChangedEventArgs("imgChoosed"));
            }
        }

        //Guardar receita
        public Command GuardarReceitaCommand => new Command(GuardarReceita);
        private async void GuardarReceita()
        {
            //validação, email ou password não é preenchida

            var receita = await FirebaseHelper.AddReceita(UserReceita, NomeReceita, Ingredientes, Preparacao, ImagemPath, Privacidade);
            //Adiciona User 
            if (receita)
                //await firebaseStorageHelper.UploadFile(file.GetStream(), Path.GetFileName(file.Path));
                await App.Current.MainPage.DisplayAlert("Receita guardada com Sucesso", "", "Ok");
            else
                await App.Current.MainPage.DisplayAlert("Error", "Erro ao guardar receita", "OK");
        }
    }
}
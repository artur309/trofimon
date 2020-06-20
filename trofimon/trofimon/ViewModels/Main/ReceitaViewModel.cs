using System;
using System.ComponentModel;
using System.Diagnostics;
using trofimon.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace trofimon.ViewModels.Main
{
    public class ReceitaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private LoginViewModel loginViewModel = new LoginViewModel();

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

        private string receitaImagemPath;
        public string ReceitaImagemPath
        {
            get => receitaImagemPath;
            set
            {
                receitaImagemPath = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ReceitaImagemPath"));
            }
        }

        public bool receitaPrivacidade
        {
            get => Preferences.Get("receitaPrivacidade", false);
            set
            {
                try
                {   //para fazer handle de sessao
                    Preferences.Set("receitaPrivacidade", value);
                    PropertyChanged(this, new PropertyChangedEventArgs("receitaPrivacidade"));

                    Application.Current.Properties["receitaPrivacidade"] = false;
                    Application.Current.SavePropertiesAsync();
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Error:{e}");
                }
            }
        }

        //Guardar receita
        public Command GuardarReceitaCommand => new Command(GuardarReceita);

        private async void GuardarReceita()
        {
            //Adiciona Receita
            var receita = await FirebaseHelper.AddReceita(UserReceita, NomeReceita, Ingredientes, Preparacao, ReceitaImagemPath, receitaPrivacidade);
            if (receita)
                await App.Current.MainPage.DisplayAlert("Receita guardada com Sucesso", "Sucesso", "OK");
            else
                await App.Current.MainPage.DisplayAlert("Erro ao guardar receita", "Error", "OK");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using trofimon.Models;
using trofimon.ViewModel;
using trofimon.Views;
using trofimon.Views.Form;
using trofimon.Views.Main;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trofimon.ViewModel
{
    public static class FirebaseHelper
    {
        public static FirebaseClient firebase = new FirebaseClient("https://trofimon-pap.firebaseio.com/");

        //Lista de utilizadores
        public static async Task<List<Users>> GetAllUser()
        {
            try
            {
                var userlist = (await firebase
                    .Child("Users")
                    .OnceAsync<Users>()).Select(item =>
                new Users
                {
                    Email = item.Object.Email,
                    Password = item.Object.Password
                }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Read 
        public static async Task<Users> GetUser(string email)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebase
                    .Child("Users")
                    .OnceAsync<Users>();
                return allUsers.Where(a => a.Email == email).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Adicionar utilizador
        public static async Task<bool> AddUser(string email, string username, string password)
        {
            try
            {
                //id automatico 
                int IDUser = 0;
                var idCount = await firebase
                         .Child("Users")
                         .OrderByKey()
                         .OnceAsync<Users>();

                foreach (var id in idCount)
                {
                    IDUser++;
                    if (id.Object.Id == IDUser)
                        IDUser++;
                }

                bool? accountAvailable = null;
                //verificaoca de Utilizador
                foreach (var id in idCount)
                {
                    if (id.Object.Email != email)
                        accountAvailable = true;
                    else
                        accountAvailable = false;
                }

                //guarda novo user 
                if (accountAvailable == false)
                    return false;

                else
                {
                    Debug.WriteLine("Erro");
                    var user = await firebase
                    .Child("Users")
                    .PostAsync(new Users()
                    {
                        Email = email,
                        Username = username,
                        Password = password,
                        Id = IDUser
                    });
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Atualizar utilizador 
        public static async Task<bool> UpdateUser(string email, string password)
        {
            try
            {
                var toUpdateUser = (await firebase
                    .Child("Users")
                    .OnceAsync<Users>()).Where(a => a.Object.Email == email).FirstOrDefault();
                await firebase
                    .Child("Users")
                    .Child(toUpdateUser.Key)
                    .PutAsync(new Users() { Email = email, Password = password });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Apagar Utilizador
        public static async Task<bool> DeleteUser(string email)
        {
            try
            {
                var toDeletePerson = (await firebase
                    .Child("Users")
                    .OnceAsync<Users>()).Where(a => a.Object.Email == email).FirstOrDefault();
                await firebase.Child("Users").Child(toDeletePerson.Key).DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Lista de Receitas
        public static async Task<List<Receitas>> GetAllReceitas()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            try
            {
                var receitaLista = (await firebase
                    .Child("Receitas")
                    .Child(Preferences.Get(loginViewModel.Email, loginViewModel.Email))
                    .OnceAsync<Receitas>()).Select(item =>
                new Receitas
                {
                    NomeReceita = item.Object.NomeReceita,
                    Ingredientes = item.Object.Ingredientes,
                    Preparacao = item.Object.Preparacao,
                    Imagem = item.Object.Imagem,
                    Privacidade = item.Object.Privacidade
                }).ToList();
                return receitaLista;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        public static async Task<ObservableCollection<Receitas>> GetReceita(string user)
        {/*
            var userlist = (await firebase
                    .Child("Users")
                    .OnceAsync<Users>()).Select(item =>
                new Users
                {
                    Email = item.Object.Email,
                    Password = item.Object.Password
                }).ToList();
            return userlist;
            */
            try
            {
                var receitas = await firebase
                    .Child("Receitas")
                    .Child(Preferences.Get(user, user))
                    .OrderByKey()
                    .OnceAsync<Receitas>();

                ObservableCollection<Receitas> receitasList = new ObservableCollection<Receitas>();
                foreach (var receita in receitas)
                {
                    receitasList
                        .Select(item => 
                        new Receitas
                        {
                            NomeReceita = receita.Object.NomeReceita,
                        })
                        .ToString();

                    receitasList
                        .Add(new Receitas
                        {
                            NomeReceita = Convert.ToString(receita.Object.NomeReceita),
                        });
                }
                  
                return receitasList;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Adicionar Receita
        public static async Task<bool> AddReceita(string nomeReceita, string ingredientes, string preparacao, string imagem, bool privacidade)
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            try
            {
                await firebase
                .Child("Receitas")
                .Child(Preferences.Get(loginViewModel.Email, loginViewModel.Email))
                .PostAsync(new Receitas()
                {
                    NomeReceita = nomeReceita,
                    Ingredientes = ingredientes,
                    Preparacao = preparacao,
                    Imagem = imagem,
                    Privacidade = privacidade,
                });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
    }
}
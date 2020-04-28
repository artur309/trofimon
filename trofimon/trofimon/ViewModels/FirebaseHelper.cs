using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using trofimon.Models;

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
        public static async Task<bool> AddUser(string email, string password)
        {
            try
            {
                await firebase
                .Child("Users")
                .PostAsync(new Users() { Email = email, Password = password });
                return true;
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
            try
            {
                var receitaLista = (await firebase
                    .Child("Receitas")
                    .OnceAsync<Receitas>()).Select(item =>
                new Receitas
                {
                    NomeReceita = item.Object.NomeReceita,
                    Ingredientes = item.Object.Ingredientes,
                    Preparacao = item.Object.Preparacao,
                    Imagem = item.Object.Imagem,
                    //Privacidade = item.Object.Privacidade
                }).ToList();
                return receitaLista;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Adicionar Receita
        public static async Task<bool> AddReceita(string nomeReceita, string ingredientes, string preparacao, string imagem)
        {
            try
            {
                await firebase
                .Child("Receitas")
                .PostAsync(new Receitas()
                {
                    NomeReceita = nomeReceita, 
                    Ingredientes = ingredientes,
                    Preparacao = preparacao,
                    Imagem = imagem,
                    //Privacidade = privacidade,
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
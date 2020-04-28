using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;
using trofimon.Models;
using trofimon.ViewModel;
using Xamarin.Essentials;

namespace trofimon
{
    class FirebaseStorageHelper
    {
        public FirebaseStorage firebaseStorage = new FirebaseStorage("trofimon-pap.appspot.com");

        //string idUser = Preferences.Get(loginViewModel.Email, loginViewModel.Email);  

        public async Task<string> UploadFile(Stream fileStream, string fileName)
        {
            var imageUrl = await firebaseStorage
                .Child("Receitas")
                .Child("12")
                .Child(fileName)
                .PutAsync(fileStream);
            return imageUrl;
        }

        public async Task DeleteFile(string fileName)
        {
            await firebaseStorage
                .Child("Receitas")
                .Child("12")
                .Child(fileName)
                .DeleteAsync();
        }
    }
}
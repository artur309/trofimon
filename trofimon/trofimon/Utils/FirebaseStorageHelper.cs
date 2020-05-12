using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using trofimon.Models;
using trofimon.ViewModel;
using trofimon.Views;
using trofimon.Views.Form;
using trofimon.Views.Main;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trofimon
{
    public class FirebaseStorageHelper
    {
        public FirebaseStorage firebaseStorage = new FirebaseStorage("trofimon-pap.appspot.com");

        public async Task<string> UploadFile(Stream fileStream, string fileName)
        {
            var imageUrl = await firebaseStorage
                .Child("Receitas")
                .Child("IdName")
                .Child(fileName)
                .PutAsync(fileStream);
            return imageUrl;
        }

        public async Task<string> GetFile(string fileName)
        {
            return await firebaseStorage
                .Child("Receitas")
                .Child("IdName")
                .Child(fileName)
                .GetDownloadUrlAsync();
        }

        public async Task DeleteFile(string fileName)
        {
            await firebaseStorage
                .Child("Receitas")
                .Child("IdName")
                .Child(fileName)
                .DeleteAsync();
        }
    }
}
using Firebase.Storage;
using System.IO;
using System.Threading.Tasks;
using trofimon.ViewModel;
using Xamarin.Essentials;

namespace trofimon
{
    public class FirebaseStorageHelper
    {
        public FirebaseStorage firebaseStorage = new FirebaseStorage("trofimon-pap.appspot.com");
        LoginViewModel loginViewModel = new LoginViewModel();

        public async Task<string> UploadFile(Stream fileStream, string fileName)
        {
            var storageImage = await firebaseStorage
                .Child("Receitas")
                .Child(Preferences.Get(loginViewModel.Email, loginViewModel.Email))
                .Child(fileName)
                .PutAsync(fileStream);
            string imageUrl = storageImage;
            return imageUrl;
        }

        public async Task<string> GetFile(string fileName, string user)
        {
            return await firebaseStorage
                .Child("Receitas")
                .Child(user)
                .Child(fileName)
                .GetDownloadUrlAsync();
        }

        public async Task DeleteFile(string fileName)
        {
            await firebaseStorage
                .Child("Receitas")
                .Child(Preferences.Get(loginViewModel.Email, loginViewModel.Email))
                .Child(fileName)
                .DeleteAsync();
        }
    }
}
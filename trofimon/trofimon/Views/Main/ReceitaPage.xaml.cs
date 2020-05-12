using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trofimon.ViewModels.Main;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Diagnostics;
using System.IO;
using Plugin.Permissions.Abstractions;
using trofimon.ViewModels;
using System.ComponentModel;
using System.Threading;
using trofimon.ViewModel;
using trofimon.Views;
using trofimon.Utils;
using EncryptStringSample;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceitaPage : ContentPage
    {
        FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();
        ReceitaViewModel receitaViewModel;
        MediaFile file;

        public ReceitaPage()
        {
            receitaViewModel = new ReceitaViewModel();
            InitializeComponent();
            //set binding
            BindingContext = receitaViewModel;
        }

        //galeria imagens
        private async void PickImg(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                //Validacao de permissoes Ficheiros
                var permissoes = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                if (permissoes != Xamarin.Essentials.PermissionStatus.Granted)
                    permissoes = await Permissions.RequestAsync<Permissions.StorageRead>();
                if (permissoes != Xamarin.Essentials.PermissionStatus.Granted)
                    return;

                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });

                if (file == null) return;

                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        //camera
        private async void CameraImg(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                //Validacao de permissoes camera
                var permissoes = await Permissions.CheckStatusAsync<Permissions.Camera>();

                if (permissoes != Xamarin.Essentials.PermissionStatus.Granted)
                    permissoes = await Permissions.RequestAsync<Permissions.Camera>();
                if (permissoes != Xamarin.Essentials.PermissionStatus.Granted)
                    return;

                file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions() { });

                if (file == null) return;

                //if (file != null) imgChoosed.Source = ImageSource.FromStream(() => { return file.GetStream(); });

                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async void guardaIMG(object sender, EventArgs e) => await firebaseStorageHelper.UploadFile(file.GetStream(), Path.GetFileName(file.Path));
    }
}
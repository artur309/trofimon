using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using trofimon.ViewModels.Main;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceitaPage : ContentPage
    {
        private FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();
        private ReceitaViewModel receitaViewModel;
        private MediaFile file;

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

                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });

                if (file == null) return;

                imgChoosed.Source = ImageSource.FromStream(() => { return file.GetStream(); });

                ReceitaImagemPath.Text = $"{Path.GetFileName(file.Path)}";
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

                //file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions() {});

                if (file == null) return;

                if (file != null) imgChoosed.Source = ImageSource.FromStream(() => { return file.GetStream(); });

                ReceitaImagemPath.Text = $"{Path.GetFileName(file.Path)}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void guardaIMG(object sender, EventArgs e) => await firebaseStorageHelper.UploadFile(file.GetStream(), Path.GetFileName(file.Path));
    }
}
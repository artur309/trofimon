using trofimon.UWP;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace trofimon.UWP
{
    public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            Application.Current.Exit();
        }
    }
}

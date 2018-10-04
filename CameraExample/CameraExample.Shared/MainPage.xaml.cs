using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CameraExample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public void button_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();

            captureUI.ImageCaptured += (o, ea) =>
            {
                var messageDialog = new MessageDialog("Image captured, URI: " + ea.ImageUri.ToString());
                messageDialog.ShowAsync();
            };

            captureUI.ShowCameraCaptureUI();
        }
    } // class

    public class ImageCapturedEventArgs : EventArgs
    {
        public Android.Net.Uri ImageUri { get; internal set; }
        public Bitmap Image { get; internal set; }
    } // class

    public partial class CameraCaptureUI : Activity
    {
        public delegate void ImageCapturedEventHandler(object sender, ImageCapturedEventArgs e);
        public event ImageCapturedEventHandler ImageCaptured;

        public Context Context { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");

            ImageCaptured?.Invoke(this, new ImageCapturedEventArgs { Image = bitmap });
        }

        public CameraCaptureUI()
        {
            this.Context = Android.App.Application.Context;
        }

        public void ShowCameraCaptureUI()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);

            //Start the intent
            Context.StartActivity(intent);
        }
    } // class
} // namespace

using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Activity;
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Platform;
using MyGYMMaui.Platforms.Android;

namespace MyWebApplication
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public static void Set() => MyWebApplication.MainPage.BlazorWebViewInitializing = BlazorWebViewInitialized;

        public static void BlazorWebViewInitialized(object? sender, BlazorWebViewInitializedEventArgs e)
        {
            if (e.WebView.Context?.GetActivity() is not ComponentActivity activity)
            {
                throw new InvalidOperationException($"The permission-managing WebChromeClient requires that the current activity be a '{nameof(ComponentActivity)}'.");
            }

            e.WebView.Settings.JavaScriptEnabled = true;
            e.WebView.Settings.AllowFileAccess = true;
            e.WebView.Settings.MediaPlaybackRequiresUserGesture = false;
            e.WebView.Settings.SetGeolocationEnabled(true);
            e.WebView.Settings.SetGeolocationDatabasePath(e.WebView.Context?.FilesDir?.Path);
            e.WebView.SetWebChromeClient(new PermissionManagingBlazorWebChromeClient(e.WebView.WebChromeClient!, activity));
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (Intent?.Action == Android.Content.Intent.ActionSend)
            {
                Stream? inputStream = null;
                var filePath = Intent?.ClipData?.GetItemAt(0);
                if (filePath?.Uri != null)
                {
                    inputStream = ContentResolver!.OpenInputStream(filePath.Uri)!;
                }

                if (inputStream != null)
                {
                    using (var reader = new StreamReader(inputStream))
                    {
                        var content = reader.ReadToEnd();

                        //process the content here...
                    }

                    inputStream.Close();
                    inputStream.Dispose();
                }
            }
        }
    }
}

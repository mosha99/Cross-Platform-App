using Microsoft.AspNetCore.Components.WebView;

namespace MyWebApplication
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            blazorWebView.BlazorWebViewInitialized += (sender, args) =>
            {
                BlazorWebViewInitializing?.Invoke(sender, args);
            };
        }

        public static Action<object?, BlazorWebViewInitializedEventArgs> BlazorWebViewInitializing;
    }
}

using Microsoft.UI.Xaml.Controls;


namespace UnoApp6.Presentation {
    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();
        }


        private void GoToJSONListe(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateViewAsync<JSONListe>(this);

        }

    }
}
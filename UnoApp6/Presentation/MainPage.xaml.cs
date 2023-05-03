namespace UnoApp6.Presentation {

    public sealed partial class MainPage : Page {

        public MainPage() {
            this.InitializeComponent();
        }

        private void GoToJSONListe(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(JSONListe), dateiName.Text);

        }
    }
}
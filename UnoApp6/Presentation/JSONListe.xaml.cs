namespace UnoApp6.Presentation {
    public sealed partial class JSONListe : Page {



        public JSONListe() {
            this.InitializeComponent();
        }

        private void GoToMainPage(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern));
        }

    }
}
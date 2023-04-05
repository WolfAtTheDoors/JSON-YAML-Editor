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

        private void GoToBestaetigen(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Bestaetigen));
        }

        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter)) {
                JSON.Text = e.Parameter.ToString();

                dateiName.Text = e.Parameter.ToString();

            }
            else {
                JSON.Text = "Bitte gib einen gültigen Dateipfad ein!";
            }
            base.OnNavigatedTo(e);
        }




    }
}
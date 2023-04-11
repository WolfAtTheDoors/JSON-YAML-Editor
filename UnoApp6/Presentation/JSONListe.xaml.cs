using System.Text.Json;

namespace UnoApp6.Presentation {
    public sealed partial class JSONListe : Page {

        public JSONListe() {
            this.InitializeComponent();
            displayJSONContents();
        }

        private void GoToMainPage(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(MainPage));
        }
        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern), dateiName.Text);
        }
        private void GoToBestaetigen(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Bestaetigen));
        }
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter)) {
                //JSON.Text = e.Parameter.ToString();

                dateiName.Text = e.Parameter.ToString();

            }
            else {
                dateiName.Text = "Bitte gib einen gültigen Dateipfad ein!";
            }
            base.OnNavigatedTo(e);
        }

        // S:\Austausch\gisela\vmListe.json
        // display the json content
        private void displayJSONContents() {
            if (!string.IsNullOrWhiteSpace((string)dateiName.Text)) {       // <-- false

                string jsonData = File.ReadAllText(dateiName.Text);

            }
            else {
                jsonData.Text = "Bitte gib einen gültigen Dateipfad ein!";
            }


        }



    }
}
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UnoApp6.Presentation {
    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();
        }

        private void GoToJSONListe(object sender, RoutedEventArgs e) {

            //validate input as not null, empty or whitespace
            if (dateiName.Text is null || string.IsNullOrWhiteSpace((string)dateiName.Text)) {
                Fehlermeldung.Text = "Bitte gib einen gültigen Dateipfad ein!";
                this.Frame.Navigate(typeof(MainPage), Fehlermeldung.Text);
                return;
            }

            //validate as a filepath
            try {
                string jsonData = File.ReadAllText(dateiName.Text!);
                //validate file not empty
                if (jsonData == null) {
                    Fehlermeldung.Text = "Bitte gib einen gültigen Dateipfad ein!";
                    this.Frame.Navigate(typeof(MainPage), Fehlermeldung.Text);
                    return;
                }

                //valid JSON?
                if (!isJSON(jsonData)) {
                    Fehlermeldung.Text = "Bitte gib einen gültigen Dateipfad ein!";
                    this.Frame.Navigate(typeof(MainPage), Fehlermeldung.Text);
                    return;
                }

                this.Frame.Navigate(typeof(JSONListe), dateiName.Text);

            }
            catch (Exception x) {
                Fehlermeldung.Text = "Bitte gib einen gültigen Dateipfad ein!";
                this.Frame.Navigate(typeof(MainPage), Fehlermeldung.Text);
            }
        }

        private void GoToMainPage(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(MainPage), Fehlermeldung.Text);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {

            if (e.Parameter != null) {
                Fehlermeldung.Text = e.Parameter.ToString();
            }

            base.OnNavigatedTo(e);
        }
        private bool isJSON(string json) {

            try {
                JObject.Parse(json);
            }
            catch (Exception e) {
                return false;
            }
            return true;
        }
    }
}
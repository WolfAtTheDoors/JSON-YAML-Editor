using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace UnoApp6.Presentation {
    public sealed partial class Andern : Page {
        //JSONListe.jsonDataOriginal;  speichert die originale Datei, die später überschrieben wird
        public static JObject? jsonObject;
        public static string kopierterName = "Hello";
        public static string? kopierteEigenschaft = "World";

        public Andern() {
            this.InitializeComponent();
        }

        private void Ubernehmen(object sender, RoutedEventArgs e) {
            int ignore;
            double ignore2;

            //deserialisieren
            jsonObject = JObject.Parse(jsonData.Text);
            //eigenschaft entfernen
            if ((!string.IsNullOrEmpty(alterName.Text)) && string.IsNullOrEmpty(neuerName.Text) && string.IsNullOrEmpty(neuerWert.Text)) {
                jsonObject.Remove(alterName.Text);
            }
            //namen ersetzen
            if ((!string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text)) && string.IsNullOrEmpty(neuerWert.Text)) {
                jsonData.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                jsonData.Text = jsonData.Text.Replace(alterName.Text, neuerName.Text);
                jsonObject = JObject.Parse(jsonData.Text);
            }

            //Parse an integer
            if (int.TryParse(neuerWert.Text, out ignore)) {
                int neuerWertZahl = int.Parse(neuerWert.Text);

                //eigenschaft hinzufügen (name)
                if (string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text)) {
                    jsonObject[neuerName.Text] = neuerWertZahl;
                }
                //Wert ersetzen
                if ((!string.IsNullOrEmpty(alterName.Text)) && string.IsNullOrEmpty(neuerName.Text) && (!string.IsNullOrEmpty(neuerWertZahl.ToString()))) {
                    jsonObject[alterName.Text] = neuerWertZahl;
                }
                //alles ändern
                if (!string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text) && !string.IsNullOrEmpty(neuerWertZahl.ToString())) {
                    jsonObject[alterName.Text] = neuerWertZahl;
                    jsonData.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                    jsonData.Text = jsonData.Text.Replace(alterName.Text, neuerName.Text);
                    jsonObject = JObject.Parse(jsonData.Text);
                }
            }
            //Parse a double
            else if (double.TryParse(neuerWert.Text, out ignore2)) {
                neuerWert.Text = neuerWert.Text.Replace(",", ".");
                double neuerWertZahl = double.Parse(neuerWert.Text, NumberStyles.Any, CultureInfo.InvariantCulture);
                //eigenschaft hinzufügen (name)
                if (string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text)) {
                    jsonObject[neuerName.Text] = neuerWertZahl;
                }
                //Wert ersetzen
                if ((!string.IsNullOrEmpty(alterName.Text)) && string.IsNullOrEmpty(neuerName.Text) && (!string.IsNullOrEmpty(neuerWertZahl.ToString()))) {
                    jsonObject[alterName.Text] = neuerWertZahl;
                }
                //alles ändern
                if (!string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text) && !string.IsNullOrEmpty(neuerWertZahl.ToString())) {
                    jsonObject[alterName.Text] = neuerWertZahl;
                    jsonData.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                    jsonData.Text = jsonData.Text.Replace(alterName.Text, neuerName.Text);
                    jsonObject = JObject.Parse(jsonData.Text);
                }
            }
            //Parse a string
            else {
                //eigenschaft hinzufügen (name)
                if (string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text)) {
                    jsonObject[neuerName.Text] = neuerWert.Text;
                }
                //Wert ersetzen
                if ((!string.IsNullOrEmpty(alterName.Text)) && string.IsNullOrEmpty(neuerName.Text) && (!string.IsNullOrEmpty(neuerWert.Text))) {
                    jsonObject[alterName.Text] = neuerWert.Text;
                }
                //alles ändern
                if (!string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text) && !string.IsNullOrEmpty(neuerWert.Text)) {
                    jsonObject[alterName.Text] = neuerWert.Text;
                    jsonData.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                    jsonData.Text = jsonData.Text.Replace(alterName.Text, neuerName.Text);
                    jsonObject = JObject.Parse(jsonData.Text);
                }
            }

            //zurück serialisieren
            jsonData.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

            //zurück zur Darstellung, mit geändertem Objekt
            this.Frame.Navigate(typeof(Andern), jsonData.Text);
        }

        private void kopieren(object sender, RoutedEventArgs e) {
            jsonObject = JObject.Parse(jsonData.Text);

            kopierterName = alterName.Text;
            kopierteEigenschaft = (string?)jsonObject[alterName.Text];

            this.Frame.Navigate(typeof(Andern), jsonData.Text);

        }
        private void einfügen(object sender, RoutedEventArgs e) {
            jsonObject = JObject.Parse(jsonData.Text);

            if (kopierterName != "Hello") {
                jsonObject[kopierterName] = kopierteEigenschaft;
            }
            jsonData.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

            this.Frame.Navigate(typeof(Andern), jsonData.Text);
        }
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        private void GoHome(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(JSONListe), jsonData.Text);
        }
        private void speichern(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateViewAsync<JSONListe2>(this, qualifier: Qualifiers.Dialog, jsonData.Text);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {

            jsonData.Text = e.Parameter.ToString();

            base.OnNavigatedTo(e);
        }
    }
}


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
// C:\Users\gisela.wolf\Projekte\vmListe.json array
// C:\Users\gisela.wolf\Projekte\rahmenduebel.json objekte
// C:\Users\gisela.wolf\Projekte\UnoApp6-master\EinfachstesJSON.json
// C:\Users\gisela.wolf\Projekte\TestDatei.json

namespace UnoApp6.Presentation {

    public sealed partial class Andern : Page {
        public static string kopierterName = "Hello";
        public static string? kopierteEigenschaft = "World";
        //JSONListe.jsonDataOriginal;  keeps the original file in order to add the changes to it

        public static JObject? jsonObject;

        public Andern() {
            this.InitializeComponent();
        }

        //private void GoToJSONListe(object sender, RoutedEventArgs e) {
        //    this.Frame.Navigate(typeof(JSONListe));
        //}
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        private void GoToBestaetigen(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateViewAsync<JSONListe2>(this, qualifier: Qualifiers.Dialog, jsonData.Text);

        }    //<-- passes on the altered object to JSONListe 3
        private void Ubernehmen(object sender, RoutedEventArgs e) {
            //Deserialize
            jsonObject = JObject.Parse(jsonData.Text);

            //eigenschaft entfernen !
            if ((!string.IsNullOrEmpty(alterName.Text)) && string.IsNullOrEmpty(neuerName.Text) && string.IsNullOrEmpty(neuerWert.Text)) {
                jsonObject.Remove(alterName.Text);
            }
            //eigenschaft hinzufügen (name) X
            if (string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text)) {
                jsonObject[neuerName.Text] = neuerWert.Text;
            }
            //namen ersetzen !
            if ((!string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text)) && string.IsNullOrEmpty(neuerWert.Text)) {
                jsonData.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                jsonData.Text = jsonData.Text.Replace(alterName.Text, neuerName.Text);
                jsonObject = JObject.Parse(jsonData.Text);
            }
            //Wert ersetzen !
            if ((!string.IsNullOrEmpty(alterName.Text)) && string.IsNullOrEmpty(neuerName.Text) && (!string.IsNullOrEmpty(neuerWert.Text))) {
                jsonObject[alterName.Text] = neuerWert.Text;
            }
            //alles ändern !
            if (!string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text) && !string.IsNullOrEmpty(neuerWert.Text)) {
                jsonObject[alterName.Text] = neuerWert.Text;
                jsonData.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                jsonData.Text = jsonData.Text.Replace(alterName.Text, neuerName.Text);
                jsonObject = JObject.Parse(jsonData.Text);
            }

            //zurück serialisieren
            jsonData.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

            //navigate to same page with updated jsonData.Text
            this.Frame.Navigate(typeof(Andern), jsonData.Text);
        }
        private void kopieren(object sender, RoutedEventArgs e) {
            jsonObject = JObject.Parse(jsonData.Text);

            kopierterName = alterName.Text;
            kopierteEigenschaft = (string?)jsonObject[alterName.Text];

            jsonData.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

            this.Frame.Navigate(typeof(Andern), jsonData.Text);
        }
        private void einfügen(object sender, RoutedEventArgs e) {
            //deserialise
            jsonObject = JObject.Parse(jsonData.Text);

            if (kopierterName != "Hello") {
                jsonObject[kopierterName] = kopierteEigenschaft;
            }


            //reserialise
            jsonData.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

            this.Frame.Navigate(typeof(Andern), jsonData.Text);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {

            jsonData.Text = e.Parameter.ToString();

            base.OnNavigatedTo(e);
        }
    }
}






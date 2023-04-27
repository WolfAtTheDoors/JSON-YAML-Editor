using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json;
using Windows.Management.Deployment.Preview;

namespace UnoApp6.Presentation {
    public sealed partial class JSONListe2 : Page {
        public static string dateiPfad = "C:\\Users\\gisela.wolf\\Projekte\\default.json";

        public JSONListe2() {
            this.InitializeComponent();
        }

        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern), jsonData.Text);
        }
        private void GoToOffnen(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateViewAsync<Offnen>(this, qualifier: Qualifiers.Dialog, jsonData.Text);
            //this.Frame.Navigate(typeof(Offnen), jsonData.Text);
        }
        private void GoToBestaetigen(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateViewAsync<Bestaetigen>(this, qualifier: Qualifiers.Dialog, jsonData.Text);
            //this.Frame.Navigate(typeof(Bestaetigen), jsonData.Text);
        }
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter)) {

                jsonData.Text = e.Parameter.ToString();

            }

            else {
                dateiName.Text = "Bitte gib einen gültigen Dateipfad ein!";
            }
            base.OnNavigatedTo(e);
        }

        // S:\Austausch\gisela\vmListe.json chaos
        // S:\Austausch\gisela\rahmenduebel.json ordnung
        // C:\Users\gisela.wolf\Projekte\UnoApp6-master\EinfachstesJSON.json
        //  C:\Users\gisela.wolf\Projekte\TestDatei.json

    }
}




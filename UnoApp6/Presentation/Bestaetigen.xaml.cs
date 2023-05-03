// C:\Users\gisela.wolf\Projekte\vmListe.json array
// C:\Users\gisela.wolf\Projekte\rahmenduebel.json objekte
// C:\Users\gisela.wolf\Projekte\UnoApp6-master\EinfachstesJSON.json
//  C:\Users\gisela.wolf\Projekte\TestDatei.json

namespace UnoApp6.Presentation {

    public sealed partial class Bestaetigen : Page {
        public Bestaetigen() {
            this.InitializeComponent();
        }

        //JSONListe.jsonDataOriginal;  keeps the original file in order to add the changes to it

        private void speichern(object sender, RoutedEventArgs e) {

            File.WriteAllText(JSONListe.dateiPfad, jsonData.Text);          //<-- hier wird die gesamte JSON gespeichert

            this.Frame.Navigate(typeof(JSONListe), JSONListe.dateiPfad);

            //_ = this.Navigator()?.NavigateBackAsync(this);

        }
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {

            jsonData.Text = e.Parameter.ToString();

            base.OnNavigatedTo(e);
        }

    }
}

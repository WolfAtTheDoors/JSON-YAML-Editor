// C:\Users\gisela.wolf\Projekte\vmListe.json array
// C:\Users\gisela.wolf\Projekte\rahmenduebel.json objekte
// C:\Users\gisela.wolf\Projekte\UnoApp6-master\EinfachstesJSON.json
//  C:\Users\gisela.wolf\Projekte\TestDatei.json

namespace UnoApp6.Presentation {
    public sealed partial class JSONListe2 : Page {

        public JSONListe2() {
            this.InitializeComponent();
        }
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern), jsonData.Text);
        }
        private void GoToOffnen(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateViewAsync<Offnen>(this, qualifier: Qualifiers.Dialog, jsonData.Text);

        }
        private void GoToBestaetigen(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateViewAsync<JSONListe3>(this, qualifier: Qualifiers.Dialog, jsonData.Text);

        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {

            jsonData.Text = e.Parameter.ToString();

            base.OnNavigatedTo(e);
        }
    }
}




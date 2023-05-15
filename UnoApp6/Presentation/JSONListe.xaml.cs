
namespace UnoApp6.Presentation {
    public sealed partial class JSONListe : Page {
        public static string dateiPfad = "C:\\Users\\gisela.wolf\\Projekte\\default.json";
        public static string? jsonDataOriginal;  //hält das orignal vor, um es später zu ändern
        public static string? Fehlermeldung = "!";

        public JSONListe() {
            this.InitializeComponent();
        }

        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern), jsonData.Text);
        }
        private void GoToOffnen(object sender, RoutedEventArgs e) {
            //_ = this.Navigator()?.NavigateViewAsync<Offnen>(this, qualifier: Qualifiers.Dialog, jsonData.Text);
            this.Frame.Navigate(typeof(Offnen), jsonData.Text);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {

            string? dateiName = e.Parameter.ToString();
            //dateiPfad = dateiName.Text!;
            try {
                jsonData.Text = File.ReadAllText(dateiName!);
                jsonDataOriginal = jsonData.Text;
            }
            catch {
                jsonData.Text = dateiName;
            }


            base.OnNavigatedTo(e);
        }
    }
}



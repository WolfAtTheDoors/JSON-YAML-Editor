using Microsoft.UI.Composition;

namespace UnoApp6.Presentation {
    public sealed partial class JSONListe : Page {
        public static string dateiPfad = "C:\\default.json";
        public static string? dataOriginal;  //hält das Orignal vor, um es später zu ändern

        public JSONListe() {
            this.InitializeComponent();
        }

        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern), dataText.Text);
        }
        private void GoToOffnen(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Offnen), dataText.Text);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            string? dateiName;
            dateiName = e.Parameter.ToString();

            try {
                dataText.Text = File.ReadAllText(dateiName!);
                dateiPfad = dateiName!;
                dataOriginal = dataText.Text;
            }
            catch {
                dataText.Text = dateiName;
            }

            if (!Offnen.objectIsCorrect) {
                deineDatei.Text = "Das war kein gültiges Objekt. Bitte nochmal probieren."; 
            }
            else if (MainPage.fileIsYAML) {

                deineDatei.Text = "Deine YAML";
            }
            else {

                deineDatei.Text = "Deine JSON";

            }

            base.OnNavigatedTo(e);

        }
    }
}


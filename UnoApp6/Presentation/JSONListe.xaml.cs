namespace UnoApp6.Presentation {
    public sealed partial class JSONListe : Page {
        public static string dateiPfad = "C:\\default.json";
        public static string? dataOriginal;  //hält das orignal vor, um es später zu ändern

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

            if (Offnen.objektNameNummerListe.Count < 1) {
                dateiName = e.Parameter.ToString();
                dataText.Text = File.ReadAllText(dateiName!);
                dataOriginal = dataText.Text;
            }
            else {
                dateiName = e.Parameter.ToString();
                dataText.Text = dateiName;
            }


            if (MainPage.fileIsYAML) {
                deineDatei.Text = "Deine YAML";
            }
            else {
                deineDatei.Text = "Deine JSON";
            }

            base.OnNavigatedTo(e);

        }
    }
}


using Newtonsoft.Json.Linq;
using System.Dynamic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace UnoApp6.Presentation {
    public sealed partial class MainPage : Page {
        public static bool fileIsYAML = false;

        public MainPage() {
            this.InitializeComponent();
        }
        private void GoToJSONListe(object sender, RoutedEventArgs e) {

            //validiert den Input als string, nicht leer und nicht null
            if (dateiName.Text is null || string.IsNullOrWhiteSpace((string)dateiName.Text)) {
                Fehlermeldung.Text = "Bitte gib einen gültigen Dateipfad ein!";
                this.Frame.Navigate(typeof(MainPage), Fehlermeldung.Text);
                return;
            }

            //validiert den Dateipfad
            try {
                string jsonData = File.ReadAllText(dateiName.Text!);

                //validiert die Datei
                if (jsonData == null) {
                    Fehlermeldung.Text = "Bitte gib einen gültigen Dateipfad ein!";
                    this.Frame.Navigate(typeof(MainPage), Fehlermeldung.Text);
                    return;
                }

                //validiert die JSON
                if (!isJSON(jsonData)) {
                    Fehlermeldung.Text = "Bitte gib einen gültigen Dateipfad ein!";
                    this.Frame.Navigate(typeof(MainPage), Fehlermeldung.Text);

                    //validiert die YAML
                    if (isYAML(jsonData)) {
                        fileIsYAML = true;
                        this.Frame.Navigate(typeof(JSONListe), dateiName.Text);
                        return;
                    }
                    else {
                        return;
                    }
                }

                this.Frame.Navigate(typeof(JSONListe), dateiName.Text);

            }
            catch (Exception x) {
                Fehlermeldung.Text = "Bitte gib einen gültigen Dateipfad ein!";
                this.Frame.Navigate(typeof(MainPage), Fehlermeldung.Text);
            }
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
        private bool isYAML(string json) {

            try {
                var deserializer = new DeserializerBuilder()
                    //.WithNamingConvention(HyphenatedNamingConvention.Instance)
                    .Build();
                var yaml = deserializer.Deserialize<ExpandoObject>(json);
            }
            catch (Exception e) {
                return false;
            }
            return true;
        }
    }
}
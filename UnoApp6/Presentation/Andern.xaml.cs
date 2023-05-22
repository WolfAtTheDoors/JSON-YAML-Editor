using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Globalization;
using YamlDotNet;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace UnoApp6.Presentation {
    public sealed partial class Andern : Page {
        //JSONListe.dataOriginal;  speichert die originale Datei, die später überschrieben wird
        public static string kopierterName = "Hello";
        public static dynamic? kopierteEigenschaft = "World";
        public static JObject? jsonObject;

        public Andern() {
            this.InitializeComponent();
        }

        private void Ubernehmen(object sender, RoutedEventArgs e) {

            if (!MainPage.fileIsYAML) {
                int parsedInt;
                double parsedDouble;
                bool parsedBool;
                jsonObject = JObject.Parse(dataText.Text);

                //eigenschaft entfernen
                if ((!string.IsNullOrEmpty(alterName.Text)) && string.IsNullOrEmpty(neuerName.Text) && string.IsNullOrEmpty(neuerWert.Text)) {
                    jsonObject.Remove(alterName.Text);
                }
                //namen ersetzen
                if ((!string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text)) && string.IsNullOrEmpty(neuerWert.Text)) {
                    dataText.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                    dataText.Text = dataText.Text.Replace(alterName.Text, neuerName.Text);
                    jsonObject = JObject.Parse(dataText.Text);
                }

                //Parse an integer
                if (int.TryParse(neuerWert.Text, out parsedInt)) {

                    //eigenschaft hinzufügen (name)
                    if (string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text)) {
                        jsonObject[neuerName.Text] = parsedInt;
                    }
                    //Wert ersetzen
                    if ((!string.IsNullOrEmpty(alterName.Text)) && string.IsNullOrEmpty(neuerName.Text) && (!string.IsNullOrEmpty(parsedInt.ToString()))) {
                        jsonObject[alterName.Text] = parsedInt;
                    }
                    //alles ändern
                    if (!string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text) && !string.IsNullOrEmpty(parsedInt.ToString())) {
                        jsonObject[alterName.Text] = parsedInt;
                        dataText.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                        dataText.Text = dataText.Text.Replace(alterName.Text, neuerName.Text);
                        jsonObject = JObject.Parse(dataText.Text);
                    }
                }

                //Parse a double
                else if (double.TryParse(neuerWert.Text, out parsedDouble)) {
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
                        dataText.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                        dataText.Text = dataText.Text.Replace(alterName.Text, neuerName.Text);
                        jsonObject = JObject.Parse(dataText.Text);
                    }
                }
                //Parse a boolean
                else if (bool.TryParse(neuerWert.Text, out parsedBool)) {

                    //eigenschaft hinzufügen (name)
                    if (string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text)) {
                        jsonObject[neuerName.Text] = parsedBool;
                    }
                    //Wert ersetzen
                    if ((!string.IsNullOrEmpty(alterName.Text)) && string.IsNullOrEmpty(neuerName.Text) && (!string.IsNullOrEmpty(parsedBool.ToString()))) {
                        jsonObject[alterName.Text] = parsedBool;
                    }
                    //alles ändern
                    if (!string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text) && !string.IsNullOrEmpty(parsedBool.ToString())) {
                        jsonObject[alterName.Text] = parsedBool;
                        dataText.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                        dataText.Text = dataText.Text.Replace(alterName.Text, neuerName.Text);
                        jsonObject = JObject.Parse(dataText.Text);
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
                        dataText.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                        dataText.Text = dataText.Text.Replace(alterName.Text, neuerName.Text);
                        jsonObject = JObject.Parse(dataText.Text);
                    }
                }



                //zurück serialisieren
                dataText.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                //zurück zur Darstellung, mit geändertem Objekt
                this.Frame.Navigate(typeof(Andern), dataText.Text);

            }

            if (MainPage.fileIsYAML) {
                dynamic? yamlObject;
                string yamlText;

                //deserialize the yaml
                var deserializer = new DeserializerBuilder().Build();
                yamlObject = deserializer.Deserialize<dynamic>(dataText.Text);

                //eigenschaft entfernen
                if ((!string.IsNullOrEmpty(alterName.Text)) && string.IsNullOrEmpty(neuerName.Text) && string.IsNullOrEmpty(neuerWert.Text)) {
                    yamlObject.Remove(alterName.Text);
                }
                //namen ersetzen
                if ((!string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text)) && string.IsNullOrEmpty(neuerWert.Text)) {
                    var serializer2 = new SerializerBuilder().Build();
                    var yamlTextToReplace = serializer2.Serialize(yamlObject);

                    yamlTextToReplace = yamlTextToReplace.Replace(alterName.Text, neuerName.Text);

                    var deserializer2 = new DeserializerBuilder().Build();
                    yamlObject = deserializer2.Deserialize<dynamic>(yamlTextToReplace);
                }
                //eigenschaft hinzufügen (name)
                if (string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text)) {
                    yamlObject[neuerName.Text] = neuerWert.Text;
                }
                //Wert ersetzen
                if ((!string.IsNullOrEmpty(alterName.Text)) && string.IsNullOrEmpty(neuerName.Text) && (!string.IsNullOrEmpty(neuerWert.Text))) {
                    yamlObject[alterName.Text] = neuerWert.Text;
                }
                //alles ändern
                if (!string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text) && !string.IsNullOrEmpty(neuerWert.Text)) {
                    yamlObject[alterName.Text] = neuerWert.Text;

                    var serializer2 = new SerializerBuilder().Build();
                    var yamlTextToReplace = serializer2.Serialize(yamlObject);

                    yamlTextToReplace = yamlTextToReplace.Replace(alterName.Text, neuerName.Text);

                    var deserializer2 = new DeserializerBuilder().Build();
                    yamlObject = deserializer2.Deserialize<dynamic>(yamlTextToReplace);
                }

                //zurück serialisieren
                var serializer = new SerializerBuilder().Build();
                yamlText = serializer.Serialize(yamlObject);

                //zurück zur Darstellung, mit geändertem Objekt
                this.Frame.Navigate(typeof(Andern), yamlText);

            }
        }

        private void kopieren(object sender, RoutedEventArgs e) {

            if (!MainPage.fileIsYAML) {
                jsonObject = JObject.Parse(dataText.Text);

                kopierterName = alterName.Text;
                kopierteEigenschaft = jsonObject[alterName.Text];
            }
            else {
                var deserializer = new DeserializerBuilder().Build();
                var yamlObject = deserializer.Deserialize<dynamic>(dataText.Text);
                kopierterName = alterName.Text;
                kopierteEigenschaft = yamlObject[alterName.Text];

            }


            this.Frame.Navigate(typeof(Andern), dataText.Text);

        }
        private void einfügen(object sender, RoutedEventArgs e) {

            if (!MainPage.fileIsYAML) {
                jsonObject = JObject.Parse(dataText.Text);
                if (kopierterName != "Hello") {
                    jsonObject[kopierterName] = kopierteEigenschaft;
                }
                dataText.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);


            }
            else {
                var deserializer = new DeserializerBuilder().Build();
                var yamlObject = deserializer.Deserialize<dynamic>(dataText.Text);
                if (kopierterName != "Hello") {
                    yamlObject[kopierterName] = kopierteEigenschaft;
                }
                var serializer = new SerializerBuilder().Build();
                dataText.Text = serializer.Serialize(yamlObject);

            }

            this.Frame.Navigate(typeof(Andern), dataText.Text);

        }

        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        private void GoHome(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(JSONListe), dataText.Text);
        }
        private void speichern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(JSONListe2), dataText.Text);

        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {

            dataText.Text = e.Parameter.ToString();

            base.OnNavigatedTo(e);
        }
    }
}



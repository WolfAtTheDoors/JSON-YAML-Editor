using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YamlDotNet.Serialization;

namespace UnoApp6.Presentation {
    public sealed partial class Offnen : Page {
        public static JToken? jsonObject;
        public static JToken? jsonObjectDesired;
        public static dynamic? yamlObjectDesired;
        public static string? dataText = "default";
        public static List<string> objektNameNummerListe = new List<string>();
        public Offnen() {
            this.InitializeComponent();
        }
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        private void GoToJSONListe(object sender, RoutedEventArgs e) {

            if (!MainPage.fileIsYAML) {
                //input ist Arraynummer oder Objektname. Diese Liste liefert später die Adresse, mit der Änderungen gespeichert werden.
                jsonObject = JToken.Parse(dataText!);
                objektNameNummerListe.Add(objektNummerName.Text);

                if (jsonObject is JArray) {                                                                                           //Wenn das Objekt ein Array ist,
                    var jsonObjectArray = jsonObject as JArray;
                    if (objektNameNummerListe[objektNameNummerListe.Count - 1].All(char.IsDigit)) {                                   //muss es mit einem integer angesprochen werden,
                        if (int.Parse(objektNameNummerListe[objektNameNummerListe.Count - 1]) <= ((int)jsonObjectArray!.Count)) {     //der kleiner gleich der Anzahl der Objekte im Array ist
                            jsonObjectDesired = jsonObjectArray[int.Parse(objektNameNummerListe[objektNameNummerListe.Count - 1])];
                        }
                        else {
                            jsonObjectDesired = "So viele Objekte hat das Array nicht.";
                        }
                    }
                    else {
                        jsonObjectDesired = "Dieses Objekt ist ein Array und muss mit einem Integer aufgerufen werden.";
                    }
                }

                else if (jsonObject is JObject) {
                    jsonObjectDesired = jsonObject[objektNameNummerListe[objektNameNummerListe.Count - 1]];
                }

                else {
                    jsonObjectDesired = jsonObject;
                }

                if (jsonObjectDesired != null) {
                    dataText = JsonConvert.SerializeObject(jsonObjectDesired, Formatting.Indented);
                }
                else {
                    dataText = "Bitte das Objekt mit einer Nummer (im Array) oder mit dem Namen (im Objekt) aufrufen.";
                }
            }

            if (MainPage.fileIsYAML) {
                var deserializer = new DeserializerBuilder().Build();
                var yamlObject = deserializer.Deserialize<dynamic>(dataText!);

                objektNameNummerListe.Add(objektNummerName.Text);

                yamlObjectDesired = yamlObject[objektNameNummerListe[objektNameNummerListe.Count - 1]];

                var serializer = new SerializerBuilder().Build();
                dataText = serializer.Serialize(yamlObjectDesired);
            }

            this.Frame.Navigate(typeof(JSONListe), dataText);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            dataText = e.Parameter.ToString();
            base.OnNavigatedTo(e);

        }
    }
}



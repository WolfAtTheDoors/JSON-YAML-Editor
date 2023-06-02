using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using YamlDotNet.Serialization;

namespace UnoApp6.Presentation {
    public sealed partial class Offnen : Page {
        public static JToken? jsonObject;
        public static JToken? jsonObjectDesired;
        public static dynamic? yamlObjectDesired;
        public static string? dataText = "default";
        public static List<object> objektNameNummerListe = new List<object>();
        public static bool objectIsCorrect = true;
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

                if (jsonObject is JArray) {
                    var jsonObjectArray = jsonObject as JArray;
                    if ((!Int32.TryParse(objektNummerName.Text, out int result)) || result >= ((int)jsonObjectArray!.Count)) {
                        dataText = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                        objectIsCorrect = false;
                    }
                    else {
                        objektNameNummerListe.Add(objektNummerName.Text);
                        jsonObjectDesired = jsonObjectArray![result];
                        objectIsCorrect = true;
                        dataText = JsonConvert.SerializeObject(jsonObjectDesired, Formatting.Indented);

                    }
                }

                else if (jsonObject is JObject) {
                    if (jsonObject[objektNummerName.Text] is null) {
                        dataText = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                        objectIsCorrect = false;
                    }
                    else {
                        objektNameNummerListe.Add(objektNummerName.Text);
                        jsonObjectDesired = jsonObject[objektNameNummerListe[objektNameNummerListe.Count - 1]];
                        objectIsCorrect = true;
                        dataText = JsonConvert.SerializeObject(jsonObjectDesired, Formatting.Indented);
                    }
                }
            }

            if (MainPage.fileIsYAML) {
                var deserializer = new DeserializerBuilder().Build();
                var yamlObject = deserializer.Deserialize<dynamic>(dataText!);

                try {
                    yamlObjectDesired = yamlObject[objektNummerName.Text];
                    var serializer = new SerializerBuilder().Build();
                    dataText = serializer.Serialize(yamlObjectDesired);
                    objektNameNummerListe.Add(objektNummerName.Text);
                    objectIsCorrect = true;

                }
                catch {
                    var serializer = new SerializerBuilder().Build();
                    dataText = serializer.Serialize(yamlObject);
                    objectIsCorrect = false;
                }
            }

            this.Frame.Navigate(typeof(JSONListe), dataText);

        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            dataText = e.Parameter.ToString();
            base.OnNavigatedTo(e);
        }
    }
}

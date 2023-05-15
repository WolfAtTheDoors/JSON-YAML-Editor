using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UnoApp6.Presentation {
    public sealed partial class Offnen : Page {
        public static JToken? jsonObject;
        public static JToken? jsonObjectDesired;
        public static string? jsonData = "default";
        public static List<string> objektNameNummerListe = new List<string>();

        public Offnen() {
            this.InitializeComponent();
        }
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        private void GoToJSONListe(object sender, RoutedEventArgs e) {

            //input ist Arraynummer oder Objektname. Diese Liste liefert später die Adresse, mit der änderungen gespeichert werden.
            jsonObject = JToken.Parse(jsonData!);
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
                jsonData = JsonConvert.SerializeObject(jsonObjectDesired, Formatting.Indented);
            }
            else {
                jsonData = "Bitte das Objekt mit einer Nummer (im Array) oder mit dem Namen (im Objekt) aufrufen.";
            }

            this.Frame.Navigate(typeof(JSONListe), jsonData);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            jsonData = e.Parameter.ToString();

            base.OnNavigatedTo(e);

        }
    }
}

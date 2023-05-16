using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UnoApp6.Presentation {
    public sealed partial class JSONListe2 : Page {
        //JSONListe.jsonDataOriginal (original JSON)
        //jsonData.Text (geändertes Object)
        //Offnen.objektNameNummerListe (adress of object)
        public static string? alteredJsonData;
        public static JToken? OriginalChangedObject;
        public JSONListe2() {
            this.InitializeComponent();
        }

        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern), alteredJsonData);
        }
        private void GoToOffnen(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateViewAsync<Offnen>(this, qualifier: Qualifiers.Dialog, alteredJsonData);
        }
        private void speichern(object sender, RoutedEventArgs e) {
            // hier wird die gesamte JSON gespeichert
            File.WriteAllText(JSONListe.dateiPfad, jsonDataNewFinal.Text);
            // hier gehen wir zurück zur Darstellung aus dem DateiPfad
            this.Frame.Navigate(typeof(JSONListe), JSONListe.dateiPfad);
        }
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            //das geänderte JSON Objekt
            alteredJsonData = e.Parameter.ToString();

            if (Offnen.objektNameNummerListe.Count != 0) {   //Ist das Objekt verschachtelt?
                //Original JSON als Objekt
                JToken jsonDataOriginalObject = JToken.Parse(JSONListe.dataOriginal!);
                OriginalChangedObject = jsonDataOriginalObject;

                //gibt uns das original des geänderten Objekts
                foreach (var b in Offnen.objektNameNummerListe) {
                    if (b.All(char.IsDigit)) {
                        OriginalChangedObject = OriginalChangedObject[int.Parse(b)]!;
                    }
                    else {
                        OriginalChangedObject = OriginalChangedObject[b]!;
                    }
                }

                //das Objekt anhand seiner Adresse ersetzen
                JToken jsonDataNewToken = JToken.Parse(alteredJsonData!);
                //addressiere das original
                JToken OriginalJSONToken = JToken.Parse(JSONListe.dataOriginal!);

                //Objektadresse als int oder string
                dynamic adress0 = -1;
                dynamic adress1 = -1;
                dynamic adress2 = -1;
                dynamic adress3 = -1;
                dynamic adress4 = -1;

                if (Offnen.objektNameNummerListe.Count > 0 && Offnen.objektNameNummerListe[0] != null) {
                    if (Offnen.objektNameNummerListe[0].All(char.IsDigit)) {
                        adress0 = int.Parse(Offnen.objektNameNummerListe[0]);
                    }
                    else {
                        adress0 = Offnen.objektNameNummerListe[0];
                    }
                }
                if (Offnen.objektNameNummerListe.Count > 1 && Offnen.objektNameNummerListe[1] != null) {
                    if (Offnen.objektNameNummerListe[1].All(char.IsDigit)) {
                        adress1 = int.Parse(Offnen.objektNameNummerListe[1]);
                    }
                    else {
                        adress1 = Offnen.objektNameNummerListe[1];
                    }
                }
                if (Offnen.objektNameNummerListe.Count > 2 && Offnen.objektNameNummerListe[2] != null) {
                    if (Offnen.objektNameNummerListe[2].All(char.IsDigit)) {
                        adress2 = int.Parse(Offnen.objektNameNummerListe[2]);
                    }
                    else {
                        adress2 = Offnen.objektNameNummerListe[2];
                    }
                }
                if (Offnen.objektNameNummerListe.Count > 3 && Offnen.objektNameNummerListe[3] != null) {

                    if (Offnen.objektNameNummerListe[3].All(char.IsDigit)) {
                        adress3 = int.Parse(Offnen.objektNameNummerListe[3]);
                    }
                    else {
                        adress3 = Offnen.objektNameNummerListe[3];
                    }
                }
                if (Offnen.objektNameNummerListe.Count > 4 && Offnen.objektNameNummerListe[4] != null) {
                    if (Offnen.objektNameNummerListe[4].All(char.IsDigit)) {
                        adress4 = int.Parse(Offnen.objektNameNummerListe[4]);
                    }
                    else {
                        adress4 = Offnen.objektNameNummerListe[4];
                    }
                }

                //Original Objekt ersetzen, je nach Anzahl der Verschachtelungen
                switch (Offnen.objektNameNummerListe.Count) {
                    case 1:
                    OriginalJSONToken[adress0]!.Replace(jsonDataNewToken);
                    break;

                    case 2:
                    OriginalJSONToken[adress0]![adress1]!.Replace(jsonDataNewToken);
                    break;

                    case 3:
                    OriginalJSONToken[adress0]![adress1]![adress2]!.Replace(jsonDataNewToken);
                    break;

                    case 4:
                    OriginalJSONToken[adress0]![adress1]![adress2]![adress3]!.Replace(jsonDataNewToken);
                    break;

                    case 5:
                    OriginalJSONToken[adress0]![adress1]![adress2]![adress3]![adress4]!.Replace(jsonDataNewToken);
                    break;
                }

                //schreibt das geänderte Token in die neue, serialisierte JSON, zur Darstellung
                jsonDataNewFinal.Text = JsonConvert.SerializeObject(OriginalJSONToken, Formatting.Indented);

            }
            else {
                jsonDataNewFinal.Text = alteredJsonData;

            }

            base.OnNavigatedTo(e);
        }
    }
}


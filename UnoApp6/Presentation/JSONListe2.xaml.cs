using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Uno;

// C:\Users\gisela.wolf\Projekte\vmListe.json array
// C:\Users\gisela.wolf\Projekte\rahmenduebel.json objekte
// C:\Users\gisela.wolf\Projekte\UnoApp6-master\EinfachstesJSON.json
// C:\Users\gisela.wolf\Projekte\TestDatei.json

namespace UnoApp6.Presentation {
    public sealed partial class JSONListe2 : Page {
        //JSONListe.jsonDataOriginal
        //jsonData.Text (altered Object)
        //Offnen.objektNameNummerListe (adress of object)
        public static string? jsonData;
        public static JToken? OriginalChangedObject;
        public JSONListe2() {
            this.InitializeComponent();
        }

        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern), jsonData);
        }
        private void GoToOffnen(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateViewAsync<Offnen>(this, qualifier: Qualifiers.Dialog, jsonData);
        }
        private void GoToBestaetigen(object sender, RoutedEventArgs e) {
            if (Offnen.objektNameNummerListe.Count != 0) {

                JToken jsonDataOriginalObject = JToken.Parse(JSONListe.jsonDataOriginal!);  //Original JSON als Object
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
                JToken jsonDataNewToken = JToken.Parse(jsonData!);
                //address the original Token
                JToken OriginalJSONToken = JToken.Parse(JSONListe.jsonDataOriginal!); //<-- specify the token that must be changed

                dynamic adress0 = -1;
                dynamic adress1 = -1;
                dynamic adress2 = -1;
                dynamic adress3 = -1;
                dynamic adress4 = -1;

                if (Offnen.objektNameNummerListe[0] != null && Offnen.objektNameNummerListe.Count > 0) {
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

                jsonDataNewFinal.Text = JsonConvert.SerializeObject(OriginalJSONToken, Formatting.Indented);

            }
            else {
                jsonDataNewFinal.Text = jsonData;

            }

            File.WriteAllText(JSONListe.dateiPfad, jsonDataNewFinal.Text);          //<-- hier wird die gesamte JSON gespeichert

            this.Frame.Navigate(typeof(JSONListe), JSONListe.dateiPfad);



        } //<- die echte speicherfunktion, übergebe die gesamte JSON 

        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {

            jsonData = e.Parameter.ToString(); //this is the altered JSON Object
            base.OnNavigatedTo(e);


        }
        public static string ReplaceOfMyOwn(string oldJSON, string oldString, string newString) {

            string[] splitJSON = oldJSON.Split(oldString);
            string newJSON = splitJSON[0] + newString + splitJSON[1];

            return newJSON;
        }
    }
}



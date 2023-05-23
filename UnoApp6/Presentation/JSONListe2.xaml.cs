using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YamlDotNet.Serialization;

namespace UnoApp6.Presentation {
    public sealed partial class JSONListe2 : Page {
        //JSONListe.dataOriginal (original JSON)
        //jsonData.Text (geändertes Object)
        //Offnen.objektNameNummerListe (adress of object)
        public static string? alteredData;
        public static JToken? OriginalChangedObject;
        public JSONListe2() {
            this.InitializeComponent();
        }

        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern), alteredData);
        }
        private void GoToOffnen(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateViewAsync<Offnen>(this, qualifier: Qualifiers.Dialog, alteredData);
        }
        private void speichern(object sender, RoutedEventArgs e) {
            // hier wird die gesamte Datei gespeichert
            File.WriteAllText(JSONListe.dateiPfad, dataNewFinal.Text);
            //null the adress list
            Offnen.objektNameNummerListe.Clear();
            // hier gehen wir zurück zur Darstellung aus dem DateiPfad
            this.Frame.Navigate(typeof(JSONListe), JSONListe.dateiPfad);
        }
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            //das geänderte Objekt
            alteredData = e.Parameter.ToString();

            //JSON speichern
            if (!MainPage.fileIsYAML) {
                //Ist das Objekt verschachtelt? 
                if (Offnen.objektNameNummerListe.Count != 0) {
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
                    JToken jsonDataNewToken = JToken.Parse(alteredData!);
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
                    dataNewFinal.Text = JsonConvert.SerializeObject(OriginalJSONToken, Formatting.Indented);

                }
                else {
                    JToken jsonDataNewToken = JToken.Parse(alteredData!);
                    dataNewFinal.Text = JsonConvert.SerializeObject(jsonDataNewToken, Formatting.Indented);
                }
            }

            //YAML speichern
            else if (MainPage.fileIsYAML) {
                //Ist das Objekt verschachtelt?
                if (Offnen.objektNameNummerListe.Count != 0) {

                    var deserializer = new DeserializerBuilder().Build();
                    var OriginalChangedObject = deserializer.Deserialize<dynamic>(JSONListe.dataOriginal!);

                    //gibt uns das original des geänderten Objekts
                    foreach (var b in Offnen.objektNameNummerListe) {
                        OriginalChangedObject = OriginalChangedObject[b]!;
                    }

                    //gibt uns das geänderte Objekt
                    var alteredObject = deserializer.Deserialize<dynamic>(alteredData!);

                    //original Text als Objekt
                    var originalDataObject = deserializer.Deserialize<dynamic>(JSONListe.dataOriginal!);

                    //Objektadresse
                    dynamic adress0 = -1;
                    dynamic adress1 = -1;
                    dynamic adress2 = -1;
                    dynamic adress3 = -1;
                    dynamic adress4 = -1;
                    if (Offnen.objektNameNummerListe.Count > 0 && Offnen.objektNameNummerListe[0] != null) {
                        adress0 = Offnen.objektNameNummerListe[0];
                    }
                    if (Offnen.objektNameNummerListe.Count > 1 && Offnen.objektNameNummerListe[1] != null) {
                        adress1 = Offnen.objektNameNummerListe[1];
                    }
                    if (Offnen.objektNameNummerListe.Count > 2 && Offnen.objektNameNummerListe[2] != null) {
                        adress2 = int.Parse(Offnen.objektNameNummerListe[2]);
                    }
                    if (Offnen.objektNameNummerListe.Count > 3 && Offnen.objektNameNummerListe[3] != null) {
                        adress3 = int.Parse(Offnen.objektNameNummerListe[3]);
                    }
                    if (Offnen.objektNameNummerListe.Count > 4 && Offnen.objektNameNummerListe[4] != null) {
                        adress4 = int.Parse(Offnen.objektNameNummerListe[4]);

                    }

                    //Original Objekt ersetzen, je nach Anzahl der Verschachtelungen
                    switch (Offnen.objektNameNummerListe.Count) {
                        case 1:
                        originalDataObject.Remove(originalDataObject[adress0]!);
                        foreach (var item in alteredObject) {
                            originalDataObject[adress0]![item.Key] = item.Value;
                        }
                        break;

                        case 2:
                        originalDataObject.Remove(originalDataObject[adress0]![adress1]!);
                        foreach (var item in alteredObject) {
                            originalDataObject[adress0]![adress1]![item.Key] = item.Value;
                        }
                        break;

                        case 3:
                        originalDataObject.Remove(originalDataObject[adress0]![adress1]![adress2]!);
                        foreach (var item in alteredObject) {
                            originalDataObject[adress0]![adress1]![adress2]![item.Key] = item.Value;
                        }
                        break;

                        case 4:
                        originalDataObject.Remove(originalDataObject[adress0]![adress1]![adress2]![adress3]!);
                        foreach (var item in alteredObject) {
                            originalDataObject[adress0]![adress1]![adress2]![adress3]![item.Key] = item.Value;
                        }
                        break;

                        case 5:
                        originalDataObject.Remove(originalDataObject[adress0]![adress1]![adress2]![adress3]![adress4]!);
                        foreach (var item in alteredObject) {
                            originalDataObject[adress0]![adress1]![adress2]![adress3]![adress4]![item.Key] = item.Value;
                        }
                        break;
                    }

                    //schreibt das geänderte Token in die neue, serialisierte yaml zur Darstellung
                    var serializer = new SerializerBuilder().Build();
                    dataNewFinal.Text = serializer.Serialize(originalDataObject);

                }
                //das Objekt ist nicht verschachtelt
                else {
                    dataNewFinal.Text = alteredData;
                }
            }
            base.OnNavigatedTo(e);
        }
    }
}


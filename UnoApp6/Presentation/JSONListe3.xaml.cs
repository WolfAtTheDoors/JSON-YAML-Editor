using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.Management.Deployment.Preview;
using Windows.Security.Cryptography.Core;
// C:\Users\gisela.wolf\Projekte\vmListe.json objekte
// C:\Users\gisela.wolf\Projekte\rahmenduebel.json array
// C:\Users\gisela.wolf\Projekte\UnoApp6-master\EinfachstesJSON.json
//  C:\Users\gisela.wolf\Projekte\TestDatei.json

namespace UnoApp6.Presentation {
    public sealed partial class JSONListe3 : Page {
        public static string? jsonDataNewFinal;
        //JSONListe.jsonDataOriginal
        //jsonData.Text (altered Object)
        //Offnen.objektNameNummerListe (adress of object)

        public JSONListe3() {
            this.InitializeComponent();
        }

        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern), jsonData.Text);
        }
        private void GoToOffnen(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateViewAsync<Offnen>(this, qualifier: Qualifiers.Dialog, jsonData.Text);
        }

        private void GoToBestaetigen(object sender, RoutedEventArgs e) {
            //jsonDataNewFinal = jsonDataOriginalObject[Offnen.objektNameNummerListe[0]][Offnen.objektNameNummerListe[1]][Offnen.objektNameNummerListe[2]] + jsonData

            JToken jsonDataOriginalObject = JToken.Parse(JSONListe.jsonDataOriginal!);
            JToken OriginalChangedObject = jsonDataOriginalObject;

            //JToken OriginalChangedObject = jsonDataOriginalObject[Offnen.objektNameNummerListe[0]];         //<-- adresse des geänderten Objekts
            //ersetze das OriginalChangedObject mit dem jsonData.Text (geändertes Objekt)


            for (int i = 0; i <= Offnen.OpenObjectCounter; i++) {

                //Todo add if Integer or String parse int or not
                OriginalChangedObject = OriginalChangedObject[int.Parse(Offnen.objektNameNummerListe[i])]!;
                //gibt uns das original des geänderten Objekts
                //Wir brauchen die Adresse des originals, um es durch das geänderte objekt zu ersetzen

            }

            //Versuch Nummer 1: Die String-ersetzen Methode

            //stringify Original objekt
            String OriginalChangedObjectString = JsonConvert.SerializeObject(OriginalChangedObject, Formatting.Indented);

            //ersetze originalobjekt mit geändertem Objekt
            jsonDataNewFinal = JSONListe.jsonDataOriginal!.Replace(OriginalChangedObjectString, jsonData.Text);

            //Versuch Nummer 2: Finde einen Weg, das Objekt durch seine Adresse zu ersetzen (vermeidet unerwünschtes ersetzen)

            _ = this.Navigator()?.NavigateViewAsync<Bestaetigen>(this, qualifier: Qualifiers.Dialog, jsonDataNewFinal);


        } //<- die echte speicherfunktion, übergebe die gesamte JSON 
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            jsonData.Text = e.Parameter.ToString(); //this is the altered JSON Object

            base.OnNavigatedTo(e);
        }
    }
}




using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using System.Text.RegularExpressions;
using System.Linq;

namespace UnoApp6.Presentation {

    public sealed partial class Offnen : Page {
        public static JToken? jsonObject;
        public static JToken? jsonObjectDesired;
        public static string? jsonData = "default";

        public static List<string> objektNameNummerListe = new List<string>();

        public static int OpenObjectCounter = -1;

        public Offnen() {
            this.InitializeComponent();
        }

        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        private void GoToJSONListe2(object sender, RoutedEventArgs e) {

            //deserialize
            jsonObject = JToken.Parse(jsonData!);

            //input is Arraynumber or Objektname. Add to Liste, count openobjectcounter. This list provides the adress later for saving to the entire JSON
            objektNameNummerListe.Add(objektNummerName.Text);
            OpenObjectCounter++;

            //array
            if (jsonObject is JArray) {                                                                                 //Wenn das Objekt ein Array ist,
                var jsonObjectArray = jsonObject as JArray;
                if (objektNameNummerListe[OpenObjectCounter].All(char.IsDigit)) {                                       //muss es mit einem integer angesprochen werden,
                    if (int.Parse(objektNameNummerListe[OpenObjectCounter]) <= ((int)jsonObjectArray!.Count) - 1) {     //der kleiner gleich der Anzahl der Objekte im Array ist
                        jsonObjectDesired = jsonObjectArray[int.Parse(objektNameNummerListe[OpenObjectCounter])];
                    }
                    else {
                        jsonObjectDesired = "So viele Objekte hat das Array nicht.";
                    }
                }
                else {
                    jsonObjectDesired = "Dieses Objekt ist ein Array und muss mit einer Nummer aufgerufen werden";
                }
            }

            //object
            else if (jsonObject is JObject) {
                jsonObjectDesired = jsonObject[objektNameNummerListe[OpenObjectCounter]];
            }

            //all else
            else {
                jsonObjectDesired = jsonObject;
            }

            //serialize
            if (!(jsonObjectDesired is null)) {
                jsonData = JsonConvert.SerializeObject(jsonObjectDesired, Formatting.Indented);
            }
            else {
                jsonData = "Bitte das Objekt mit einer Nummer (im Array) oder mit dem Namen (im Objekt) aufrufen.";
            }

            this.Frame.Navigate(typeof(JSONListe2), jsonData);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            jsonData = e.Parameter.ToString();

            base.OnNavigatedTo(e);

        }
    }
}


// C:\Users\gisela.wolf\Projekte\vmListe.json array
// C:\Users\gisela.wolf\Projekte\rahmenduebel.json objekte
// C:\Users\gisela.wolf\Projekte\UnoApp6-master\EinfachstesJSON.json
//  C:\Users\gisela.wolf\Projekte\TestDatei.json
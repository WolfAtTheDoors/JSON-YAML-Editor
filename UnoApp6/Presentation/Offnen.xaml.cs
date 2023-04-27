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

        public Offnen() {
            this.InitializeComponent();
        }

        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern), objektNummerName.Text);
        }
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        private void GoToJSONListe2(object sender, RoutedEventArgs e) {
            int objektNummerInt = 0;
            string objektNameString = "";

            //input is arraynumber or objectname
            if (objektNummerName.Text.All(char.IsDigit)) {
                objektNummerInt = int.Parse(objektNummerName.Text);
            }
            else {
                objektNameString = objektNummerName.Text;
            }

            //deserialize
            jsonObject = JToken.Parse(jsonData!);

            //array
            if (jsonObject is JArray) {
                var jsonObject2 = jsonObject as JArray;

                if (objektNummerInt <= ((int)jsonObject2!.Count) - 1) {
                    jsonObjectDesired = jsonObject[objektNummerInt];
                }
                else {
                    jsonObjectDesired = "So viele Objekte hat das Array nicht.";
                }
            }
            //object
            else if (jsonObject is JObject) {
                jsonObjectDesired = jsonObject[objektNameString];
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
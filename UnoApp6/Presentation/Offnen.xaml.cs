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

namespace UnoApp6.Presentation {

    public sealed partial class Offnen : Page {
        public static JToken? jsonObject;
        public static JToken? jsonObjectDesired;
        public static string? jsonData = "default";

        public Offnen() {
            this.InitializeComponent();
        }

        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern), objektNummer);
        }
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        private void GoToJSONListe2(object sender, RoutedEventArgs e) {
            int objektNummerInt = 0;

            if (objektNummer != null) {
                objektNummerInt = int.Parse(objektNummer.Text);
            }
            else {
                jsonData = "Bitte gib eine richtige Objektnummer ein";
            }

            //grab the correct object (objectNummerInt from jsonData) and pass it back to JSONListe
            //JContainer. beliebiger typ
            jsonObject = JToken.Parse(jsonData!);

            //array
            if (jsonData!.Contains("[")) {
                jsonObjectDesired = jsonObject[objektNummerInt];
            }

            //object
            else if (jsonData!.Contains("{")) {
                jsonObjectDesired = jsonObject[objektNummerInt];
            }

            //all else
            else {
                jsonObjectDesired = jsonObject;
            }

            jsonData = JsonConvert.SerializeObject(jsonObjectDesired, Formatting.Indented);
            this.Frame.Navigate(typeof(JSONListe2), jsonData);
        }


        protected override void OnNavigatedTo(NavigationEventArgs e) {

            jsonData = e.Parameter.ToString();
            base.OnNavigatedTo(e);

        }
    }
}

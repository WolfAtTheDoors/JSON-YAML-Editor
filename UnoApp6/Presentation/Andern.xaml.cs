using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Nodes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Net.Mime.MediaTypeNames;

namespace UnoApp6.Presentation {

    public sealed partial class Andern : Page {
        public Andern() {
            this.InitializeComponent();
        }

        private void GoToJSONListe(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(JSONListe));
        }

        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }

        private void GoToBestaetigen(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Bestaetigen));
        }

        private void Ubernehmen(object sender, RoutedEventArgs e) {

            //eingegebene Werte
            //string a = alterName.Text;
            //string b = neuerName.Text;
            //string c = neuerWert.Text;

            //Deserialize
            JObject jsonObject = JObject.Parse(jsonData.Text);

            //change Wert to neuer Wert
            jsonObject[alterName.Text] = neuerWert.Text;

            //Namen ändern

            //void renameKey(object jsonObject, string alterName, string neuerName) {
            //    jsonObject[neuerName.Text] = jsonObject[alterName.Text];
            //    jsonObject[alterName.Text].Remove();
            //}

            //if (neuerName.Text is string && !string.IsNullOrWhiteSpace(neuerName.Text)) {

            //    renameKey(jsonObject, alterName.Text, neuerName.Text);

            //}

            //ganze eigenschaft entfernen


            //Serialise for display
            jsonData.Text = jsonObject.ToString();
            jsonData.Text.Replace(alterName.Text, neuerName.Text);


            //in neuer Datei speichern
            //in gleicher Datei speichern

            this.Frame.Navigate(typeof(Andern), jsonData.Text);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter)) {
                jsonData.Text = e.Parameter.ToString();

            }
            else {
                jsonData.Text = "Bitte gib einen gültigen Dateipfad ein!";
            }
            base.OnNavigatedTo(e);
        }

    }
}

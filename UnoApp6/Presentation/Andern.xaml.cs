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
        //in neuer Datei speichern
        //in gleicher Datei speichern

        private void Ubernehmen(object sender, RoutedEventArgs e) {

            //eingegebene Werte
            //string a = alterName.Text;
            //string b = neuerName.Text;
            //string c = neuerWert.Text;

            // S:\Austausch\gisela\vmListe.json chaos
            // S:\Austausch\gisela\rahmenduebel.json ordnung
            // C:\Users\gisela.wolf\JSON Editor\EinfachstesJSON.json

            //Deserialize
            JObject jsonObject = JObject.Parse(jsonData.Text);

            //Wert zu neuer Wert ändern
            if (neuerWert.Text is string && !string.IsNullOrWhiteSpace(neuerWert.Text)) {
                jsonObject[alterName.Text] = neuerWert.Text;
            }
            //ganze Eigenschaft entfernen



            //Kopieren und Einfügen


            //Namen ändern und zurück serialisieren

            jsonData.Text = JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
            if (neuerName.Text is string && !string.IsNullOrWhiteSpace(neuerName.Text)) {
                jsonData.Text = jsonData.Text.Replace(alterName.Text, neuerName.Text);
            }



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

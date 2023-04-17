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
using Uno;
using Windows.ApplicationModel.Email.DataProvider;
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

            // S:\Austausch\gisela\vmListe.json chaos
            // S:\Austausch\gisela\rahmenduebel.json ordnung
            // C:\Users\gisela.wolf\JSON Editor\EinfachstesJSON.json

            //Deserialize
            JObject jsonObject = JObject.Parse(jsonData.Text);


            //eigenschaft entfernen !
            if ((!string.IsNullOrEmpty(alterName.Text)) && string.IsNullOrEmpty(neuerName.Text) && string.IsNullOrEmpty(neuerWert.Text)) {
                jsonObject.Remove(alterName.Text);
            }



            //eigenschaft hinzufügen (name) X
            if (string.IsNullOrEmpty(alterName.Text)) {
                jsonObject[neuerName.Text] = neuerWert.Text;
            }


            //namen ersetzen !
            if ((!string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text)) && string.IsNullOrEmpty(neuerWert.Text)) {
                jsonData.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                jsonData.Text = jsonData.Text.Replace(alterName.Text, neuerName.Text);
                jsonObject = JObject.Parse(jsonData.Text);
            }

            //Wert ersetzen !
            if ((!string.IsNullOrEmpty(alterName.Text)) && string.IsNullOrEmpty(neuerName.Text) && (!string.IsNullOrEmpty(neuerWert.Text))) {
                jsonObject[alterName.Text] = neuerWert.Text;
            }

            //alles ändern !
            if (!string.IsNullOrEmpty(alterName.Text) && !string.IsNullOrEmpty(neuerName.Text) && !string.IsNullOrEmpty(neuerWert.Text)) {
                jsonObject[alterName.Text] = neuerWert.Text;
                jsonData.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                jsonData.Text = jsonData.Text.Replace(alterName.Text, neuerName.Text);
                jsonObject = JObject.Parse(jsonData.Text);
            }

            /*
            
                jsonObject[alterName.Text] = neuerWert.Text;


                jsonData.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                jsonData.Text = jsonData.Text.Replace(alterName.Text, neuerName.Text);
                JObject jsonObject = JObject.Parse(jsonData.Text);

            */

            //Kopieren und Einfügen

            //zurück serialisieren
            jsonData.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);



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

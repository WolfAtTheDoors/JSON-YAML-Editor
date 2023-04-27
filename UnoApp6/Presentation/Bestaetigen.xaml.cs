using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace UnoApp6.Presentation {

    public sealed partial class Bestaetigen : Page {
        public Bestaetigen() {
            this.InitializeComponent();
        }

        //JSONListe.jsonDataOriginal;  keeps the original file in order to add the changes to it

        private void GoToJSONListe(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(JSONListe));
        }
        private void speichern(object sender, RoutedEventArgs e) {

            File.WriteAllText(JSONListe.dateiPfad, jsonData.Text);

            _ = this.Navigator()?.NavigateBackAsync(this);

            //@"C:\Users\gisela.wolf\Projekte\TestDatei.json"
        }
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {

            jsonData.Text = e.Parameter.ToString();

            base.OnNavigatedTo(e);
        }

    }
}

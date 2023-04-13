using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace UnoApp6.Presentation {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnoApp6.Presentation {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Offnen : Page {
        public Offnen() {
            this.InitializeComponent();
        }

        private void GoToJSONListe(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(JSONListe), objektNummer);

        }

        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }

        private void GoToMainPage(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;

namespace UnoApp6.Presentation {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Offnen : Page {
        public Offnen() {
            this.InitializeComponent();
        }

        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern), objektNummer);
        }
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            //string test1! = e.Parameter.ToString();
            string test = objektNummer.Text;

            //attempt to access the requested object using objektNummer.jsonData or something like that
            //e is the formatted json from JSONListe


            base.OnNavigatedTo(e);
        }

    }
}

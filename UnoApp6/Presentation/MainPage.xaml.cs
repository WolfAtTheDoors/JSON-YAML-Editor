using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using System.Xml.Linq;
using Windows.Devices.Enumeration;

namespace UnoApp6.Presentation {

    public sealed partial class MainPage : Page {

        public MainPage() {
            this.InitializeComponent();

        }

        private void GoToJSONListe(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(JSONListe), dateiName.Text);

        }
    }
}
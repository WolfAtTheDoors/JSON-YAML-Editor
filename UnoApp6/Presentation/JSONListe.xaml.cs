using System.Text;
using System.Text.Json;

namespace UnoApp6.Presentation {
    public sealed partial class JSONListe : Page {

        public JSONListe() {
            InitializeComponent();

        }

        private void GoToMainPage(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(MainPage));
        }
        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern), dateiName.Text);
        }
        private void GoToBestaetigen(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Bestaetigen));
        }
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter)) {

                dateiName.Text = e.Parameter.ToString();
                jsonData.Text = File.ReadAllText(dateiName.Text!);

            }
            else {
                dateiName.Text = "Bitte gib einen gültigen Dateipfad ein!";
            }
            base.OnNavigatedTo(e);
        }

        // S:\Austausch\gisela\vmListe.json chaos
        // S:\Austausch\gisela\rahmenduebel.json ordnung
        // display the json content
    }


    class JsonHelper {
        private const string INDENT_STRING = "    ";

        public static string FormatJson(string jsonData) {

            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();

            for (var i = 0; i < jsonData.Length; i++) {
                var ch = jsonData[i];

                switch (ch) {

                    case '{':
                    case '[':
                    sb.Append(ch);

                    if (!quoted) {
                        sb.AppendLine();
                        Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
                    }
                    break;

                    case '}':
                    case ']':
                    if (!quoted) {
                        sb.AppendLine();
                        Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
                    }
                    sb.Append(ch);
                    break;

                    case '"':
                    sb.Append(ch);
                    bool escaped = false;
                    var index = i;
                    while (index > 0 && jsonData[--index] == '\\')
                        escaped = !escaped;
                    if (!escaped)
                        quoted = !quoted;
                    break;

                    case ',':
                    sb.Append(ch);
                    if (!quoted) {
                        sb.AppendLine();
                        Enumerable.Range(0, indent).ForEach(item => sb.Append(INDENT_STRING));
                    }
                    break;

                    case ':':
                    sb.Append(ch);
                    if (!quoted)
                        sb.Append(" ");
                    break;

                    default:
                    sb.Append(ch);
                    break;
                }
            }
            return sb.ToString();
        }
    }

    static class Extensions {
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action) {
            foreach (var i in ie) {
                action(i);
            }
        }
    }

}

//JSONListe stellt immer nur die ganze JSON dar. Alle unterobjekte sind immer in JSONListe2
// C:\Users\gisela.wolf\Projekte\vmListe.json array
// C:\Users\gisela.wolf\Projekte\rahmenduebel.json objekte
// C:\Users\gisela.wolf\Projekte\UnoApp6-master\EinfachstesJSON.json
//  C:\Users\gisela.wolf\Projekte\TestDatei.json

namespace UnoApp6.Presentation {
    public sealed partial class JSONListe : Page {
        public static string dateiPfad = "C:\\Users\\gisela.wolf\\Projekte\\default.json";
        public static string? jsonDataOriginal;  //keeps the original file in order to add the changes to it

        public JSONListe() {
            this.InitializeComponent();
        }

        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern), jsonData.Text);
        }
        private void GoToOffnen(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateViewAsync<Offnen>(this, qualifier: Qualifiers.Dialog, jsonData.Text);
            //this.Frame.Navigate(typeof(Offnen), jsonData.Text);
        }
        private void GoToBestaetigen(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateViewAsync<JSONListe3>(this, qualifier: Qualifiers.Dialog, jsonData.Text);

        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter)) {

                dateiName.Text = e.Parameter.ToString();
                jsonData.Text = File.ReadAllText(dateiName.Text!);
                jsonDataOriginal = File.ReadAllText(dateiName.Text!);

                if (dateiName.Text != null) {
                    dateiPfad = dateiName.Text;
                }
            }
            else {
                dateiName.Text = "Bitte gib einen gültigen Dateipfad ein!";
            }

            base.OnNavigatedTo(e);
        }
    }

    class JsonHelper {
        private const string INDENT_STRING = "    ";
        private const string INTEGER_STRING = "0123456789";

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
                    sb.AppendLine();
                    Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
                    break;

                    case '}':
                    case ']':
                    sb.AppendLine();
                    Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
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
                    if (!INTEGER_STRING.Contains(jsonData[i + 1])) {
                        sb.AppendLine();
                        Enumerable.Range(0, indent).ForEach(item => sb.Append(INDENT_STRING));
                    }
                    break;

                    case ':':
                    sb.Append(ch);
                    if (!quoted)
                        sb.Append(" ");
                    break;

                    case '\\':
                    if (jsonData[i + 1].Equals("\"")) {
                        sb.Replace('\\', ' ');
                    }
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








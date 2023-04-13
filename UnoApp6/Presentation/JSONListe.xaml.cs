using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json;
using Windows.Management.Deployment.Preview;

namespace UnoApp6.Presentation {
    public sealed partial class JSONListe : Page {

        public JSONListe() {
            this.InitializeComponent();

        }

        private void GoToMainPage(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(MainPage));
        }
        private void GoToAndern(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Andern), jsonData.Text);
        }
        private void GoToOffnen(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Offnen), jsonData.Text);
        }
        private void GoBack(object sender, RoutedEventArgs e) {
            _ = this.Navigator()?.NavigateBackAsync(this);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {

            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter)) {

                dateiName.Text = e.Parameter.ToString();
                jsonData.Text = JsonHelper.FormatJson(File.ReadAllText(dateiName.Text!));

            }

            else {
                dateiName.Text = "Bitte gib einen gültigen Dateipfad ein!";
            }
            base.OnNavigatedTo(e);
        }

        // S:\Austausch\gisela\vmListe.json chaos
        // S:\Austausch\gisela\rahmenduebel.json ordnung
        // BeispielJSON

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



namespace BeispielJSON {
    public class Studenten {
        public string? nachname { get; set; }

    }
    public class Universitäten {
        public string? universität { get; set; }

        public IList<Studenten>? studenten { get; set; }

    }
    public class KlassenUnis {

        public Universitäten? universitäten { get; set; }

    }

    class Program {

        static void UniProgram() {

            KlassenUnis KlassenUni1 = new KlassenUnis();

            KlassenUni1.universitäten = new Universitäten();
            KlassenUni1.universitäten.universität = "KIT";

            List<Studenten> studenten = new List<Studenten>();
            Studenten studenten1 = new Studenten {
                nachname = "Wolf"
            };
            Studenten studenten2 = new Studenten {
                nachname = "Schmidt"
            };
            Studenten studenten3 = new Studenten {
                nachname = "Sailer"
            };

            studenten.Add(studenten1);
            studenten.Add(studenten2);
            studenten.Add(studenten3);

            string BeispielJSON = JsonConvert.SerializeObject(KlassenUni1);


        }
    }

}







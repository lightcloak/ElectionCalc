using LiteDB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ElectionCalc
{
    /// <summary>
    /// Interaction logic for Summary.xaml
    /// </summary>
    public partial class Summary : Window
    {
        private readonly string candidatesUrl = "http://webtask.future-processing.com:8069/candidates";
        private Window parent;
        private bool exit = false;

        public Summary(Window input)
        {
            parent = input;
            InitializeComponent();
            setUpUI();
        }

        private void setUpUI()
        {
            using (var db = new LiteDatabase(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                // Dodanie glosu
                var candidates = db.GetCollection<Candidate>("candidatesVotes");

                var votesTotal = candidates.Find(x => x.Name != "void");
                var votesVoid = candidates.Find(x => x.Name == "void");

                double tvotes;
                double ttotal = votesTotal.Count();
                double lwidth = List.Width;

                txtBlk.Text += "\nSo far there were: " + votesTotal.Count() + " votes added, " +
                               "not including: " + votesVoid.Count() + " votes that were void.";

                // Koniecznosc wyprowadzenia kodu obslugujacego warstwe UI do osobnej metody/klasy
                foreach (Candidate person in getCandidates(candidatesUrl))
                {
                    Label candidate = new Label();
                    candidate.Content = person.Name + " " + " (" + person.Party + ")";

                    string[] name = person.Name.Split(' ');
                    var votes = candidates.Find(x => x.Name == name[0] && x.Surname == name[1]);
                    tvotes = votes.Count();

                    candidate.Content += ": " + votes.Count();
                    candidate.Margin = new Thickness(10, 0, 10, 0);

                    Rectangle rec = new Rectangle();
                    rec.Height = 10;

                    // Zmiany w celu zaprezentowania prostej wizualizacji wykresu
                    double width = lwidth * (tvotes / ttotal);
                    if (width > List.Width) width = 0;

                    // pominiecie formatowanie .ToString(P2) i String.Format
                    rec.ToolTip = ((width / lwidth * 100) + "%").ToString();

                    rec.Width = width + 10;
                    rec.Fill = new SolidColorBrush(System.Windows.Media.Colors.BlueViolet);
                    rec.HorizontalAlignment = HorizontalAlignment.Left;

                    List.Children.Add(candidate);
                    List.Children.Add(rec);
                }
            }
        }
        // Redundancja kodu
        private IList<Candidate> getCandidates(string url)
        {
            var candidatesJson = WebTools.JSONFromUrl(url);

            IList<JToken> results = candidatesJson["candidates"]["candidate"].Children().ToList();
            IList<Candidate> candidates = new List<Candidate>();

            foreach (JToken item in results)
            {
                Candidate person = JsonConvert.DeserializeObject<Candidate>(item.ToString());
                candidates.Add(person);
            }

            return candidates;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Przy wyjsciu z aplikacji upewnij sie ze ukryte okna zostana obsluzone
            if (!exit) parent.Visibility = Visibility.Visible;
        }
    }
}
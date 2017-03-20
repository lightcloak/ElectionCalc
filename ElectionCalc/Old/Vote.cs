using LiteDB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ElectionCalc
{
    /// <summary>
    /// Interaction logic for Vote.xaml
    /// </summary>
    public partial class Vote : Window
    {
        private readonly string candidatesUrl = "http://webtask.future-processing.com:8069/candidates";
        private Window parent;
        private bool exit = false;

        public Vote(Window input)
        {
            parent = input;

            InitializeComponent();
            setUpUI();
        }

        // ======================================================================
        // Rozbic na odpowiednia metode - farbyka obiektow
        // Obiekt nie jest przechowywany w pamieci - nie optymalne rozwiazanie
        // w przypadku czestego przelaczania miedzy oknami
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

        private void setUpUI()
        {
            txtBlk.Text += " " + WorkWindow.loggedPerson.Name + " " +
                           WorkWindow.loggedPerson.Surname + ". " +
                           "Cast Your Vote for only one candidate. If You don't select or select more than one candidate, " +
                           "Your vote will void and you won't be able to vote again.";

            foreach (Candidate person in getCandidates(candidatesUrl))
            {
                CheckBox candidate = new CheckBox();
                candidate.Content = person.Name + " " + person.Surname + " (" + person.Party + ")";
                candidate.Margin = new Thickness(10, 10, 10, 10);
                List.Children.Add(candidate);
            }
        }

        private void castVote(string person)
        {
            string name, surname, party;

            if (person != "void")
            {
                // Interesuje nas imie i nazwisko kandydata wiec pobieramy te dwie informacje
                string[] cridentials = Regex.Replace(person, @"\s+", " ")
                                            .Split(' ')
                                            .Where((item, index) => index < 2)
                                            .ToArray();

                name = cridentials[0];
                surname = cridentials[1];
                party = person.Split('(', ')')[1];
            }
            else
            {
                name = surname = party = person;
            }

            // Do zaimplementowania zabezpieczenie przechowywanego hasla
            using (var db = new LiteDatabase(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                // Dodanie glosu
                var candidates = db.GetCollection<Candidate>("candidatesVotes");

                using (var trans = db.BeginTrans())
                {
                    candidates.Insert(new Candidate
                    {
                        Name = name,
                        Surname = surname,
                        Party = party,
                        VotingCitizen = WorkWindow.loggedPerson.Pesel
                    });

                    trans.Commit();
                }

                // Aktualizacja listy glosujacych
                // Nie rozbito na odpowiednia postac znormalizowana z powodu wydajnosci LiteDB
                var voted = db.GetCollection<Voter>("peopleVoted");

                using (var trans = db.BeginTrans())
                {
                    voted.Insert(new Voter
                    {
                        Name = WorkWindow.loggedPerson.Name,
                        Surname = WorkWindow.loggedPerson.Surname,
                        Pesel = WorkWindow.loggedPerson.Pesel
                    });

                    trans.Commit();
                }
                // Nie eleganckie pominiecie wykonania zdarzenia przywracajacego Visibility
                // Koniecznosc implementacji setUpUI w oknie WorkWindow w celu pominienia
                // wyjscia z aplikacji, ktore mozna zreszta zastopowac i oddac kolejny glos
                exit = true;
                Application.Current.Shutdown();
            }
        }

        // ======================================================================
        // Akcje
        // ======================================================================

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Przy wyjsciu z aplikacji upewnij sie ze ukryte okna zostana obsluzone
            if (!exit) parent.Visibility = Visibility.Visible;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnVote_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Cast Your Vote", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                // Pobranie zaznaczonych elementow
                // wraz z obsluga nullable przez ??
                List<string> selectedValues = List.Children.Cast<CheckBox>()
                    .Where(x => x.IsChecked ?? false)
                    .Select(x => x.Content.ToString())
                    .ToList();

                switch (selectedValues.Count)
                {
                    case 0:
                        MessageBox.Show("You haven't selected a candidate from the list. Your vote is void.");
                        castVote("void");
                        break;
                    case 1:
                        MessageBox.Show("You've casted your vote. Candidate voted for: " + selectedValues.FirstOrDefault() +
                                        "\nAfter processing your vote, this program will exit." +
                                        "\nThank You.");
                        castVote(selectedValues.FirstOrDefault());
                        break;
                    default:
                        MessageBox.Show("You've selected more than one candidate. Your vote is void.");
                        castVote("void");
                        break;
                }
            }
        }
    }
}
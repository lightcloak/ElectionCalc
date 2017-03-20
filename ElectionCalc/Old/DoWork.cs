using LiteDB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System;

namespace ElectionCalc
{
    /// <summary>
    /// Interaction logic for WorkWindow.xaml
    /// </summary>
    public partial class WorkWindow : Window, IDisposable
    {
        private readonly string blockedUrl = "http://webtask.future-processing.com:8069/blocked";

        public static Voter loggedPerson { get; private set; }
        private bool voted = false;
        private bool blocked = false;
        private bool closing = false;

        private bool IsDisposed = false;

        // Brak kontroli kolejnych instancji
        public WorkWindow(Voter person)
        {
            // Wykona sie przed ContentRendered
            loggedPerson = person;

            InitializeComponent();

            setUpUI();
        }

        public void setUpUI()
        {
            if (closing) return;

            // Konieczne jest sprawdzenie uprawnien
            checkPrivlidges(loggedPerson);

            // Labelka z powitaniem
            txtBlk.Text = "Welcome " + loggedPerson.Name + " " + loggedPerson.Surname;

            // Redundancja w kodzie - do poprawy
            if (blocked)
            {
                btnCast.IsEnabled = false;
                txtBlk.Text += ". Although you can't cast your vote, You can check the current votes and export them to different formats.";
            }
            else if (voted)
            {
                btnCast.IsEnabled = false;
                txtBlk.Text += ". You've already voted. You can check the current votes and export them to different formats.";
            }
            else
            {
                btnCast.IsEnabled = true;
                txtBlk.Text += ". You can cast your vote or You can check the current votes and export them to different formats.";
            }
        }

        private void checkPrivlidges(Voter person)
        {
            // Do zaimplementowania zabezpieczenie przechowywanego hasla
            using (var db = new LiteDatabase(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                // Tworzy lub pobiera tabele przechowywana w bazie
                var peopleVoted = db.GetCollection<Voter>("peopleVoted");

                // Zapytanie lamda wyciagajace rekordy z numerami pesel
                var votedPool = peopleVoted.Find(x => x.Pesel.Equals(person.Pesel));
                bool isBlocked = getBlocked(blockedUrl).Any(x => x.Pesel == loggedPerson.Pesel);

                if (votedPool.Count() > 0) voted = true;
                else voted = false;

                if (isBlocked) blocked = true;
                else blocked = false;
            }
        }

        // ======================================================================
        // Rozbic na odpowiednia metode - farbyka obiektow
        private IList<Voter> getBlocked(string url)
        {
            var blockedJson = WebTools.JSONFromUrl(url);

            IList<JToken> results = blockedJson["disallowed"]["person"].Children().ToList();
            IList<Voter> blocked = new List<Voter>();

            foreach (JToken item in results)
            {
                Voter person = JsonConvert.DeserializeObject<Voter>(item.ToString());
                blocked.Add(person);
            }

            return blocked;
        }

        // ======================================================================
        // Akcje
        // ======================================================================

        private void btnCast_Click(object sender, RoutedEventArgs e)
        {
            Vote vote = new Vote(this.SecondWindow);
            this.Visibility = Visibility.Collapsed;
            vote.Show();
        }

        private void btnSummary_Click(object sender, RoutedEventArgs e)
        {
            Summary summary = new Summary(this.SecondWindow);
            this.Visibility = Visibility.Collapsed;
            summary.Show();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            Dispose();
            MainWindow window = new MainWindow();
            window.Show();

            this.Close();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool Disposing)
        {
            if (!IsDisposed)
            {
                if (Disposing)
                { // zwalniaj zasoby zarządzalne }
                  // zwalniaj zasoby niezarządzalne
                    loggedPerson = null;
                    voted = false;
                    blocked = false;
                }
                IsDisposed = true;
            }
        }

        ~WorkWindow()
        {
            Dispose(false);
        }
    }

}
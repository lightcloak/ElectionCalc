namespace ElectionCalc
{
    using System.Windows;
    using ElectionCalc.ViewModels;
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        //public void logIn(string name, string surname, string pesel)
        //{
        //    var loggedPerson = new Voter
        //    {
        //        Name = name,
        //        Surname = surname,
        //        Pesel = Convert.ToInt64(pesel)
        //    };

        //    WorkWindow window2 = new WorkWindow(loggedPerson);
        //    window2.Show();
        //    this.Close();
        //}

        //// ======================================================================
        //// Akcje
        //// ======================================================================

        //private void tboxName_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (tboxName.Text == nameCTA)
        //    {
        //        tboxName.Text = string.Empty;
        //    }
        //}

        //private void tboxName_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if(string.IsNullOrWhiteSpace(tboxName.Text))
        //    {
        //        tboxName.Text = nameCTA;
        //    }
        //}

        //private void tboxSurname_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if(tboxSurname.Text == surnameCTA)
        //    {
        //        tboxSurname.Text = string.Empty;
        //    }
        //}

        //private void tboxSurname_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(tboxSurname.Text))
        //    {
        //        tboxSurname.Text = surnameCTA;
        //    }
        //}

        //private void tboxPesel_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (tboxPesel.Text == peselCTA)
        //    {
        //        tboxPesel.Text = string.Empty;
        //    }
        //}

        //private void tboxPesel_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(tboxPesel.Text))
        //    {
        //        tboxPesel.Text = peselCTA;
        //    }
        //}

        //private void tboxPesel_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    // Ograniczenie wartosci jakie moga byc wprowadzone w pole
        //    // Inny sposob: IsDigit
        //    // ((e.Key >= Key.A && e.Key <= Key.Z) to litery
        //    if ((e.Key >= Key.D0 && e.Key <= Key.D9) ||
        //        (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
        //         e.Key == Key.Back ||
        //         e.Key == Key.Delete ||
        //         e.Key == Key.Enter)
        //    {
        //        if(e.Key == Key.Enter)
        //        {
        //            btnLogIn_Click(null, null);
        //        }
        //        else
        //            return;
        //    }
        //    else
        //    {
        //        e.Handled = true;
        //    }
        //}

        //private void btnLogIn_Click(object sender, RoutedEventArgs e)
        //{
        //    // Do zaimplementowania metoda prezentacji pol, ktore nie przeszly walidacji
        //    // https://mndevnotes.wordpress.com/2012/12/01/wpf-walidacja-danych/

        //    // Blokada na czas przetwarzania pola
        //    tboxName.IsEnabled = false;
        //    tboxSurname.IsEnabled = false;
        //    tboxPesel.IsEnabled = false;

        //    string name = tboxName.Text;
        //    string surname = tboxSurname.Text;
        //    string pesel = tboxPesel.Text;

        //    if (!string.IsNullOrWhiteSpace(name) &&
        //                         name != nameCTA &&

        //        !string.IsNullOrWhiteSpace(surname) &&
        //                      surname != surnameCTA &&

        //        !string.IsNullOrWhiteSpace(pesel) &&
        //                        pesel != peselCTA &&

        //         Validators.IsValidPESEL(pesel) &&
        //         Validators.IsOldEnough(pesel))
        //    {
        //        // przekazanie parametrow w troche mniej popularny sposob
        //        logIn(name: name, surname: surname, pesel: pesel);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please check input fields for errors." +
        //                        "\nBelow are some common problems:" +
        //                        "\n\nName, Surname and Pesel has to be filled in." +
        //                        "\nProvided Pesel number has to be valid." +
        //                        "\nYou have to be at least 18 years old in order to Log in.",
        //                        "We've detected some problems");
        //        // Zwolnienie blokady
        //        tboxName.IsEnabled = true;
        //        tboxSurname.IsEnabled = true;
        //        tboxPesel.IsEnabled = true;
        //    }
        //}

        private void ControlButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ControlViewModel();
        }

        private void VoteButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new VoteViewModel();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new LogInViewModel();
        }
    }
}

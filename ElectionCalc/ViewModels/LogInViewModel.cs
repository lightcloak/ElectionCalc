namespace ElectionCalc.ViewModels
{
    using Models;
    using System.Windows;

    class LogInViewModel : ObservableObject
    {
        public Voter Voter { get; private set; }

        public LogInViewModel()
        {
            Voter = new Voter();
        }

        public void LogIn()
        {
            MessageBox.Show(Voter.Name + " " + Voter.Surname + " " + Voter.Pesel);
        }
    }
}

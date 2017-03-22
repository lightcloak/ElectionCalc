namespace ElectionCalc.ViewModels
{
    using Commands;
    using Models;
    using System.Windows;

    public class LogInViewModel : ObservableObject
    {
        public Voter Voter { get; private set; }
        public PerformCommand LogInCommand { get; private set; }

        public LogInViewModel()
        {
            Voter = new Voter();
            LogInCommand = new PerformCommand(LogIn);
        }

        public void LogIn()
        {
            if (PeselValidationTools.OldEnough(Voter.Pesel))
            {
                MessageBox.Show(Voter.Name + " " + Voter.Surname + " " + Voter.Pesel);
            };
        }
    }
}

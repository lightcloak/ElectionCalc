namespace ElectionCalc.ViewModels
{
    using Models;

    class LogInViewModel : ObservableObject
    {
        public Voter Voter { get; private set; }

        public LogInViewModel()
        {
            Voter = new Voter();
        }
    }
}

namespace ElectionCalc.Views
{
    using System.Windows.Controls;
    using ViewModels;
    /// <summary>
    /// Interaction logic for LogInView.xaml
    /// </summary>
    public partial class LogInView : UserControl
    {
        private LogInViewModel viewModel;

        public LogInView()
        {
            InitializeComponent();
            viewModel = new LogInViewModel();
            this.DataContext = viewModel;
        }

        private void btnLogIn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            viewModel.LogIn();
        }
    }
}

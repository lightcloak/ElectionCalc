namespace ElectionCalc.ViewModels
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    class LogInViewModel
    {
        private ICommand doSomethingCommand = new MyCommand();

        public ICommand DoSomethingCommand
        {
            get
            {
                return doSomethingCommand;
            }
        }
    }

    public class MyCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            string dataFromView = (string)parameter;

            // ...
            MessageBox.Show("Hello: " + dataFromView);
        }
    }
}

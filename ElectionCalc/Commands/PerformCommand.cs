namespace ElectionCalc.Commands
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Implementing ICommand interface in order to route methods as delegates i MVVM.
    /// </summary>
    public class PerformCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _execute;

        public PerformCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }
}

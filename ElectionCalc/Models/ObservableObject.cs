namespace ElectionCalc.Models
{
    using System.ComponentModel;

    /// <summary>
    /// Object implementing INotifyPropertyChanged.
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnProperyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}

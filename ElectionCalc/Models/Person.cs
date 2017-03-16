namespace ElectionCalc
{
    using ElectionCalc.Models;

    abstract public class Person : ObservableObject
    {
        private string _name;
        private string _surname;
        
        public string Name
        {
            get
            {
                if(string.IsNullOrWhiteSpace(_name))
                {
                    return "Unknown";
                }
                return _name;
            }
            set
            {
                _name = value;
                OnProperyChanged("Name");
            }
        }
        public string Surname
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_surname))
                {
                    return "Unknown";
                }
                return _surname;
            }
            set
            {
                _surname = value;
                OnProperyChanged("Surname");
            }
        }
    }

    public class Voter : Person
    {
        private long _pesel;

        public long Pesel
        {
            get
            {
                return _pesel;
            }
            set
            {
                _pesel = value;
                OnProperyChanged("Pesel");
            }
        }
    }

    public class Candidate : Person
    {
        private string _party;
        private long _votingcitizenpesel;

        public string Party
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_party))
                {
                    return "Unknown";
                }
                return _party;
            }
            set
            {
                _party = value;
                OnProperyChanged("Party");
            }
        }
        public long VotingCitizenPesel
        {
            get
            {
                return _votingcitizenpesel;
            }
            set
            {
                _votingcitizenpesel = value;
                OnProperyChanged("VotingCitizenPesel");
            }
        }
    }
}

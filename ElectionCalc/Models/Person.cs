namespace ElectionCalc
{
    abstract public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class Voter : Person
    {
        public long Pesel { get; set; }
        // Nie zaimplementowane 
        public Voter() { }
    }

    public class Candidate : Person
    {
        public string Party { get; set; }
        public long VotingCitizen { get; set; }
        // Nie zaimplementowane 
        public Candidate() { }
    }
}

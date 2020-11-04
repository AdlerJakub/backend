namespace API.DTOs
{
    public class SavingObjective
    {
        public string name { get; set; }
        public string description { get; set; }
        public string imagePath { get; set; }
        public Income[] incomes { get; set; }

    }


    public class Income
    {
        public string date { get; set; }
        public int amount { get; set; }
    }
}
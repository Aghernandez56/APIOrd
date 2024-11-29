namespace APIOrd.Models
{
    public class FortResponse
    {
        public int? status { get; set; }
        public Data data { get; set; } = new Data();    
    }

    public class Data
    {
        public List<Item> items { get; set; } = new List<Item>();

    }

    public class Item
    {
        public Br? br { get; set; }
    }

    public class Br 
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
    }
}

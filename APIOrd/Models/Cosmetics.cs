using Newtonsoft.Json;

namespace APIOrd.Models
{
    public class FortResponse
    {
        public int? status { get; set; }
        public Data data { get; set; } = new Data();
    }

    public class Data
    {
        public Dictionary<string, List<Item>> items { get; set; } = new Dictionary<string, List<Item>>();
    }

    public class Item
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Type type { get; set; }
        public Rarity rarity { get; set; }
        public Series series { get; set; }
        public Set set { get; set; }
        public Introduction introduction { get; set; }
        public Images images { get; set; }
        public string added { get; set; }
    }

    public class Type
    {
        public string value { get; set; }
        public string displayValue { get; set; }
        public string backendValue { get; set; }
    }

    public class Rarity
    {
        public string value { get; set; }
        public string displayValue { get; set; }
        public string backendValue { get; set; }
    }

    public class Series
    {
        public string value { get; set; }
        public string image { get; set; }
        public List<string> colors { get; set; }
        public string backendValue { get; set; }
    }

    public class Set
    {
        public string value { get; set; }
        public string text { get; set; }
        public string backendValue { get; set; }
    }

    public class Introduction
    {
        public string chapter { get; set; }
        public string season { get; set; }
        public string text { get; set; }
        public int backendValue { get; set; }
    }

    public class Images
    {
        public string smallIcon { get; set; }
        public string icon { get; set; }
    }
}

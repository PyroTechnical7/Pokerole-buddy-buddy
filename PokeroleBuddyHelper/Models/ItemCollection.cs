namespace PokeroleBuddyHelper.Models
{
    public class ItemCollection
    {
        [System.Text.Json.Serialization.JsonPropertyName("itemCollection")]
        public List<Item>? ItemList { get; set; }
    }
}
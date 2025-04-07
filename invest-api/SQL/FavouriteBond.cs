using System.Text.Json.Serialization;

namespace invest_api.SQL
{
  public class FavouriteBond
  {
    [JsonPropertyName("Figi")]
    public string Figi { get; set; }
    [JsonPropertyName("Isin")]
    public string Isin { get; set; }
    [JsonPropertyName("Name")]
    public string Name { get; set; }
  }
}

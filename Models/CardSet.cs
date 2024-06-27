namespace api.Models;

public class CardSet
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Series { get; set; }
    public int PrintedTotal { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string SymbolUrl { get; set; }
    public string LogoUrl { get; set; }
}
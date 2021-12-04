namespace TeleTwins.Twins.Publications;

public class TwinPublication
{
    public Guid TwinId { get; init; }
    
    public string Text { get; init; }
    
    public string Url { get; init; }
    
    // TODO: create entity for data mining source
    public string TemporarySource { get; init; } 
    
    public decimal SentimentMetric { get; init; }
    
    // TODO: create entity for publications type
    public string TemporaryType { get; init; }
    
    public decimal Truthfulness { get; init; }
    
    public DateTime SourcePublicationDate { get; init; }
    
    public DateTime? OriginalPublicationDate { get; init; }
    
    public DateTime MiningDateTime { get; set; }
}
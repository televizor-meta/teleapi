namespace TeleTwins.Twins;

public class Twin
{
    public Guid Id { get; init; }
    
    public TwinTextMetrics Comments { get; init; }
    
    public TwinTextMetrics Answers { get; init; }
    
    public TwinMediaMetrics Photos { get; init; }
    
    public long TemporaryEmotionId { get; init; }
}
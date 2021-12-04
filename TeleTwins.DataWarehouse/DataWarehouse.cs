namespace TeleTwins.DataWarehouse;

public class DataWarehouse : IDataWarehouse
{
    public Task StartMiningFor(Guid id, CancellationToken cancellationToken) => Task.CompletedTask;
}
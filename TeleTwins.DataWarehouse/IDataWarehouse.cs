namespace TeleTwins.DataWarehouse;

public interface IDataWarehouse
{
    Task StartMiningFor(Guid id, CancellationToken cancellationToken);
}
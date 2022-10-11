namespace TwoPoi.DataBase;

internal class Repository
{
    public IEnumerable<PointOfInterest> Items => GetItems();

    private IEnumerable<PointOfInterest> GetItems()
    {
        using var dbContext = new DataContext();
        return dbContext.PointsOfInterest.ToArray();
    }

    public async Task AddItemAsync(PointOfInterest poi)
    {
        using var dataContext = new DataContext();
        await dataContext.AddAsync(poi);
        await dataContext.SaveChangesAsync();
    }

    public async Task UpdateItemAsync(PointOfInterest poi)
    {
        using var dataContext = new DataContext();
        dataContext.Update(poi);
        await dataContext.SaveChangesAsync();
    }

    public async Task DeleteItemAsync(PointOfInterest poi)
    {
        using var dataContext = new DataContext();
        dataContext.Remove(poi);
        await dataContext.SaveChangesAsync();
    }
}

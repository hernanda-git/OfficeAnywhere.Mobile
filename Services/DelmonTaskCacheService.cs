namespace OfficeAnywhere.Mobile.Services;

public class DelmonTaskCacheService
{
    private string? _cachedTask;

    public void CacheTask(string task) => _cachedTask = task;

    public string? GetCachedTask()
    {
        var task = _cachedTask;
        _cachedTask = null; 
        return task;
    }
}

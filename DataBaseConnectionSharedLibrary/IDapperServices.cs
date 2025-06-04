
namespace DataBaseConnectionSharedLibrary
{
    public interface IDapperServices
    {
        int Execute(string query, object? parameters = null);
        List<T> Query<T>(string query, object? parameters = null);
    }
}
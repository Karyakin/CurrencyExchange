using TestApi.Entities.Entities;

namespace TestApi.Utils.Helpers.Interfaces;
/// <summary>
/// Получение записей по входному диапазону<br/> 
/// </summary>
public interface IUserHelper
{
    public Task<IEnumerable<User>> GetUsers(Range recordsRange);
}
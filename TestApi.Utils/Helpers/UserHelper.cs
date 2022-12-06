using TestApi.Entities.Entities;
using TestApi.Utils.Helpers.Interfaces;

namespace TestApi.Utils.Helpers;

public class UserHelper : IUserHelper
{
    public async Task<IEnumerable<User>> GetUsers(Range recordsRange)
    {
        // нечего тут выполнять асинхронно. Просто для наглядности
        try
        {
            #region Адьтернативный варик
                /*return AutoPrepareUsers()
                .Skip(recordsRange.Start.Value)
                .Take(recordsRange.End.Value)
                .Select(x => x);
                */
            #endregion

            if (recordsRange.Start.Value > recordsRange.End.Value)
                throw new InvalidOperationException("Проверте диапазон входных параметров");
            
            var users = AutoPrepareUsers()
                .ToList();

            var maxIndex = users.Count - recordsRange.Start.Value;
            
            return users.GetRange(recordsRange.Start.Value, recordsRange.End.Value == 0 ? users.Count : maxIndex);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static IEnumerable<User> AutoPrepareUsers()
    {
        var temp = 100;
        while (temp > 0)
        {
            yield return new User()
            {
                UserName = "SomeUserName",
                FirstName = "SomeFirstName",
                Age = 33,
                Address = new Address
                {
                    City = "SomeCity",
                    Country = "SomeCountry",
                    Street = "SomeStreet",
                    House = 33,
                    Flat = 22,
                }
            };
            temp--;
        }
    }
}
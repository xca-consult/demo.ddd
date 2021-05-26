using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Integration.Database;
using Demo.DDD.Domain;

namespace Demo.DDD.Integration.Database
{
    public class UserRepository : IReaderWriterRepository<User>
    {
        const string FetchSql = "SELECT Id, Name, PhoneNumber, PhoneNumber_CountryCode FROM Users where Id = @Id";

        private const string UpsertSql = @"
                MERGE Users WITH (HOLDLOCK)
                USING ( 
                    VALUES (@Id, @Name, @PhoneNumber, @PhoneNumber_Countrycode)
                ) AS UpdateSource (Id, Name, PhoneNumber, PhoneNumber_Countrycode) 
                ON Users.Id = UpdateSource.Id
                WHEN MATCHED THEN
                   UPDATE SET Name = UpdateSource.Name, PhoneNumber = UpdateSource.PhoneNumber, PhoneNumber_Countrycode = UpdateSource.PhoneNumber_Countrycode
                WHEN NOT MATCHED THEN
                   INSERT (Id, Name, PhoneNumber, PhoneNumber_Countrycode)
                   VALUES (UpdateSource.Id, UpdateSource.Name, UpdateSource.PhoneNumber, UpdateSource.PhoneNumber_Countrycode)
                ;
                ";

        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddAsync(User user)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(UpsertSql, new { Id = user.UserId.Value, Name = user.UserName.Value, PhoneNumber = user.PhoneNumber.Number, PhoneNumber_Countrycode = user.PhoneNumber.CountryCode });
        }
        
        public async Task<User> FindByIdAsync(Guid id)
        {
            await using var connection = new SqlConnection(_connectionString);
            var userDao = await connection.QueryFirstOrDefaultAsync(FetchSql, new { Id = id });

            if (userDao == null)
                return null;

            bool phonePresent = !string.IsNullOrEmpty(userDao.PhoneNumber);

            var user = new User();

            var userId = new UserId(userDao.Id);
            var userName = new UserName(userDao.Name);
            var phoneNumber = phonePresent ? new PhoneNumber(userDao.PhoneNumber_CountryCode, userDao.PhoneNumber) : PhoneNumber.EmptyPhoneNumber;
            ((IFetchAbleAggregate)user).Init(userId, userName, phoneNumber);

            return user;
        }
    }
}

 

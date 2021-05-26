using Demo.DDD.Domain;
using Integration.Database;
using System;
using System.Threading.Tasks;
using Demo.DDD.ApplicationServices.ReadModels;

namespace Demo.DDD.ApplicationServices.QueryServices
{
    public class UserDetailsQueryService : IUserDetailsQueryService
    {
        private readonly IReaderRepository<User> _userAggregateRepository;

        public UserDetailsQueryService(IReaderRepository<User> userAggregateRepository)
        {
            _userAggregateRepository = userAggregateRepository;
        }
       
        public async Task<UserDetailsReadModel> GetUserDetailsAsync(Guid userid)
        {
            var user = await _userAggregateRepository.FindByIdAsync(userid);

            if (user == null)
            {
                throw new ReadModelNotFoundException($"Readmodel for user with Id {userid} not found");
            }

            return new UserDetailsReadModel(user.UserId.Value, user.UserName, user.PhoneNumber?.ToString());
        }
    }
}

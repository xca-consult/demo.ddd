using System;
using System.Threading.Tasks;
using Demo.DDD.ApplicationServices.ReadModels;

namespace Demo.DDD.ApplicationServices.QueryServices
{
    public interface IUserDetailsQueryService
    {
        Task<UserDetailsReadModel> GetUserDetailsAsync(Guid userid);
    }
}
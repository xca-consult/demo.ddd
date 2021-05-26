namespace Demo.DDD.Domain
{
    public interface IFetchAbleAggregate
    {
        void Init(UserId userid, UserName userName, PhoneNumber phoneNumber);
    }
}
namespace SampleRest.Services
{
    public interface IApiUsers
    {
        public bool Authenticate(string login, string password);
    }
}

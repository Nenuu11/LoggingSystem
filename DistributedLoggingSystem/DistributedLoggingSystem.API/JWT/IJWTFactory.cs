namespace DistributedLoggingSystem.API.JWT
{
    public interface IJWTFactory
    {
        string GenerateToken();
        bool IsValidToken(long ticks);
    }
}

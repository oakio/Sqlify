namespace Sqlify.Postgres
{
    public enum PgLockMode
    {
        Update,
        NoKeyUpdate,
        Share,
        KeyShare
    }
}
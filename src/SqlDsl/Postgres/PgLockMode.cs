namespace SqlDsl.Postgres
{
    public enum PgLockMode
    {
        Update,
        NoKeyUpdate,
        Share,
        KeyShare
    }
}
namespace ManahostManager.Domain.Entity
{
    public enum EAccountState
    {
        ADMIN,
        DISABLED,
        REGISTERED, // not payed / only read-only and export data
        VIP         // payed
    }
}
namespace ManahostManager.Domain.Entity
{
    // If you add or delete a value in this enum for the client.
    // Don't forget to change the managerKeyTypes in KeyGeneratorValidation
    public enum EKeyType
    {
        BETA,       // Admin purpose, generation of beta key, free trial
        CLIENT,     // Client key for gift card, discount
        MANAHOST,   // Admin purpose, discount on manahost price
        VALUE_MAX_ENUM
    }
}
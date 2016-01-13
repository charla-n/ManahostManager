namespace ManahostManager.Domain.Entity
{
    public enum EAnswerType
    {
        DESC,   // Response with text
        NUMBER, // Response with a number 0 to 5
        RADIO,   // Response with yes or no (radio button)
        VALUE_MAX_ENUM
    }
}
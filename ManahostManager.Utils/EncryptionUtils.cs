using System;

namespace ManahostManager.Utils
{
    public class EncryptionUtils
    {
        public static void Compare(string orig, string cmp)
        {
            if (BCrypt.CheckPassword(orig, cmp) == false)
                throw new ManahostException(GenericError.INVALID_GIVEN_PARAMETER);
        }

        public static String Encrypt(string plainPassword)
        {
            return BCrypt.HashPassword(plainPassword, BCrypt.GenerateSalt());
        }
    }
}
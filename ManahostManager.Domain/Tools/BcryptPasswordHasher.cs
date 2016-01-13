using ManahostManager.Utils;
using Microsoft.AspNet.Identity;

namespace ManahostManager.Domain.Tools
{
    /// <summary>
    /// Classe qui permet le hash de mot de password pour l'API utilisant BCRYPT comme base.
    /// Elle est encapsuler dans une autre classe
    /// </summary>
    public class BcryptPasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// Permet de hasher le mot de passe
        /// </summary>
        /// <param name="password">Le mot de passe en plaintext</param>
        /// <returns>Mot de passe hasher</returns>
        public string HashPassword(string password)
        {
            return EncryptionUtils.Encrypt(password);
        }

        /// <summary>
        /// Permet la vérification du mot de passe par rapport au mot de passe hasher
        /// </summary>
        /// <param name="hashedPassword">Le mot de passe hasher</param>
        /// <param name="providedPassword">le mot de passe en plainText</param>
        /// <returns></returns>
        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            try
            {
                EncryptionUtils.Compare(providedPassword, hashedPassword);
            }
            catch (ManahostException)
            {
                return PasswordVerificationResult.Failed;
            }
            return PasswordVerificationResult.Success;
        }
    }
}
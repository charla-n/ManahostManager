using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ManahostManager.Utils
{
    public class AES256
    {
        static public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        static public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        /*
        ** Encrypt and decrypt file
        */

        static public void EncryptFile(String filePath, String password)
        {
            byte[] bytesToBeEncrypted = File.ReadAllBytes(filePath);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] bytesEncrypted;

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            bytesEncrypted = AES256.AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                File.WriteAllBytes(filePath, bytesEncrypted);
            }
        }

        static public Stream DecryptFile(String filePath, String password)
        {
            byte[] bytesToBeDecrypted = File.ReadAllBytes(filePath);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] bytesDecrypted;

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            bytesDecrypted = AES256.AES_Decrypt(bytesToBeDecrypted, passwordBytes);
            return new MemoryStream(bytesDecrypted);
        }
    }
}
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Neurotec.Biometrics;
using Neurotec.Images;

namespace mvvFacialRecognition
{
    class cryptography
    {
        int byteCount = 0;

        public cryptography()
        {
        }

        internal string encryptString(string text)
        {
            byte[] stringBytes = Encoding.UTF8.GetBytes(text);
            byte[] encryptedString = EncryptBytes(stringBytes, "damelon");
            return Convert.ToBase64String(encryptedString);
        }

        internal string decryptString(string encryptedId)
        {
            byte[] stringBytes = Convert.FromBase64String(encryptedId);
            byte[] decryptedString = decryptBytes(stringBytes, "damelon");
            return Encoding.UTF8.GetString(decryptedString, 0, byteCount);
        }

        internal byte[] encryptImage(NImage image)
        {
            MemoryStream myStream = new MemoryStream();
            image.ToBitmap().Save(myStream, ImageFormat.Bmp);
            byte[] imageBytes = new byte[myStream.Length];
            myStream.Position = 0;
            myStream.Read(imageBytes, 0, imageBytes.Length);
            byte[] encryptedImage = EncryptBytes(imageBytes, "damelon");
            return encryptedImage;
        }

        //internal byte[] encryptTemplate(NLTemplate template)
        //{
        //    //byte[] templateArray = template.Save().CopyTo(templateArray);
        //    //byte[] encryptedTemplate = EncryptBytes(templateArray, "damelon");
        //    //return encryptedTemplate;
            
        //}

        public Image decryptImage(byte[] encryptedFile)
        {
            byte[] decryptedBytes = decryptBytes(encryptedFile, "damelon");
            ImageConverter ic = new ImageConverter();
            Image decryptedImage = (Image)ic.ConvertFrom(decryptedBytes);
            return decryptedImage;
        }

        private static byte[] EncryptBytes(
            byte[] fileBytes,
            string password,
            string salt = "camiShadow",
            string hashAlgorithm = "SHA1",
            string initialVector = "X^EzyDuH6$k2S7Dg",
            int passwordIterations = 2,
            int keySize = 256)
        {
            if (fileBytes.Length == 0)
            {
                return fileBytes;
            }

            byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
            PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = derivedPassword.GetBytes(keySize / 8);
            RijndaelManaged symetricKey = new RijndaelManaged();
            symetricKey.Mode = CipherMode.CBC;
            byte[] cipherTextBytes = null;

            using (ICryptoTransform encryptor = symetricKey.CreateEncryptor(keyBytes, initialVectorBytes))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    using (CryptoStream stream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                    {
                        stream.Write(fileBytes, 0, fileBytes.Length);
                        stream.FlushFinalBlock();
                        cipherTextBytes = memStream.ToArray();
                        memStream.Close();
                        stream.Close();
                    }
                }
            }
            symetricKey.Clear();
            return cipherTextBytes;
        }

        public byte[] decryptBytes(
            byte[] encryptedFile,
            string password = "damelon",
            string salt = "camiShadow",
            string hashAlgorithm = "SHA1",
            string initialVector = "X^EzyDuH6$k2S7Dg",
            int passwordIterations = 2,
            int keySize = 256)
        {
            if (encryptedFile.Length == 0)
            {
                return encryptedFile;
            }

            byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
            PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = derivedPassword.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            byte[] decryptedBytes = new byte[encryptedFile.Length];
            byteCount = 0;
            using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initialVectorBytes))
            {
                using (MemoryStream myStream = new MemoryStream(encryptedFile))
                {
                    using (CryptoStream cryptostream = new CryptoStream(myStream, decryptor, CryptoStreamMode.Read))
                    {
                        byteCount = cryptostream.Read(decryptedBytes, 0, decryptedBytes.Length);
                        myStream.Close();
                        cryptostream.Close();
                    }
                }
            }
            symmetricKey.Clear();
            return decryptedBytes;
        }
    }
}

namespace Asp.NetCore.Shared.Helpers
{
    public static class EncryptHelper
    {
        public static string SecurityKey = "default";

        /// <summary>
        /// Encrypts the Md5.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static string EncryptMD5(string content)
        {
            UTF8Encoding Unic = new UTF8Encoding();

            byte[] bytes = Unic.GetBytes(content);

            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] result = md5.ComputeHash(bytes);

            return BitConverter.ToString(result);
        }

        /// <summary>
        /// Encrypts the SHA.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static string EncryptSHA(string content)
        {
            System.Text.UnicodeEncoding encoding = new System.Text.UnicodeEncoding();

            byte[] hashBytes = encoding.GetBytes(content);

            //Compute the SHA-1 hash
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            byte[] result = sha1.ComputeHash(hashBytes);

            return BitConverter.ToString(result);
        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            //System.Configuration.AppSettingsReader settingsReader =
            //                                    new AppSettingsReader();
            // Get the key from config file

            //string key = ConfigurationManager.AppSettings["SecurityKey"];
            string key = "test";
            if (String.IsNullOrEmpty(key))
            {
                key = SecurityKey;
            }

            // (string)settingsReader.GetValue("SecurityKey",typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            //System.Configuration.AppSettingsReader settingsReader =
            //                                    new AppSettingsReader();
            //Get your key from config file to open the lock!
            //string key = (string)settingsReader.GetValue("SecurityKey",
            //                                             typeof(String));

            //string key = ConfigurationManager.AppSettings["SecurityKey"];
            string key = "test";
            if (String.IsNullOrEmpty(key))
            {
                key = SecurityKey;
            }

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }   
}

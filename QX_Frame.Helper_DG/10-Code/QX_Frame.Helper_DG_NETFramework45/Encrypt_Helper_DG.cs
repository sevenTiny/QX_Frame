using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Web.Security;
using System.IO;

namespace QX_Frame.Helper_DG
{
    /*2016-11-11 17:21:08 author:qixiao*/
    public abstract class Encrypt_Helper_DG
    {
        #region MD5 Encrypt
        /// <summary>
        /// Encypt via MD5
        /// </summary>
        /// <param name="str">encrypt content</param>
        /// <param name="MD5_length">the value length</param>
        /// <returns>Md5 value</returns>
        public static string MD5_Encrypt(string str, int MD5_length = 32)
        {
            if (MD5_length == 16)
#pragma warning disable CS0618 // Type or member is obsolete
                return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
#pragma warning restore CS0618 // Type or member is obsolete
            else if (MD5_length == 32)
#pragma warning disable CS0618 // Type or member is obsolete
                return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
#pragma warning restore CS0618 // Type or member is obsolete
            else
                throw new ArgumentException("the MD5_length is can not be except by 16 or 32 bit --QX_Frame");
        }

        #endregion

        #region RSA Encrypt
        /// <summary>
        /// RSA的容器 可以解密的源字符串长度为 DWKEYSIZE/8-11 
        /// </summary>
        private const int DWKEYSIZE = 1024;
        /// <summary>
        /// the class of RSA_Keys put publicKey and privateKey
        /// </summary>
        public struct RSA_Keys
        {
            public string publicKey { get; set; }
            public string privateKey { get; set; }
        }
        /// <summary>
        /// RSA_GetKeys
        /// </summary>
        /// <returns>Dictionary<string,string> public_key or private_key</string></returns>
        public static RSA_Keys RSA_GetKeys()
        {
            var rsaProvider = new RSACryptoServiceProvider(DWKEYSIZE);
            RSAParameters parameter = rsaProvider.ExportParameters(true);
            return new RSA_Keys()
            {
                publicKey = rsaProvider.ToXmlString(false),//BytesToHexString(parameter.Exponent) + "," + BytesToHexString(parameter.Modulus),
                privateKey = rsaProvider.ToXmlString(true)
            };
        }

        /// <summary>
        /// RSA_Encrypt
        /// </summary>
        /// <param name="content">Data</param>
        /// <param name="privateKey">privateKey</param>
        /// <returns></returns>
        public static string RSA_Encrypt(string content, string publicKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                return Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(content), false));
            }
        }

        /// <summary>
        /// RSA Decrypt method
        /// </summary>
        /// <param name="encryptedContent">encryptData</param>
        /// <param name="privateKey">privateKey</param>
        /// <returns></returns>
        public static string RSA_Decrypt(string encryptedContent, string privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                return Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(encryptedContent), false));
            }
        }

        /// <summary>
        /// Get RSA HashDescription
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSA_GetHashDescription(string content)
        {
            HashAlgorithm MD5 = HashAlgorithm.Create("MD5");
            return Convert.ToBase64String(MD5.ComputeHash(Encoding.UTF8.GetBytes(content)));
        }

        #region RSA Signature
        /// <summary>
        /// RSA_Signature
        /// </summary>
        /// <param name="privateKey">privateKey</param>
        /// <param name="hashContent">hashContent</param>
        /// <returns></returns>
        public static string RSA_Signature(string privateKey, string hashContent)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(privateKey);
                RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA);
                RSAFormatter.SetHashAlgorithm("MD5");//set signature arithmetic
                return Convert.ToBase64String(RSAFormatter.CreateSignature(Convert.FromBase64String(hashContent)));
            }
        }

        /// <summary>
        /// RSA_SignatureVerify
        /// </summary>
        /// <param name="publicKey">publicKey</param>
        /// <param name="RSA_HashDescription_String">RSA_HashDescription_String</param>
        /// <param name="RSA_Signature_String">RSA_Signature_String</param>
        /// <returns></returns>
        public static bool RSA_SignatureVerify(string publicKey, string RSA_HashDescription_String, string RSA_Signature_String)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(publicKey);
                RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(RSA);
                RSADeformatter.SetHashAlgorithm("MD5");
                byte[] HashDescription_String = Convert.FromBase64String(RSA_HashDescription_String);
                byte[] Signature_String = Convert.FromBase64String(RSA_Signature_String);
                return RSADeformatter.VerifySignature(HashDescription_String, Signature_String) == true ? true : false;
            }
        }

        #endregion

        #endregion END RSA Encrypt
    }
}
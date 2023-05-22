using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace QX_Frame.Bantina
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

        /**
         * RSA 加密解密签名算法简介：
         * RSA公钥-> 1：用于加密数据，然后用私钥解密；2：用于得到数据的Hash码并且配合私钥签名进行签名验证；
         * RSA私钥-> 1：用于解密公钥加密后的密文；2：用于对数据Hash签名
         * 使用场景：甲，乙二人，甲执公钥，乙执私钥
         * 甲向乙发送内容，并用公钥加密，只能由乙使用私钥解密查看，其他持有公钥的无法解密；
         * 乙向甲发送内容，先用内容的Hash进行签名，然后将内容和签名一起发给甲，甲判断是否内容被篡改，Hash内容然后使用Hash内容、签名、公钥对签名进行验证，通过则未被篡改
         * */

        /// <summary>
        /// RSA的容器 可以解密的源字符串长度为 DWKEYSIZE/8-11 
        /// </summary>
        private const int DWKEYSIZE = 1024;
        /// <summary>
        /// the class of RSA_Keys put publicKey and privateKey
        /// </summary>
        public struct RSA_Keys
        {
            public string PublicKey { get; set; }
            public string PrivateKey { get; set; }
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
                PublicKey = rsaProvider.ToXmlString(false),//BytesToHexString(parameter.Exponent) + "," + BytesToHexString(parameter.Modulus),
                PrivateKey = rsaProvider.ToXmlString(true)
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

        #region 3DES Encrypt
        //构造一个对称算法
        private static SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
        /// <summary>
        /// 字符串的加密
        /// </summary>
        /// <param name="Content">要加密的字符串</param>
        /// <param name="sKey">密钥，必须24位</param>
        /// <param name="sIV_12bit">向量，必须是8个字符</param>
        /// <returns>加密后的字符串</returns>
        public static string DES3_Encrypt(string Content, string sKey_24bit, string sIV_8bit= "qx_frame")
        {
            try
            {
                mCSP.Key = Encoding.UTF8.GetBytes(sKey_24bit);
                mCSP.IV = Encoding.UTF8.GetBytes(sIV_8bit);
                //指定加密的运算模式
                mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
                //获取或设置加密算法的填充模式
                mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ICryptoTransform ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);//创建加密对象
                byte[] byt = Encoding.UTF8.GetBytes(Content);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="encryptedContent">加密后的字符串</param>
        /// <param name="sKey">密钥，必须24位</param>
        /// <param name="sIV_12bit">向量，必须是8个字符</param>
        /// <returns>解密后的字符串</returns>
        public static string DES3_Decrypt(string encryptedContent, string sKey_24bit, string sIV_8bit= "qx_frame")
        {
            try
            {
                //将3DES的密钥转换成byte
                mCSP.Key = Encoding.UTF8.GetBytes(sKey_24bit);
                //将3DES的向量转换成byte
                mCSP.IV = Encoding.UTF8.GetBytes(sIV_8bit);
                mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
                mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ICryptoTransform ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);//加密转换运算,创建对称解密对象
                byte[] byt = Convert.FromBase64String(encryptedContent);
                MemoryStream ms = new MemoryStream();//内存流
                CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);//数据流连接到数据加密转换的流
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Base64 Encode
        public static string Base64_Encode(string content)
        {
            byte[] inArray = System.Text.Encoding.UTF8.GetBytes(content);
            return Convert.ToBase64String(inArray);
        }

        public static string Base64_Decode(string content)
        {
            byte[] inArray = Convert.FromBase64String(content);
            return System.Text.Encoding.UTF8.GetString(inArray);
        }
        #endregion

        #region 混淆与反混淆

        /// <summary>
        /// The timestamp length.
        /// </summary>
        private const int TimestampLength = 36;

        /// <summary>
        /// 用时间简单混淆
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns>混淆后字符串</returns>
        public static string MixUp(string str)
        {
            var guid = Guid.NewGuid().ToString();
            var count = str.Length + TimestampLength;
            var sbd = new StringBuilder(count);
            int j = 0;
            int k = 0;
            for (int i = 0; i < count; i++)
            {
                if (j < TimestampLength && k < str.Length)
                {
                    if (i % 2 == 0)
                    {
                        sbd.Append(str[k]);
                        k++;
                    }
                    else
                    {
                        sbd.Append(guid[j]);
                        j++;
                    }
                }
                else if (j >= TimestampLength)
                {
                    sbd.Append(str[k]);
                    k++;
                }
                else if (k >= str.Length)
                {
                    break;
                }
            }

            return sbd.ToString();
        }

        /// <summary>
        /// 简单反混淆
        /// </summary>
        /// <param name="str">混淆后字符串</param>
        /// <returns>原来字符串</returns>
        public static string ReMixUp(string str)
        {
            var sbd = new StringBuilder();
            int j = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (i % 2 == 0)
                {
                    sbd.Append(str[i]);
                }
                else
                {
                    j++;
                }

                if (j > TimestampLength)
                {
                    sbd.Append(str.Substring(i));
                    break;
                }
            }
            return sbd.ToString();
        }

        #endregion
    }
}
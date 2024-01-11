using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;

namespace TaxServiceAdaptor.Helpers {
    public static class EncryptionHelper {

        /// <summary>
        /// Encrypts provided string parameter with DES Encryption Algorithm
        /// </summary>
        public static string Encrypt (string s, string key) {
            if (string.IsNullOrEmpty (s)) return string.Empty;

            string result = string.Empty;

            try {
                byte[] buffer = Encoding.ASCII.GetBytes (s);

                DESCryptoServiceProvider des =
                    new DESCryptoServiceProvider ();

                des.Key = ASCIIEncoding.ASCII.GetBytes (key);
                des.Mode = CipherMode.ECB;

                des.Padding = PaddingMode.Zeros;
                result = Convert.ToBase64String (
                    des.CreateEncryptor ().TransformFinalBlock (
                        buffer, 0, buffer.Length));
            } catch (Exception e) {
                throw e;
            }

            return result;
        }

        /// <summary>
        /// Decrypts provided string parameter with DES Encryption Algorithm
        /// </summary>  
        public static string Decrypt (string s, byte[] key) {
            if (string.IsNullOrEmpty (s)) return string.Empty;

            string result = string.Empty;

            try {
                byte[] buffer = Convert.FromBase64String (s);

                DESCryptoServiceProvider des =
                    new DESCryptoServiceProvider ();

                des.Mode = CipherMode.ECB;
                des.Key = key;

                des.Padding = PaddingMode.None;

                result = Encoding.ASCII.GetString (
                    des.CreateDecryptor ().TransformFinalBlock (
                        buffer, 0, buffer.Length));
            } catch (Exception e) {
                throw e;
            }

            return result;
        }

        /// <summary>
        /// Computes MD5 Hash on a given test/string
        /// </summary>
        public static string ComputeMD5Hash (string s) {
            if (s == null || s.Length == 0) return string.Empty;

            byte[] hashed = null;

            MD5CryptoServiceProvider MD5 =
                new MD5CryptoServiceProvider ();

            hashed = MD5.ComputeHash (ASCIIEncoding.ASCII.GetBytes (s));

            return Convert.ToBase64String (hashed);
        }

        /// <summary>
        /// Encrypted by private key using RSA algorithm
        /// </summary>
        public static string RSAEncrypt (string textToEncrypt, string publicKeyString) {
            if (string.IsNullOrEmpty (publicKeyString)) return string.Empty;

            byte[] buffer = Encoding.ASCII.GetBytes (textToEncrypt);

            AsymmetricKeyParameter asymKey = ReadPrivateKey (publicKeyString);
            var keyPair = GetKeyPairFromPrivateKey (asymKey);
            IAsymmetricBlockCipher engine = new Pkcs1Encoding (new RsaEngine ());
            engine.Init (true, keyPair.Private);
            byte[] cipheredBytes = engine.ProcessBlock (buffer, 0, buffer.Length);
            return Convert.ToBase64String (cipheredBytes);
        }

        /// <summary>
        /// Reads the private key.
        /// </summary>
        /// <param name="privateKey">Private key string</param>
        /// <returns>private key object</returns>
        private static AsymmetricKeyParameter ReadPrivateKey (string privateKey) {
            AsymmetricKeyParameter asymKey;
            using (TextReader sr = new StringReader ($"-----BEGIN PRIVATE KEY-----\n{privateKey}\n-----END PRIVATE KEY-----")) {
                asymKey = (AsymmetricKeyParameter) new PemReader (sr).ReadObject ();
            }
            return asymKey;
        }
        /// <summary>
        /// Decrypt by private key using RSA algorithm
        /// </summary>
        public static byte[] RsaDecryptWithPrivateKey (string privateKey, string textToDecrypt) {
            var bytesToDecrypt = Convert.FromBase64String (textToDecrypt);
            AsymmetricKeyParameter asymKey = ReadPrivateKey (privateKey);
            var keyPair = GetKeyPairFromPrivateKey (asymKey);
            var decryptEngine = new Pkcs1Encoding (new RsaEngine ());
            decryptEngine.Init (false, keyPair.Private);
            return decryptEngine.ProcessBlock (bytesToDecrypt, 0, bytesToDecrypt.Length);
        }
        private static AsymmetricCipherKeyPair GetKeyPairFromPrivateKey (AsymmetricKeyParameter privateKey) {
            AsymmetricCipherKeyPair keyPair = null;
            if (privateKey is RsaPrivateCrtKeyParameters rsa) {
                var pub = new RsaKeyParameters (false, rsa.Modulus, rsa.PublicExponent);
                keyPair = new AsymmetricCipherKeyPair (pub, privateKey);
            } else if (privateKey is Ed25519PrivateKeyParameters ed) {
                var pub = ed.GeneratePublicKey ();
                keyPair = new AsymmetricCipherKeyPair (pub, privateKey);
            } else if (privateKey is ECPrivateKeyParameters ec) {
                var q = ec.Parameters.G.Multiply (ec.D);
                var pub = new ECPublicKeyParameters (ec.AlgorithmName, q, ec.PublicKeyParamSet);
                keyPair = new AsymmetricCipherKeyPair (pub, ec);
            }
            if (keyPair == null)
                throw new NotSupportedException ($"The key type {privateKey.GetType().Name} is not supported.");
            return keyPair;
        }
    }
}
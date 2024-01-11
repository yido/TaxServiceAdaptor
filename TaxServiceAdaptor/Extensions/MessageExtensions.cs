using System.Text;
using TaxServiceAdaptor.DTO;
using TaxServiceAdaptor.Helpers;

namespace TaxServiceAdaptor {
    public static class MessageExtensions {
        public static Message<ServiceType> Encrypt (this Message<ServiceType> msg) {
            msg.Content = EncryptionHelper.Encrypt (msg.Content, msg.ServiceType.DESKey);
            msg.Sign = EncryptionHelper.ComputeMD5Hash (msg.Content);
            msg.Key = EncryptionHelper.RSAEncrypt (msg.ServiceType.DESKey, msg.ServiceType.PrivateKey);

            return msg;
        }
        public static Message<ServiceType> Dycrypt (this Message<ServiceType> msg, bool is_private_key_application) { 

            if (is_private_key_application) 
            { 
                msg.Content = EncryptionHelper.Decrypt (msg.Content, Encoding.ASCII.GetBytes (msg.ServiceType.DESKey)); 
            } 
            else 
            {
                var key_in_byte_array = EncryptionHelper.RsaDecryptWithPrivateKey (msg.ServiceType.PrivateKey, msg.Key);
                msg.Content = EncryptionHelper.Decrypt (msg.Content, key_in_byte_array);
            }

            return msg;
        }
    }
}
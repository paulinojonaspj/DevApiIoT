using System.Security.Cryptography;
using System.Text;

namespace IOTBack.Configuracao
{
    public class Key
    {
        public static string Secret = "7a40dde2d5303300964fb7c8f3c14ab5";

        // string publicKey = rsa.ToXmlString(false);
        // string privateKey = rsa.ToXmlString(true);

        private static string chavePublicaRSA = "<RSAKeyValue><Modulus>z08fyZQN8+BjFFhEQDoJLVbPT3uchIZd76EpQ6tyj5w31i9FrtcQmPi8lvQ6mQTf0Zf3C6x8vxoWVOK7DpEMu7hRiMLiYTiVZIWhXipMr/H2OeuBZ/YOJLUS3O6cvCtI77m7a1eNHpt9jH/6BGUHdZBxkFhhHLbx5ZliX1wSl8E=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private static string chavePrivadaRSA = "<RSAKeyValue><Modulus>z08fyZQN8+BjFFhEQDoJLVbPT3uchIZd76EpQ6tyj5w31i9FrtcQmPi8lvQ6mQTf0Zf3C6x8vxoWVOK7DpEMu7hRiMLiYTiVZIWhXipMr/H2OeuBZ/YOJLUS3O6cvCtI77m7a1eNHpt9jH/6BGUHdZBxkFhhHLbx5ZliX1wSl8E=</Modulus><Exponent>AQAB</Exponent><P>09XZXubxfjUyACLAekCQxTRH/fKiW1xkvtQcA62FdpU3DcTxoJG/ElJGj1mukqQL3oDTl0yirHeG6mShz8sqLw==</P><Q>+oeyine2MhvdrS9ZPwo9mhVGTRwuqevRDQ7KqKFc6GqkIEyzG/nniru8hm4B5rc11bHA746BcCcJxdgGMwIRDw==</Q><DP>UNOx4wIfdtZ5Q4/+/SaRlo1CQuVHibCRSDbKkaSt3fdFUF2rDGdQDn+SKLRBZfZuCPICyiIVl9Trh5fqHc0ZRQ==</DP><DQ>W6NvJz8KoeGXgFWla22vgsDtah844majnQcgEfaUKV94kWf8y+rpStHI79MlVuMFChlu3TFfH0roRDn0aowC+Q==</DQ><InverseQ>OFIFeEgwK6FU15Jssr7CxDaIhI+2i9glrdAqnlf88d0cALSONtrg5NIzr7lvoKvc3+aC6B4/uH3e9jMnMGJ8Rg==</InverseQ><D>SRfJymYEQV91L6AreEW+JJk+APVBa0yA5FN7XBugCdh3q3IBbLLxbC4POF4mJvub+qdM+HW5ihulkgpbD7i+tvIxD46Vrh3Ike2ujSAXHLR9YwnwPR3pe+IGAhrAnjEz9yWDF75hVzCDnPyvf/1ROvvFzT3DsbKf/QkzPiEgno0=</D></RSAKeyValue>";

        public static string Criptografar(string mensagem)
        {
            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                rsa.FromXmlString(chavePublicaRSA);
                byte[] mensagemBytes = Encoding.UTF8.GetBytes(mensagem);
                byte[] criptografadoBytes = rsa.Encrypt(mensagemBytes, false);
                return Convert.ToBase64String(criptografadoBytes);
            }
        }

        public static string Descriptografar(string mensagemCriptografada)
        {
            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                rsa.FromXmlString(chavePrivadaRSA);
                byte[] criptografadoBytes = Convert.FromBase64String(mensagemCriptografada);
                byte[] descriptografadoBytes = rsa.Decrypt(criptografadoBytes, false);
                return Encoding.UTF8.GetString(descriptografadoBytes);
            }
        }

 
    }
}

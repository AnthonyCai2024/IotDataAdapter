using System.Security.Cryptography;

namespace WebApplication1.Cryptography;

public class RsaKeyPair
{
    public string PublicKey { get; private set; }
    public string PrivateKey { get; private set; }

    public RsaKeyPair()
    {
        using var rsa = new RSACryptoServiceProvider(2048);
        PublicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
        PrivateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
    }
}
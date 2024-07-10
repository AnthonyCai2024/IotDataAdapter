using System.Security.Cryptography;

namespace WebApplication1.Cryptography;

public class RsaPemLoader
{
    public static RSA LoadPrivateKey(string pemFilePath)
    {
        var pem = File.ReadAllText(pemFilePath);
        var rsa = RSA.Create();
        rsa.ImportFromPem(pem);
        return rsa;
    }

    public static RSA LoadPublicKey(string pemFilePath)
    {
        var pem = File.ReadAllText(pemFilePath);
        var rsa = RSA.Create();
        rsa.ImportFromPem(pem);
        return rsa;
    }
}
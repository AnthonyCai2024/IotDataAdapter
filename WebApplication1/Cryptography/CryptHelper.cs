using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Cryptography;

public class CryptHelper
{
    public static string SignData(string dataToSign, string privateKey)
    {
        var dataBytes = Encoding.UTF8.GetBytes(dataToSign);

        using var rsa = new RSACryptoServiceProvider();

        rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
        var signedBytes = rsa.SignData(dataBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);


        return Convert.ToBase64String(signedBytes);
    }

    public static bool VerifyData(string originalData, string signedData, string publicKey)
    {
        var originalDataBytes = Encoding.UTF8.GetBytes(originalData);
        var signedDataBytes = Convert.FromBase64String(signedData);

        using var rsa = new RSACryptoServiceProvider();
        rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
        return rsa.VerifyData(originalDataBytes, signedDataBytes, HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1);
    }


    public void Test()
    {
        string publicKeyPath = "public_key.pem";
        string privateKeyPath = "private_key.pem";

        // 加载公钥和私钥
        RSA publicKey = RsaPemLoader.LoadPublicKey(publicKeyPath);
        RSA privateKey = RsaPemLoader.LoadPrivateKey(privateKeyPath);

        // 要加密的消息
        string message = "Hello, this is a secret message!";
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);

        // 使用公钥加密
        byte[] encryptedBytes = publicKey.Encrypt(messageBytes, RSAEncryptionPadding.OaepSHA256);
        Console.WriteLine("加密后的数据：" + Convert.ToBase64String(encryptedBytes));

        // 使用私钥解密
        byte[] decryptedBytes = privateKey.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA256);
        string decryptedMessage = Encoding.UTF8.GetString(decryptedBytes);
        Console.WriteLine("解密后的数据：" + decryptedMessage);
        
        //使用私钥签名
        byte[] signedBytes = privateKey.SignData(messageBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        
        //使用公钥验证签名
        bool verified = publicKey.VerifyData(messageBytes, signedBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        
        // var rsaKeyPair = new RsaKeyPair();
        //
        // string data = "This is a test message.";
        // Console.WriteLine($"Original Data: {data}");
        //
        // // 服务器生成数字签名
        // string signedData = SignData(data, rsaKeyPair.PrivateKey);
        // Console.WriteLine($"Signed Data: {signedData}");
        //
        // data= "This is a test message.222";
        //
        // // 客户端验证数字签名
        // bool isVerified = VerifyData(data, signedData, rsaKeyPair.PublicKey);
        // Console.WriteLine($"Signature Verified: {isVerified}");
    }
}
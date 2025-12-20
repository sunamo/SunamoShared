// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
// Instance variables refactored according to C# conventions
namespace SunamoShared.Crypting;
/// <summary>
/// Asymmetric encryption uses a pair of keys to encrypt and decrypt.
/// There is a "public" key which is used to encrypt. Decrypting, on the other hand, 
/// requires both the "public" key and an additional "private" key. The advantage is 
/// that people can send you encrypted messages without being able to decrypt them.
/// </summary>
/// <remarks>
/// The only provider supported is the <see cref = "RSACryptoServiceProvider"/>
/// </remarks>
public partial class Asymmetric
{
    /// <summary>
    /// Returns the default private key as stored in the *.config file
    /// Vratim PrivateKey z CM.AS a G
    /// </summary>
    public PrivateKey DefaultPrivateKey
    {
        get
        {
            PrivateKey privkey = new PrivateKey();
            privkey.LoadFromConfig();
            return privkey;
        }
    }

    /// <summary>
    /// Generates a new public/private key pair as objects
    /// VO RSA a vlozim do As verejne a privitni klic, ktere vygeneruji v teto ttide.
    /// Vlozim do typovanych objektu
    /// </summary>
    public void GenerateNewKeyset(ref PublicKey publicKey, ref PrivateKey privateKey)
    {
        string PublicKeyXML = null;
        string PrivateKeyXML = null;
        GenerateNewKeyset(ref PublicKeyXML, ref PrivateKeyXML);
        publicKey = new PublicKey(PublicKeyXML);
        privateKey = new PrivateKey(PrivateKeyXML);
    }

    /// <summary>
    /// Generates a new public/private key pair as XML strings
    /// VO RSA a vlozim do As veeejne a privatni klic, ktery vygeneruji v teto tride.
    /// </summary>
    public void GenerateNewKeyset(ref string publicKeyXML, ref string privateKeyXML)
    {
        RSA rsa = RSA.Create();
        publicKeyXML = rsa.ToXmlString(false);
        privateKeyXML = rsa.ToXmlString(true);
    }

    /// <summary>
    /// Encrypts data using the default public key
    /// Zakryptuje A1 klicem v DefaultPublicKey 
    /// </summary>
    public DataCrypt Encrypt(DataCrypt d)
    {
        PublicKey PublicKey = DefaultPublicKey;
        return Encrypt(d, PublicKey);
    }
}
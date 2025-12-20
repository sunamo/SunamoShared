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
    /// Provider sifrovani RSA
    /// </summary>
    private RSACryptoServiceProvider rsaCryptoProvider;
    /// <summary>
    /// Vychozy jmeno kontejneru, ve kterem se bude uchovavat klic.
    /// </summary>
    private string _KeyContainerName = "Encryption.AsymmetricEncryption.DefaultContainerName";
    /// <summary>
    /// Vychozi velikost klice v bytech.
    /// </summary>
    private int _KeySize = 1024;
    private const string _ElementParent = "RSAKeyValue";
    private const string _ElementModulus = "Modulus";
    private const string _ElementExponent = "Exponent";
    private const string _ElementPrimeP = "P";
    private const string _ElementPrimeQ = "Q";
    private const string _ElementPrimeExponentP = "DP";
    private const string _ElementPrimeExponentQ = "DQ";
    private const string _ElementCoefficient = "InverseQ";
    private const string _ElementPrivateExponent = "D";
    // - http://forum.java.sun.com/thread.jsp?forum=9&thread=552022&tstart=0&trange=15 
    private const string _KeyModulus = "PublicKey.Modulus";
    private const string _KeyExponent = "PublicKey.Exponent";
    private const string _KeyPrimeP = "PrivateKey.P";
    private const string _KeyPrimeQ = "PrivateKey.Q";
    private const string _KeyPrimeExponentP = "PrivateKey.DP";
    private const string _KeyPrimeExponentQ = "PrivateKey.DQ";
    private const string _KeyCoefficient = "PrivateKey.InverseQ";
    private const string _KeyPrivateExponent = "PrivateKey.D";
    /// <summary>
    /// Represents a public encryption key. Intended to be shared, it 
    /// contains only the Modulus and Exponent.
    /// Ttrda verejneho klice. Ma metody pro nacteni a ulozeni z/do ruznych zdroju.
    /// </summary>
    public class PublicKey
    {
        public string Modulus;
        public string Exponent;
        /// <summary>
        /// IK
        /// </summary>
        public PublicKey()
        {
        }

        /// <summary>
        /// EK. Nactu z XML A1 obsahy tagu Modulus a Exponent a ulozim je do stejne pojm. VV
        /// </summary>
        /// <param name = "KeyXml"></param>
        public PublicKey(string KeyXml)
        {
            LoadFromXml(KeyXml);
        }

        /// <summary>
        /// Load public key from App.config or Web.config file
        /// Ulozim do PP z CM.AS
        /// </summary>
        public void LoadFromConfig()
        {
            Modulus = UtilsNonNetStandard.GetConfigString(_KeyModulus, true);
            Exponent = UtilsNonNetStandard.GetConfigString(_KeyExponent, true);
        }

        /// <summary>
        /// Returns *.config file XML section representing this public key
        /// Vratim 2x tax Add text argumenty PP Modulus a Exponent
        /// </summary>
        public string ToConfigSection()
        {
            StringBuilder stringBuilder = new StringBuilder();
            // TODO: Nevim zda bych nemel vytvorit novou instanci stringBuilder
            StringBuilder _with1 = stringBuilder;
            _with1.Append(UtilsNonNetStandard.WriteConfigKey(_KeyModulus, Modulus));
            _with1.Append(UtilsNonNetStandard.WriteConfigKey(_KeyExponent, Exponent));
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Writes the *.config file representation of this public key to a file
        /// Prepnu A1 2x tagem Add text argumenty PP Modulus a Exponent
        /// </summary>
        public void ExportToConfigFile(string filePath)
        {
            StreamWriter sw = new StreamWriter(filePath, false);
            sw.Write(ToConfigSection());
            sw.Close();
        }

        /// <summary>
        /// Loads the public key from its XML string
        /// Nactu z XML A1 obsahy tagu Modulus a Exponent a ulozim je do stejne pojm. VV
        /// </summary>
        public void LoadFromXml(string keyXml)
        {
            Modulus = UtilsNonNetStandard.GetXmlElement(keyXml, Translate.FromKey(XlfKeys.Modulus));
            Exponent = UtilsNonNetStandard.GetXmlElement(keyXml, Translate.FromKey(XlfKeys.Exponent));
        }

        /// <summary>
        /// Converts this public key to an RSAParameters object
        /// Vrati mi pp Modulus a Exponent v O RSAParameters
        /// </summary>
        public RSAParameters ToParameters()
        {
            RSAParameters r = new RSAParameters();
            r.Modulus = Convert.FromBase64String(Modulus);
            r.Exponent = Convert.FromBase64String(Exponent);
            return r;
        }

        /// <summary>
        /// Converts this public key to its XML string representation
        /// Vrati mi Tagy PP Modulus a Exponent v Tagu RSAKeyValue
        /// </summary>
        public string ToXml()
        {
            StringBuilder stringBuilder = new StringBuilder();
            // TODO: Nevim zda bych nemel vytvoiit novou instanci stringBuilder
            StringBuilder _with2 = stringBuilder;
            // Mohl bych to zapsat pomoci T RSAParameters ale nevim jak by se to vyporadalo text verejnym klicem.
            _with2.Append(UtilsNonNetStandard.WriteXmlNode(_ElementParent, false));
            _with2.Append(UtilsNonNetStandard.WriteXmlElement(_ElementModulus, Modulus));
            _with2.Append(UtilsNonNetStandard.WriteXmlElement(_ElementExponent, Exponent));
            _with2.Append(UtilsNonNetStandard.WriteXmlNode(_ElementParent, true));
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Writes the Xml representation of this public key to a file
        /// Prepne A1 Tagy PP Modulus a Exponent v Tagu RSAKeyValue
        /// </summary>
        public void ExportToXmlFile(string filePath)
        {
            StreamWriter sw = new StreamWriter(filePath, false);
            sw.Write(ToXml());
            sw.Close();
        }
    }
}
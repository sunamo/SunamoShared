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
    /// Represents a private encryption key. Not intended to be shared, as it 
    /// contains all the elements that make up the key.
    /// </summary>
    public class PrivateKey
    {
#region Prevedou se na base64 a vlozi se do objektu RSAParameters
        public string Modulus;
        public string Exponent;
        public string PrimeP;
        public string PrimeQ;
        public string PrimeExponentP;
        public string PrimeExponentQ;
        public string Coefficient;
        public string PrivateExponent;
#endregion
        /// <summary>
        /// IK
        /// </summary>
        public PrivateKey()
        {
        }

        /// <summary>
        /// Nactu z XML A1 obsahy tagu Modulus a Exponent a dalsi tagy a ulozim je do stejne pojm. VV
        /// </summary>
        /// <param name = "keyXml"></param>
        public PrivateKey(string keyXml)
        {
            LoadFromXml(keyXml);
        }

        /// <summary>
        /// Load private key from App.config or Web.config file
        /// Ulozim do PPs z CM.AS
        /// </summary>
        public void LoadFromConfig()
        {
            Modulus = UtilsNonNetStandard.GetConfigString(_KeyModulus, true);
            Exponent = UtilsNonNetStandard.GetConfigString(_KeyExponent, true);
            PrimeP = UtilsNonNetStandard.GetConfigString(_KeyPrimeP, true);
            PrimeQ = UtilsNonNetStandard.GetConfigString(_KeyPrimeQ, true);
            PrimeExponentP = UtilsNonNetStandard.GetConfigString(_KeyPrimeExponentP, true);
            PrimeExponentQ = UtilsNonNetStandard.GetConfigString(_KeyPrimeExponentQ, true);
            Coefficient = UtilsNonNetStandard.GetConfigString(_KeyCoefficient, true);
            PrivateExponent = UtilsNonNetStandard.GetConfigString(_KeyPrivateExponent, true);
        }

        /// <summary>
        /// Converts this private key to an RSAParameters object
        /// Prevedu PPs z Base64 a vlozim do O RSAParameter, ktere G 
        /// </summary>
        public RSAParameters ToParameters()
        {
            RSAParameters r = new RSAParameters();
            r.Modulus = Convert.FromBase64String(Modulus);
            r.Exponent = Convert.FromBase64String(Exponent);
            r.P = Convert.FromBase64String(PrimeP);
            r.Q = Convert.FromBase64String(PrimeQ);
            r.DP = Convert.FromBase64String(PrimeExponentP);
            r.DQ = Convert.FromBase64String(PrimeExponentQ);
            r.InverseQ = Convert.FromBase64String(Coefficient);
            r.D = Convert.FromBase64String(PrivateExponent);
            return r;
        }

        /// <summary>
        /// Returns *.config file XML section representing this private key
        /// Vratim xx tax Add text argumenty PP Modulus a Exponent
        /// </summary>
        public string ToConfigSection()
        {
            StringBuilder stringBuilder = new StringBuilder();
            // TODO: Nevim zda bych nemel vytvorit novou instanci stringBuilder
            StringBuilder _with3 = stringBuilder;
            _with3.Append(UtilsNonNetStandard.WriteConfigKey(_KeyModulus, Modulus));
            _with3.Append(UtilsNonNetStandard.WriteConfigKey(_KeyExponent, Exponent));
            _with3.Append(UtilsNonNetStandard.WriteConfigKey(_KeyPrimeP, PrimeP));
            _with3.Append(UtilsNonNetStandard.WriteConfigKey(_KeyPrimeQ, PrimeQ));
            _with3.Append(UtilsNonNetStandard.WriteConfigKey(_KeyPrimeExponentP, PrimeExponentP));
            _with3.Append(UtilsNonNetStandard.WriteConfigKey(_KeyPrimeExponentQ, PrimeExponentQ));
            _with3.Append(UtilsNonNetStandard.WriteConfigKey(_KeyCoefficient, Coefficient));
            _with3.Append(UtilsNonNetStandard.WriteConfigKey(_KeyPrivateExponent, PrivateExponent));
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Writes the *.config file representation of this private key to a file
        /// Prepnu A1 2x tagem Add text argumenty PP Modulus a Exponent a dalsi
        /// </summary>
        public void ExportToConfigFile(string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            sw.Write(ToConfigSection());
            sw.Close();
        }

        /// <summary>
        /// Loads the private key from its XML string
        /// Nactu z XML A1 obsahy tagu Modulus a Exponent a dalsi tagy a ulozim je do stejne pojm. VV
        /// </summary>
        public void LoadFromXml(string keyXml)
        {
            Modulus = UtilsNonNetStandard.GetXmlElement(keyXml, Translate.FromKey(XlfKeys.Modulus));
            Exponent = UtilsNonNetStandard.GetXmlElement(keyXml, Translate.FromKey(XlfKeys.Exponent));
            PrimeP = UtilsNonNetStandard.GetXmlElement(keyXml, "P");
            PrimeQ = UtilsNonNetStandard.GetXmlElement(keyXml, "Q");
            PrimeExponentP = UtilsNonNetStandard.GetXmlElement(keyXml, "DP");
            PrimeExponentQ = UtilsNonNetStandard.GetXmlElement(keyXml, "DQ");
            Coefficient = UtilsNonNetStandard.GetXmlElement(keyXml, "InverseQ");
            PrivateExponent = UtilsNonNetStandard.GetXmlElement(keyXml, "D");
        }

        /// <summary>
        /// Converts this private key to its XML string representation
        /// Vrati mi Tagy PP Modulus a Exponent a dalsi v Tagu RSAKeyValue
        /// </summary>
        public string ToXml()
        {
            StringBuilder stringBuilder = new StringBuilder();
            // TODO: Nevim zda bych nemel vytvorit novou instanci stringBuilder
            StringBuilder _with4 = stringBuilder;
            _with4.Append(UtilsNonNetStandard.WriteXmlNode(_ElementParent, false));
            _with4.Append(UtilsNonNetStandard.WriteXmlElement(_ElementModulus, Modulus));
            _with4.Append(UtilsNonNetStandard.WriteXmlElement(_ElementExponent, Exponent));
            _with4.Append(UtilsNonNetStandard.WriteXmlElement(_ElementPrimeP, PrimeP));
            _with4.Append(UtilsNonNetStandard.WriteXmlElement(_ElementPrimeQ, PrimeQ));
            _with4.Append(UtilsNonNetStandard.WriteXmlElement(_ElementPrimeExponentP, PrimeExponentP));
            _with4.Append(UtilsNonNetStandard.WriteXmlElement(_ElementPrimeExponentQ, PrimeExponentQ));
            _with4.Append(UtilsNonNetStandard.WriteXmlElement(_ElementCoefficient, Coefficient));
            _with4.Append(UtilsNonNetStandard.WriteXmlElement(_ElementPrivateExponent, PrivateExponent));
            _with4.Append(UtilsNonNetStandard.WriteXmlNode(_ElementParent, true));
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Writes the Xml representation of this private key to a file
        /// Prepte A1 Tagy PP Modulus a Exponent a dalsi v Tagu RSAKeyValue
        /// </summary>
        public void ExportToXmlFile(string filePath)
        {
            StreamWriter sw = new StreamWriter(filePath, false);
            sw.Write(ToXml());
            sw.Close();
        }
    //public static string FromFile(string p)
    //{
    //    throw new Exception(Translate.FromKey(XlfKeys.TheMethodOrOperationIsNotImplemented) + ".");
    //    return null;
    //}
    }

    /// <summary>
    /// Instantiates a new asymmetric encryption session using the default key size; 
    /// this is usally 1024 bits
    /// Vytvorim obejkt _rsa se kterem budu provadet sifrovaci operace
    /// </summary>
    public Asymmetric()
    {
        rsaCryptoProvider = GetRSAProvider();
    }

    /// <summary>
    /// Instantiates a new asymmetric encryption session using a specific key size
    /// OOP A1 _KeySize a do _rsa vlozim provider M GetRSAProvider. Vytvorim instance pro novou asymetrickou krypt. session text velikostm klice A1
    /// </summary>
    public Asymmetric(int keySize)
    {
        _KeySize = keySize;
        rsaCryptoProvider = GetRSAProvider();
    }

    /// <summary>
    /// Sets the name of the key container used to store this 
    /// key on disk; this is an 
    /// unavoidable side effect of the underlying 
    /// Microsoft CryptoAPI. 
    /// Nastavi jmeno kontejneru na klic ussvansho k uchovani tohoto klice na disku.
    /// Toto je vedlejsi efekt nizkourovnove Microsoft CryptoAPI
    /// </summary>
    /// <remarks>
    /// http://support.microsoft.com/default.aspx?scid=http://support.microsoft.com:80/support/kb/articles/q322/3/71.asp&amp;NoWebContent=1
    /// </remarks>
    public string KeyContainerName
    {
        get
        {
            return _KeyContainerName;
        }

        set
        {
            _KeyContainerName = value;
        }
    }

    /// <summary>
    /// Returns the current key size, in bits
    /// G akt. velikost klice
    /// </summary>
    public int KeySizeBits
    {
        get
        {
            return rsaCryptoProvider.KeySize;
        }
    }

    /// <summary>
    /// Returns the maximum supported key size, in bits
    /// Vratim max. velikost klice v bitech dle _rsa.LegalKeySizes[0]
    /// </summary>
    public int KeySizeMaxBits
    {
        get
        {
            return rsaCryptoProvider.LegalKeySizes[0].MaxSize;
        }
    }

    /// <summary>
    /// Returns the minimum supported key size, in bits
    /// Vratim min. velikost klice v bitech dle _rsa.LegalKeySizes[0]
    /// </summary>
    public int KeySizeMinBits
    {
        get
        {
            return rsaCryptoProvider.LegalKeySizes[0].MinSize;
        }
    }

    /// <summary>
    /// Returns valid key step sizes, in bits
    /// Vratim  velikost kroku v bitech dle _rsa.LegalKeySizes[0]
    /// </summary>
    public int KeySizeStepBits
    {
        get
        {
            return rsaCryptoProvider.LegalKeySizes[0].SkipSize;
        }
    }

    /// <summary>
    /// Returns the default public key as stored in the *.config file
    /// Vratim PublicKey z CM.AS a G
    /// </summary>
    public PublicKey DefaultPublicKey
    {
        get
        {
            PublicKey pubkey = new PublicKey();
            pubkey.LoadFromConfig();
            return pubkey;
        }
    }
}
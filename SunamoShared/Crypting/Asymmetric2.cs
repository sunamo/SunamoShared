namespace SunamoShared.Crypting;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
// Instance variables refactored according to C# conventions
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
    /// Encrypts data using the provided public key
    /// Prevede A2 na parametr, ktery vlozi do _rsa a zasifruje A2.
    /// </summary>
    public DataCrypt Encrypt(DataCrypt d, PublicKey publicKey)
    {
        rsaCryptoProvider.ImportParameters(publicKey.ToParameters());
        return EncryptPrivate(d);
    }

    /// <summary>
    /// Encrypts data using the provided public key as XML
    /// Nacte z xml A2 klic a zasifruje A1
    /// </summary>
    public DataCrypt Encrypt(DataCrypt d, string publicKeyXML)
    {
        LoadKeyXml(publicKeyXML, false);
        return EncryptPrivate(d);
    }

    /// <summary>
    /// Dekryptuje A1, VV pri nezdaru.
    /// </summary>
    /// <param name = "d"></param>
    private DataCrypt EncryptPrivate(DataCrypt d)
    {
        try
        {
            return new DataCrypt(rsaCryptoProvider.Encrypt(d.Bytes, false));
        }
        catch (CryptographicException ex)
        {
            if (ex.Message.ToLower().IndexOf("bad length") > -1)
            {
                throw new Exception(Translate.FromKey(XlfKeys.YourDataIsTooLargeRSAEncryptionIsDesignedToEncryptRelativelySmallAmountsOfDataTheExactByteLimitDependsOnTheKeySizeToEncryptMoreDataUseSymmetricEncryptionAndThenEncryptThatSymmetricKeyWithAsymmetricRSAEncryption) + ".");
            }
            else
            {
                throw;
            }
        }

        return null;
    }

    static Type type = typeof(Asymmetric);
    /// <summary>
    /// Decrypts data using the default private key
    /// Nacte klic z CM.AS a dekryptuje A1 text timto klicem.
    /// </summary>
    public DataCrypt Decrypt(DataCrypt encryptedDataCrypt)
    {
        PrivateKey PrivateKey = new PrivateKey();
        PrivateKey.LoadFromConfig();
        return Decrypt(encryptedDataCrypt, PrivateKey);
    }

    /// <summary>
    /// Decrypts data using the provided private key
    /// Importuji klic A2 jako parametr do _rsa
    /// Dekryptuje A1.
    /// </summary>
    public DataCrypt Decrypt(DataCrypt encryptedDataCrypt, PrivateKey PrivateKey)
    {
        rsaCryptoProvider.ImportParameters(PrivateKey.ToParameters());
        return DecryptPrivate(encryptedDataCrypt);
    }

    /// <summary>
    /// Decrypts data using the provided private key as XML
    /// Nacte klic z xml A2 - pouziva interni .net metodu.
    /// Dekryptuje data A1.
    /// </summary>
    public DataCrypt Decrypt(DataCrypt encryptedDataCrypt, string PrivateKeyXML)
    {
        LoadKeyXml(PrivateKeyXML, true);
        return DecryptPrivate(encryptedDataCrypt);
    }

    /// <summary>
    /// Nactu do O _rsa ze XML A1 net metodou. A2 slouzi k tomu aby se vypsalo ve vyjimce jaky klic se nezdariloi nacist. 
    /// </summary>
    /// <param name = "keyXml"></param>
    /// <param name = "isPrivate"></param>
    private void LoadKeyXml(string keyXml, bool isPrivate)
    {
        try
        {
            rsaCryptoProvider.FromXmlString(keyXml);
        }
        catch ( /*XmlSyntaxException*/Exception ex)
        {
            string text = null;
            if (isPrivate)
            {
                text = "private";
            }
            else
            {
                text = "public";
            }

            throw new Exception(string.Format(Translate.FromKey(XlfKeys.TheProvided0EncryptionKeyXMLDoesNotAppearToBeValid) + ".", text));
        }
    }

    /// <summary>
    /// Dekryptuje data v A1 pomoci RSA
    /// </summary>
    /// <param name = "encryptedDataCrypt"></param>
    private DataCrypt DecryptPrivate(DataCrypt encryptedDataCrypt)
    {
        return new DataCrypt(rsaCryptoProvider.Decrypt(encryptedDataCrypt.Bytes, false));
    }

    /// <summary>
    /// gets the default RSA provider using the specified key size; 
    /// note that Microsoft's CryptoAPI has an underlying file system dependency that is unavoidable
    /// Inicializuji krypt. ttidu RSA text PP a  _KeySize a _KeyContainerName
    /// Klic se bude uchovavat v ulozisti klice PC, nikoliv v uz. profilu
    /// Pokud se nepodari nacist, VV
    /// </summary>
    /// <remarks>
    /// http://support.microsoft.com/default.aspx?scid=http://support.microsoft.com:80/support/kb/articles/q322/3/71.asp&amp;NoWebContent=1
    /// </remarks>
    private RSACryptoServiceProvider GetRSAProvider()
    {
        RSACryptoServiceProvider rsa = null;
        CspParameters csp = null;
        try
        {
            csp = new CspParameters();
            csp.KeyContainerName = _KeyContainerName;
            rsa = new RSACryptoServiceProvider(_KeySize, csp);
            rsa.PersistKeyInCsp = false;
            // Klic se bude uchovavat v ulozisti klice PC, nikoliv v uc. profilu
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            return rsa;
        }
        catch (CryptographicException ex)
        {
            if (ex.Message.ToLower().IndexOf("csp for this implementation could not be acquired") > -1)
            {
                throw new Exception(Translate.FromKey(XlfKeys.UnableToObtainCryptographicServiceProvider) + ". " + Translate.FromKey(XlfKeys.EitherThePermissionsAreIncorrectOnThe) + " 'C:\\Documents and Settings\\All Users\\Application DataCrypt\\Microsoft\\Crypto\\RSA\\MachineKeys' folder, or the current security context '" + WindowsIdentity.GetCurrent().Name + "' does not have access to this folder.");
            }
            else
            {
                throw;
            }
        }
        finally
        {
            if (rsa != null)
            {
                rsa = null;
            }

            if (csp != null)
            {
                csp = null;
            }
        }

        return null;
    }
}
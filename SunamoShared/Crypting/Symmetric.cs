namespace SunamoShared.Crypting;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
// Instance variables refactored according to C# conventions
/// <summary>
/// Symmetric encryption uses a single key to encrypt and decrypt. 
/// Both parties (encryptor and decryptor) must share the same secret key.
/// Symetricke sifrovani pouzivajici jeden klic pro krypt i dekrypt.
/// Obe casti dekryptor i kryptor musi sdilet stejny klic.
/// </summary>
public partial class Symmetric
{
    private const string _DefaultIntializationVector = "%1Az=-@qT";
    private const int _BufferSize = 2048;
    /// <summary>
    /// Provideri symetrickeho sifrovani.
    /// </summary>
    public enum Provider
    {
        /// <summary>
        /// The DataCrypt Encryption Standard provider supports a 64 bit key only
        /// </summary>
        DES,
        /// <summary>
        /// The Rivest Cipher 2 provider supports keys ranging from 40 to 128 bits, default is 128 bits
        /// </summary>
        RC2,
        /// <summary>
        /// The Rijndael (also known as AES) provider supports keys of 128, 192, or 256 bits with a default of 256 bits
        /// </summary>
        Rijndael,
        /// <summary>
        /// The TripleDES provider (also known as 3DES) supports keys of 128 or 192 bits with a default of 192 bits
        /// </summary>
        TripleDES
    }

    private DataCrypt encryptionKey;
    private DataCrypt initializationVector;
    private SymmetricAlgorithm symmetricAlgorithm;
    /// <summary>
    /// IK
    /// </summary>
    private Symmetric()
    {
    }

    /// <summary>
    /// Instantiates a new symmetric encryption object using the specified Provider.
    /// Ulozim do PP _crypto spravnou instanci dle A1. Vygeneruje nahodny klic a pokud !A2, tak i IV. Pokud A2, jako iV nastavim _DefaultIntializationVector 
    /// Automaticky vypocte nahodny klic a ulozi jej do PP Key.
    /// </summary>
    public Symmetric(Provider provider, bool useDefaultInitializationVector)
    {
        switch (provider)
        {
            case Provider.DES:
                symmetricAlgorithm = new DESCryptoServiceProvider();
                break;
            case Provider.RC2:
                symmetricAlgorithm = new RC2CryptoServiceProvider();
                break;
            case Provider.Rijndael:
                symmetricAlgorithm = new RijndaelManaged();
                symmetricAlgorithm.Mode = CipherMode.CBC;
                break;
            case Provider.TripleDES:
                symmetricAlgorithm = new TripleDESCryptoServiceProvider();
                break;
        }

        // - make sure key and IV are always set, no matter what
        Key = RandomKey();
        if (useDefaultInitializationVector)
        {
            IntializationVector = new DataCrypt(_DefaultIntializationVector);
        }
        else
        {
            IntializationVector = RandomInitializationVector();
        }
    }

    /// <summary>
    /// Key size in bytes. We use the default key size for any given provider; if you 
    /// want to force a specific key size, set this property
    /// Velikost klice v bajtech. My pouzivame vychozi velikost klice pro jakohokoliv pouzivaneho providera. Pokud chces nastavit vlastni velikost klice, pouiij tuto VV
    /// Uklada se do PP _crypto a _key, ale neprenes se na zadne bity - leda ze by si to ty objekty delali sami.
    /// </summary>
    public int KeySizeBytes
    {
        get
        {
            return symmetricAlgorithm.KeySize / 8;
        }

        set
        {
            symmetricAlgorithm.KeySize = value * 8;
            encryptionKey.MaxBytes = value;
        }
    }

    /// <summary>
    /// Key size in bits. We use the default key size for any given provider; if you 
    /// want to force a specific key size, set this property
    /// Velikost klice v bajtech. My pouzivame vychozi velikost klice pro jakohokoliv pouzivaneho providera. Pokud chces nastavit vlastni velikost klice, pouzij tuto VV
    /// Uklada se do PP _crypto a _key, ale neprenes se na zadne bajty - leda ze by si to ty objekty delali sami.
    /// </summary>
    public int KeySizeBits
    {
        get
        {
            return symmetricAlgorithm.KeySize;
        }

        set
        {
            symmetricAlgorithm.KeySize = value;
            encryptionKey.MaxBits = value;
        }
    }

    /// <summary>
    /// The key used to encrypt/decrypt data
    /// GS klic. Pri S nastavim Min Max a Step hodnoty z _crypto.LegalKeySizes[0] prevede na byty(/8)
    /// </summary>
    public DataCrypt Key
    {
        get
        {
            return encryptionKey;
        }

        set
        {
            encryptionKey = value;
            encryptionKey.MaxBytes = symmetricAlgorithm.LegalKeySizes[0].MaxSize / 8;
            encryptionKey.MinBytes = symmetricAlgorithm.LegalKeySizes[0].MinSize / 8;
            encryptionKey.StepBytes = symmetricAlgorithm.LegalKeySizes[0].SkipSize / 8;
        }
    }

    /// <summary>
    /// Using the default Cipher Block Chaining (CBC) mode, all data blocks are processed using
    /// the value derived from the previous block; the first data block has no previous data block
    /// to use, so it needs an InitializationVector to feed the first block
    /// GS PP. Pri S ulozim do IV do Min a Max Bytes stejnou hodnotu - osminu _crypto.BlockSize(cili defakto velikost bloku
    /// </summary>
    public DataCrypt IntializationVector
    {
        get
        {
            return initializationVector;
        }

        set
        {
            initializationVector = value;
            initializationVector.MaxBytes = symmetricAlgorithm.BlockSize / 8;
            initializationVector.MinBytes = symmetricAlgorithm.BlockSize / 8;
        }
    }

    /// <summary>
    /// generates a random Initialization Vector, if one was not provided
    /// G nahodne IV
    /// </summary>
    public DataCrypt RandomInitializationVector()
    {
        symmetricAlgorithm.GenerateIV();
        DataCrypt d = new DataCrypt(symmetricAlgorithm.IV);
        return d;
    }

    /// <summary>
    /// generates a random Key, if one was not provided
    /// Vygeneruje nahodny klic O _crypto a vratim jej v O DataCrypt
    /// </summary>
    public DataCrypt RandomKey()
    {
        symmetricAlgorithm.GenerateKey();
        DataCrypt d = new DataCrypt(symmetricAlgorithm.Key);
        return d;
    }

    static Type type = typeof(Symmetric);
    /// <summary>
    /// Ensures that _crypto object has valid Key and IV
    /// prior to any attempt to encrypt/decrypt anythingv
    /// A2 zda se kryptuje. Pokud _key.IsEmpty a A2, vygeneruji do _crypto.Key nahodny klic, jinak VV. To same jako s klicem i s IV
    /// </summary>
    private void ValidateKeyAndIv(bool isEncrypting)
    {
        if (encryptionKey.IsEmpty)
        {
            if (isEncrypting)
            {
                encryptionKey = RandomKey();
            }
            else
            {
                throw new Exception(Translate.FromKey(XlfKeys.NoKeyWasProvidedForTheDecryptionOperation) + "!");
            }
        }

        if (initializationVector.IsEmpty)
        {
            if (isEncrypting)
            {
                initializationVector = RandomInitializationVector();
            }
            else
            {
                throw new Exception(Translate.FromKey(XlfKeys.NoInitializationVectorWasProvidedForTheDecryptionOperation) + "!");
            }
        }

        symmetricAlgorithm.Key = encryptionKey.Bytes;
        symmetricAlgorithm.IV = initializationVector.Bytes;
    }

    /// <summary>
    /// Encrypts the specified DataCrypt using provided key
    /// Zakoduuji data A1 s klicem A2. A2 OOP.
    /// </summary>
    public DataCrypt Encrypt(DataCrypt d, DataCrypt key)
    {
        Key = key;
        return Encrypt(d);
    }

    /// <summary>
    /// Encrypts the specified DataCrypt using preset key and preset initialization vector
    /// Zkontroluji platnost klice a IV a pokud nebudou platne, vygeneruji je. 
    /// Zakryptuji A1 objektem _crypto a G
    /// </summary>
    public DataCrypt Encrypt(DataCrypt d)
    {
        MemoryStream ms = new MemoryStream();
        ValidateKeyAndIv(true);
        CryptoStream cs = new CryptoStream(ms, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(d.Bytes, 0, d.Bytes.Length);
        cs.Close();
        ms.Close();
        return new DataCrypt(ms.ToArray());
    }

    /// <summary>
    /// Encrypts the stream to memory using provided key and provided initialization vector
    /// Zakryptuji proud A1 s klicem A2 a IV A3. A2,3 OOP
    /// </summary>
    public DataCrypt Encrypt(Stream s, DataCrypt key, DataCrypt iv)
    {
        IntializationVector = iv;
        Key = key;
        return Encrypt(s);
    }

    /// <summary>
    /// Encrypts the stream to memory using specified key
    /// Zakryptuji proud A1 s klicem A1. A1 OOP
    /// </summary>
    public DataCrypt Encrypt(Stream s, DataCrypt key)
    {
        Key = key;
        return Encrypt(s);
    }
}
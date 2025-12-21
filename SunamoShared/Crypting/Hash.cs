namespace SunamoShared.Crypting;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
// Instance variables refactored according to C# conventions
/// <summary>
/// Hash functions are fundamental to modern cryptography. These functions map binary 
/// strings of an arbitrary length to small binary strings of a fixed length, known as 
/// hash values. A cryptographic hash function has the property that it is computationally
/// infeasible to find two distinct inputs that hash to the same value. Hash functions 
/// are commonly used with digital signatures and for data integrity.
/// </summary>
public partial class Hash
{
    /// <summary>
    /// Type of hash; some are security oriented, others are fast and simple
    /// Vycet moznych hashovacich provideru v .NETu
    /// </summary>
    public enum Provider
    {
        /// <summary>
        /// Cyclic Redundancy Check provider, 32-bit
        /// </summary>
        CRC32,
        /// <summary>
        /// Secure Hashing Algorithm provider, SHA-1 variant, 160-bit
        /// </summary>
        SHA1,
        /// <summary>
        /// Secure Hashing Algorithm provider, SHA-2 variant, 256-bit
        /// </summary>
        SHA256,
        /// <summary>
        /// Secure Hashing Algorithm provider, SHA-2 variant, 384-bit
        /// </summary>
        SHA384,
        /// <summary>
        /// Secure Hashing Algorithm provider, SHA-2 variant, 512-bit
        /// </summary>
        SHA512,
        /// <summary>
        /// Message Digest algorithm 5, 128-bit
        /// </summary>
        MD5
    }

    /// <summary>
    /// Trida pro pocitani Hashe
    /// </summary>
    private HashAlgorithm hashAlgorithm;
    /// <summary>
    /// Naposledy vypocitani Hash
    /// </summary>
    private DataCrypt hashValue = new DataCrypt();
    /// <summary>
    /// IK
    /// </summary>
    private Hash()
    {
    }

    /// <summary>
    /// Instantiate a new hash of the specified type
    /// </summary>
    public Hash(Provider p)
    {
        switch (p)
        {
            case Provider.CRC32:
                hashAlgorithm = new CRC32();
                break;
            case Provider.MD5:
                hashAlgorithm = new MD5CryptoServiceProvider();
                break;
            case Provider.SHA1:
                hashAlgorithm = new SHA1Managed();
                break;
            case Provider.SHA256:
                hashAlgorithm = new SHA256Managed();
                break;
            case Provider.SHA384:
                hashAlgorithm = new SHA384Managed();
                break;
            case Provider.SHA512:
                hashAlgorithm = new SHA512Managed();
                break;
        }
    }

    /// <summary>
    /// Returns the previously calculated hash
    /// G vypocitanou hodnotu hash.
    /// </summary>
    public DataCrypt Value
    {
        get
        {
            return hashValue;
        }
    }

    /// <summary>
    /// Calculates hash on a stream of arbitrary length
    /// Vypocity hash ze streamu A1.
    /// </summary>
    public DataCrypt Calculate(ref Stream s)
    {
        hashValue.Bytes = hashAlgorithm.ComputeHash(s);
        return hashValue;
    }

    /// <summary>
    /// Calculates hash for fixed length <see cref = "DataCrypt"/>
    /// Vypocitam hash z A1 M CalculatePrivate a G
    /// </summary>
    public DataCrypt Calculate(DataCrypt d)
    {
        return CalculatePrivate(d.Bytes);
    }

    /// <summary>
    /// Calculates hash for a string with a prefixed salt value. 
    /// A "salt" is random data prefixed to every hashed value to prevent 
    /// common dictionary attacks.
    /// VLozim do noveho pole o velikost A1+A2 nejdrive A2 a hned za nim A1. Vypocitam Hash M CalculatePrivate
    /// Private znamena ze ulozim vysledek do privatni PP _HashValue
    /// </summary>
    public DataCrypt Calculate(DataCrypt d, DataCrypt salt)
    {
        byte[] nb = new byte[d.Bytes.Length + salt.Bytes.Length];
        salt.Bytes.CopyTo(nb, 0);
        d.Bytes.CopyTo(nb, salt.Bytes.Length);
        return CalculatePrivate(nb);
    }

    /// <summary>
    /// Calculates hash for an array of bytes
    /// Vypocitam Hash tridou HashAlgorithm, ulozim do _HashValue a G
    /// </summary>
    private DataCrypt CalculatePrivate(byte[] b)
    {
        hashValue.Bytes = hashAlgorithm.ComputeHash(b);
        return hashValue;
    }
}
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
    /// <summary>
    /// Encrypts the specified stream to memory using preset key and preset initialization vector
    /// Zasifruje proud A1. Pokud neude platny Iv nebo key, vygeneruji novy.
    /// </summary>
    public DataCrypt Encrypt(Stream s)
    {
        MemoryStream ms = new MemoryStream();
        byte[] b = new byte[_BufferSize + 1];
        int i = 0;
        ValidateKeyAndIv(true);
        CryptoStream cs = new CryptoStream(ms, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write);
        i = s.Read(b, 0, _BufferSize);
        while (i > 0)
        {
            cs.Write(b, 0, i);
            i = s.Read(b, 0, _BufferSize);
        }

        cs.Close();
        ms.Close();
        return new DataCrypt(ms.ToArray());
    }

    /// <summary>
    /// Decrypts the specified data using provided key and preset initialization vector
    /// Dekryptuje data A1 s klicem A2 ktery OOP
    /// </summary>
    public DataCrypt Decrypt(DataCrypt encryptedDataCrypt, DataCrypt key)
    {
        Key = key;
        return Decrypt(encryptedDataCrypt);
    }

    /// <summary>
    /// Decrypts the specified stream using provided key and preset initialization vector
    /// Dekryptuje proud A1 s klicem A2. A2 OOP
    /// </summary>
    public DataCrypt Decrypt(Stream encryptedStream, DataCrypt key)
    {
        Key = key;
        return Decrypt(encryptedStream);
    }

    /// <summary>
    /// Decrypts the specified stream using preset key and preset initialization vector
    /// Dekryptuje A1. Pokud klic a IV nebyly zadany, a budou prazdne, VV
    /// </summary>
    public DataCrypt Decrypt(Stream encryptedStream)
    {
        MemoryStream ms = new MemoryStream();
        byte[] b = new byte[_BufferSize + 1];
        ValidateKeyAndIv(false);
        CryptoStream cs = new CryptoStream(encryptedStream, symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Read);
        int i = 0;
        i = cs.Read(b, 0, _BufferSize);
        while (i > 0)
        {
            ms.Write(b, 0, i);
            i = cs.Read(b, 0, _BufferSize);
        }

        cs.Close();
        ms.Close();
        return new DataCrypt(ms.ToArray());
    }

    /// <summary>
    /// Decrypts the specified data using preset key and preset initialization vector
    /// Dekryptuje data A1. Musi byt zadan platny klic a IV.
    /// </summary>
    public DataCrypt Decrypt(DataCrypt encryptedDataCrypt)
    {
        MemoryStream ms = new MemoryStream(encryptedDataCrypt.Bytes, 0, encryptedDataCrypt.Bytes.Length);
        byte[] b = new byte[encryptedDataCrypt.Bytes.Length];
        ValidateKeyAndIv(false);
        CryptoStream cs = new CryptoStream(ms, symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Read);
        try
        {
            cs.Read(b, 0, encryptedDataCrypt.Bytes.Length - 1);
        }
        catch (CryptographicException ex)
        {
            throw new Exception(Translate.FromKey(XlfKeys.UnableToDecryptDataTheProvidedKeyMayBeInvalid) + "." + Exceptions.TextOfExceptions(ex));
        }
        finally
        {
            cs.Close();
        }

        return new DataCrypt(b);
    }
}
using System.Text;

namespace EncryptApp;

public static class CipherService
{
    public static string Encrypt(string text, string password)
    {
        StringBuilder result = new();

        for (int i = 0; i < text.Length; i++)
        {
            char textChar = text[i];
            char passChar = password[i % password.Length];

            if (!SymbolTable.IsSupported(textChar))
            {
                result.Append(textChar);
                continue;
            }

            int textNum = SymbolTable.GetNumber(textChar);
            int passNum = SymbolTable.GetNumber(passChar);

            int encryptedNum = (textNum + passNum) % 64;
            if (encryptedNum == 0) encryptedNum = 64;

            char encryptedChar = SymbolTable.GetChar(encryptedNum);
            result.Append(encryptedChar);
        }

        return result.ToString();
    }
}
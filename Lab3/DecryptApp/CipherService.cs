using System.Text;

namespace DecryptApp;

public static class CipherService
{
    public static string Decrypt(string text, string password)
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

            int decryptedNum = (textNum - passNum) % 64;
            if (decryptedNum <= 0) decryptedNum += 64;

            char decryptedChar = SymbolTable.GetChar(decryptedNum);
            result.Append(decryptedChar);
        }

        return result.ToString();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        public string Encrypt(string plainText, int key)
        {
            string cText = "";
            for (int i = 0; i < plainText.Length; i++)
            {   
                char PT = plainText[i];
                if (PT >= 'a')
                {
                    int PTind = PT - 'a';
                    int CTind = (PTind + key) % 26;
                    char CT = (char)(CTind + 'a');
                    cText += CT;
                }
                else if (PT >= 'A')
                {
                    int PTind = PT - 'A';
                    int CTind = (PTind + key) % 26;
                    char CT = (char)(CTind + 'A');
                    cText += CT;
                }
            }
            return cText;
        }

        public string Decrypt(string cipherText, int key)
        {
            return Encrypt(cipherText, 26 - key);
        }

        public int Analyse(string plainText, string cipherText)
        {
            //throw new NotImplementedException();
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            int key = cipherText[0] - plainText[0];
            if (key < 0)
                key += 26;
            return key ;
        }
    }
}

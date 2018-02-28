using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            //throw new NotImplementedException();
            Dictionary<char, char> myDic = new Dictionary<char, char>();
            Stack<char> mystack = new Stack<char>();

            bool[] vis = new bool[150];
            
            string p = plainText.ToLower();
            string c = cipherText.ToLower();
            

            for (char i = 'a'; i <= 'z'; i++)
            {
                myDic[i] = '.';
            }
            for (int i = 0; i < p.Length; i++)
            {
                myDic[p[i]] = c[i];
                vis[c[i]] = true;
            }


            for (char i = 'a'; i <= 'z'; i++)
            {
                if (vis[i].Equals(false))
                {
                    mystack.Push(i);
                }
            }
            string key = "";

            for (char i = 'a'; i <= 'z'; i++)
            {
                if (myDic[i].Equals('.'))
                    key += mystack.Pop();
                else
                    key += myDic[i];
            }
            return key;
        }

        public string Decrypt(string cipherText, string key)
        {
            //throw new NotImplementedException();
            string cText = "";
            cipherText = cipherText.ToLower();
            for (int i = 0; i < cipherText.Length; i++){
                for (int j = 0; j < 26; j++)
                {
                    if (cipherText[i].Equals(key[j]))
                    {
                        cText += (char)('a' + j);
                        break;
                    }
                }
            }
            return cText;
        }

        public string Encrypt(string plainText, string key)
        {
            //throw new NotImplementedException();
            string cText = "";
            for (int i = 0; i < plainText.Length; i++){
                cText += (char)key[plainText[i] - 'a'];
            }
            return cText;
        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            Dictionary<char, double> myDic = new Dictionary<char, double>();
            myDic['E'] = 12.51;
            myDic['T'] = 9.25 ;
            myDic['A'] = 8.04 ;
            myDic['O'] = 7.60 ;
            myDic['I'] = 7.26 ;
            myDic['N'] = 7.09 ;
            myDic['S'] = 6.54 ;
            myDic['R'] = 6.12 ;
            myDic['H'] = 5.49 ;
            myDic['L'] = 4.14 ;
            myDic['D'] = 3.99 ;
            myDic['C'] = 3.06 ;
            myDic['U'] = 2.71 ;
            myDic['M'] = 2.53 ;
            myDic['F'] = 2.30 ;
            myDic['P'] = 2.00 ;
            myDic['G'] = 1.96 ;
            myDic['W'] = 1.92 ;
            myDic['Y'] = 1.73 ;
            myDic['B'] = 1.54 ;
            myDic['V'] = 0.99 ;
            myDic['K'] = 0.67 ;
            myDic['X'] = 0.19 ;
            myDic['J'] = 0.16 ;
            myDic['Q'] = 0.11 ;
            myDic['Z'] = 0.09 ;

            Dictionary<char,double> myCDic = new Dictionary<char,double>();
            for (char i = 'A'; i <= 'Z'; i++)
                myCDic[i] = 0.0;
            for (int i = 0; i < cipher.Length; i++)
            {
                myCDic[cipher[i]] += 1.0;
            }
            for (char i = 'A'; i <= 'Z'; i++)
            {
                myCDic[i] = (myCDic[i]/(double)cipher.Length)*100.0;
            }

            List<char> sDic = myCDic.OrderByDescending(i => i.Value).Select(j => j.Key).ToList();
            List<char> mDic = myDic.OrderByDescending(i => i.Value).Select(k => k.Key).ToList();
            
            Dictionary<char,char> key = new Dictionary<char,char>();

            for (int i = 0; i < mDic.Count; i++)
                key[sDic[i]] = mDic[i];
            string o = "";
            for (int i = 0; i < cipher.Length; i++)
            {
                o += key[cipher[i]];
            }
            return o;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographicTechnique<string, string>
    {
        public string Decrypt(string cipherText, string key)
        {
            //throw new NotImplementedException();
            string p = cipherText.ToLower();
            string o = "";
            bool[] vis = new bool[150];
            char[,] playfair = new char[5, 5];

            Queue<char> myq = new Queue<char>();
            for (int k = 0; k < key.Length; k++)
            {
                if (!vis[key[k]])
                {
                    if (key[k] == 'i' && vis['j'])
                    {
                        vis[key[k]] = true;
                        continue;
                    }
                    if (key[k] == 'j' && vis['i'])
                    {
                        vis[key[k]] = true;
                        continue;
                    }
                    myq.Enqueue(key[k]);
                    vis[key[k]] = true;
                }
            }
            for (char z = 'a'; z <= 'z'; z++)
            {
                if (!vis[z])
                {
                    if (z == 'i')
                    {
                        if (vis['j'])
                            continue;
                        vis[z] = true;
                    }
                    if (z == 'j')
                    {
                        if (vis['i'])
                            continue;
                        vis[z] = true;
                    }
                    myq.Enqueue(z);
                }
            }
            Dictionary<char, KeyValuePair<int, int>> myDic = new Dictionary<char, KeyValuePair<int, int>>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    char temp = myq.Dequeue();
                    myDic[temp] = new KeyValuePair<int, int>(i, j);
                    playfair[i, j] = temp;
                }
            }
            //for (int i = 0; i < 5; i++)
            //{
            //    for (int j = 0; j < 5; j++)
            //    {
            //        Console.Write(playfair[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();

            if (myDic.ContainsKey('i'))
                myDic['j'] = myDic['i'];
            else if (myDic.ContainsKey('j'))
                myDic['i'] = myDic['j'];

            //--------------------------------------------------

            for (int k = 0; k < p.Length - 1; k += 2)
            {
                int Fi = myDic[p[k]].Key;
                int Fj = myDic[p[k]].Value;

                int Si = myDic[p[k + 1]].Key;
                int Sj = myDic[p[k + 1]].Value;


                if (Fi == Si)//same Row
                {
                    o += playfair[Fi, ((Fj - 1) + 5) % 5];
                    o += playfair[Si, ((Sj - 1) + 5) % 5];
                }
                else if (Fj == Sj)//same Column
                {
                    o += playfair[((Fi - 1) + 5) % 5, Fj];
                    o += playfair[((Si - 1) + 5) % 5, Sj];
                }
                else//Corners
                {
                    o += playfair[Fi, Sj];
                    o += playfair[Si, Fj];
                }
            }

            if (o.Length % 2 == 0 && o[o.Length - 1] == 'x')
                o = o.Remove(o.Length - 1, 1);

            for (int i = 0; i < o.Length - 2; i += 2)
            {
                if (o[i] == o[i+2] && o[i+1]=='x')
                {
                    o = o.Remove(i+1, 1);
                    i--;
                }
            }
            
            return o;        
        }
        public string Encrypt(string plainText, string key)
        {
            //throw new NotImplementedException();
            string p = plainText.ToLower();
            string o = "";
            bool[] vis = new bool[150];
            char[,] playfair = new char[5, 5];
            for (int k = 0; k < p.Length - 1; k += 2)
            {
                if (p[k] == p[k + 1])
                    p = p.Insert((k + 1), "x");
            }
            if (p.Length % 2 == 1)
                p += 'x';

            Console.WriteLine("P.T : " + p);

            Queue<char> myq = new Queue<char>();
            for (int k = 0; k < key.Length; k++)
            {
                if (!vis[key[k]])
                {
                    if (key[k] == 'i' && vis['j'])
                    {
                        vis[key[k]] = true;
                        continue;
                    }
                    if (key[k] == 'j' && vis['i'])
                    {
                        vis[key[k]] = true;
                        continue;
                    }
                    myq.Enqueue(key[k]);
                    vis[key[k]] = true;
                }
            }
            for (char z = 'a'; z <= 'z'; z++)
            {
                if (!vis[z])
                {
                    if (z == 'i')
                    {
                        if (vis['j'])
                            continue;
                        vis[z] = true;
                    }
                    if (z == 'j')
                    {
                        if (vis['i'])
                            continue;
                        vis[z] = true;
                    }
                    myq.Enqueue(z);
                }
            }
            Dictionary<char, KeyValuePair<int, int>> myDic = new Dictionary<char, KeyValuePair<int, int>>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    char temp = myq.Dequeue();
                    myDic[temp] = new KeyValuePair<int, int>(i, j);
                    playfair[i, j] = temp;
                }
            }
            //for (int i = 0; i < 5; i++)
            //{
            //    for (int j = 0; j < 5; j++)
            //    {
            //        Console.Write(playfair[i, j]+" ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();

            if (myDic.ContainsKey('i'))
                myDic['j'] = myDic['i'];
            else if (myDic.ContainsKey('j'))
                myDic['i'] = myDic['j'];

            //--------------------------------------------------
            for (int k = 0; k < p.Length - 1; k += 2)
            {
                int Fi = myDic[p[k]].Key;
                int Fj = myDic[p[k]].Value;

                int Si = myDic[p[k + 1]].Key;
                int Sj = myDic[p[k + 1]].Value;


                if (Fi == Si)//same Row
                {
                    o += playfair[Fi, (Fj + 1) % 5];
                    o += playfair[Si, (Sj + 1) % 5];
                }
                else if (Fj == Sj)//same Column
                {
                    o += playfair[(Fi + 1) % 5, Fj];
                    o += playfair[(Si + 1) % 5, Sj];
                }
                else//Corners
                {
                    o += playfair[Fi, Sj];
                    o += playfair[Si, Fj];
                }
            }
            return o;
        
        }
        public string Analyse(string plainText, string key)
        {
            throw new NotImplementedException();
        }
    }
}

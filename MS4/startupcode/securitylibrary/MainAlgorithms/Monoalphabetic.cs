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
            cipherText = cipherText.ToLower();
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            char[] key = new char[26];
            bool[] check_key = new bool[26];
            int[] aray_of_ascii = new int[1000];
            for (int i = 0; i < plainText.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (plainText[i] == alphabet[j])
                    {
                        key[j] = cipherText[i];
                        check_key[j] = true;
                        aray_of_ascii[(int)cipherText[i]] = 1;
                    }
                }
            }

            for (int i = 0; i < alphabet.Length; i++)
            {
                if (check_key[i] == false)
                {
                    for (int j = 0; j < alphabet.Length; j++)
                    {
                        if (aray_of_ascii[97 + j] != 1)
                        {
                            key[i] = (char)(97 + j);
                            check_key[i] = true;
                            aray_of_ascii[97 + j] = 1;
                            break;
                        }
                    }
                }
            }
            string ret = "";
            for (int r = 0; r < key.Length; r++)
            {
                ret += (char)key[r];
            }
            // Console.WriteLine(key);
            return ret;
        }
        public string Decrypt(string cipherText, string key)
        {
            string ciphe = cipherText.ToLower();
            //throw new NotImplementedException();
            char[] plaint = new char[cipherText.Length];
            char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            for (int i = 0; i < cipherText.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (ciphe[i] == key[j])
                    {
                        plaint[i] = alphabet[j];
                    }
                }
            }
            return new string(plaint);
        }

        public string Encrypt(string plainText, string key)
        {
            //throw new NotImplementedException();
            char[] citext = new char[plainText.Length];
            char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            for (int i = 0; i < plainText.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (plainText[i] == alphabet[j])
                    {
                        citext[i] = key[j];
                    }
                }
            }
            return new string(citext);
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
        struct maker
        {
            public int counter;
            public char myalpha;
            public char secalpha;
        }
        maker[] arr = new maker[26];
        public string AnalyseUsingCharFrequency(string cipher)
        {
            //throw new NotImplementedException();
            string freq = "etaoinsrhldcumfpgwybvkxjqz";
            char[] plaine = new char[cipher.Length];
            string ciphe = cipher.ToLower();
            for (int i = 0; i < 26; i++)
            {
                int ind = 97 + i;
                arr[i].myalpha = (char)ind;
            }
            for (int j = 0; j < cipher.Length; j++)
            {
                for (int l = 0; l < 26; l++)
                {
                    if (arr[l].myalpha == ciphe[j])
                    {
                        arr[l].counter++;
                    }
                }
            }
            for (int z = 0; z < 26; z++)
            {
                for (int k = z + 1; k < 26; k++)
                {
                    if (arr[z].counter < arr[k].counter)
                    {
                        char temp = arr[z].myalpha;
                        int temp2 = arr[z].counter;
                        arr[z].myalpha = arr[k].myalpha;
                        arr[z].counter = arr[k].counter;
                        arr[k].myalpha = temp;
                        arr[k].counter = temp2;
                    }
                }
            }
            for (int m = 0; m < 26; m++)
            {
                arr[m].secalpha = freq[m];
            }
            for (int n = 0; n < cipher.Length; n++)
            {
                for (int v = 0; v < 26; v++)
                {
                    if (ciphe[n] == arr[v].myalpha)
                    {
                        plaine[n] = arr[v].secalpha;
                    }
                }
            }
            return new string(plaine);
        }
    }
}
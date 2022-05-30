using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public List<int> Analyse(string plainText, string cipherText)
        {
            //throw new NotImplementedException();
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            int n = 0, breaker = 0;
            int pl = plainText.Length;
            for (int i = 0; i < pl; i++)//to know number of items in list
            {
                if (cipherText[0] == plainText[i])
                {
                    int j = i + 1;
                    while (j < cipherText.Length)
                    {
                        if (cipherText[1] == plainText[j])
                        {
                            int k = j + 1;
                            while (k < cipherText.Length)
                            {
                                if (k - j > j - i)
                                {
                                    break;
                                }
                                else if (cipherText[2] == plainText[k] && k - j == j - i)
                                {
                                    n = j - i;
                                    breaker = 1;
                                    break;
                                }
                                k++;

                            }
                        }
                        j++;
                        if (breaker == 1)
                            break;
                    }
                }
                if (breaker == 1)
                    break;
            }
            int columns = n;
            List<int> key = new List<int>(columns);
            int rows = (int)Math.Ceiling(plainText.Length / (float)n);
            char[,] table = new char[rows, columns];
            int counter0 = 0;
            for (int r = 0; r < rows; r++)//putting plaintext into matrix
            {
                for (int c = 0; c < columns; c++)
                {
                    if (counter0 < plainText.Length)
                    {
                        table[r, c] = plainText[counter0];
                        counter0++;
                    }
                    else
                    {
                        table[r, c] = 'x';
                    }
                }
            }
            for (int i = 0; i < columns; i++)
            {
                int checker = 0;
                int pointer = 0;
                int counter = 2;
                for (int j = 0; j < rows; j++)
                {

                    if ((pointer >= cipherText.Length || table[j, i] == cipherText[pointer]))
                    {
                        checker++;
                        if (checker >= rows)//to check that 3 letters is correct
                        { key.Add((int)Math.Ceiling(pointer / (float)rows)); break; }
                        pointer++;

                    }
                    else
                    {
                        j = -1;
                        int counterinc = counter++;
                        pointer = counterinc * rows - rows;
                    }
                }
            }
            return key;

        }

        public string Decrypt(string cipherText, List<int> key)
        {
            // throw new NotImplementedException();
            int columns = key.Count;
            int rows = cipherText.Length / columns;
            char[,] table = new char[rows, columns];
            string plain = "";
            int counter = 1, count2 = 0;
            for (int c = 0; c < columns; c++)
            {
                if (counter == key[c] && counter <= key.Count)
                {
                    for (int r = 0; r < rows; r++)
                    {
                        if (count2 <= cipherText.Length)
                        {
                            table[r, c] = cipherText[count2];
                            count2++;
                        }
                    }
                    counter++;
                    c = -1;
                }
            }
            for (int i = 0; i < rows; i++)
            {
                for (int z = 0; z < columns; z++)
                {
                    plain += table[i, z];
                }
            }
            return plain.ToUpper();
        }

        public string Encrypt(string plainText, List<int> key)
        {
            int columns = key.Count;
            int rows = (int)Math.Ceiling((double)plainText.Length / columns);
            char[,] table = new char[rows, columns];
            string ciphe = "";
            int mycount = key.Count;
            int counter = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (counter < plainText.Length)
                    {
                        table[r, c] = plainText[counter];
                        counter++;
                    }
                    else
                    {
                        table[r, c] = 'x';
                    }
                }
            }
            int m = 1;
            for (int i = 0; i < columns; i++)
            {
                if (m == key[i] && m <= key.Count)
                {
                    for (int z = 0; z < rows; z++)
                    {
                        ciphe += table[z, i];
                    }
                    m++;
                    i = -1;
                }
            }
            return ciphe.ToUpper();
        }
    }
}
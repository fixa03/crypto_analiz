using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using table_viginer;

namespace lib
{

    public struct grams
    {
        public string name;
        public int distance;
    };

    public struct word
    {
        public char symbol;
        public int cols;
    };

    public struct find_word
    {
        public string text;
        public word[] words;
    };

    
    class Class1
    {
        const string alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
        const char word_key = 'о';
        private string text;
        private table_v table;

        public Class1(string text)
        {
            this.text = text.ToLower();
            table = new table_v(alphabet);
        }

        private void find_grams(string text, ref List<grams> finded)
        {
            char[] symbols = {' ', '.', ',', '!', '?' };
            int l = 1;
            int k = 0, count = 0;
            string part = new String(text.ToCharArray(), 0, 2);
            while (l < text.Length - 2)
            {

                if (Array.Exists(symbols, x => x == text[l]))
                {
                    l++;
                    part = new String(text.ToCharArray(), l, 2);

                    continue;
                }

                count = new Regex(part).Matches(text).Count;

                if (count >= 2)
                    while (!Array.Exists(symbols, x => x == text[l + 1]) && new Regex(part + text[l + 1].ToString()).Matches(text).Count >= 2)
                    {
                        l++;
                        part += text[l];
                        count = new Regex(part).Matches(text).Count;
                    }

                if (count >= 2 && !finded.Exists(x => x.name == part) && part.Length >= 2)
                {
                    grams temp; temp.name = part; temp.distance = text.LastIndexOf(part) - text.IndexOf(part);
                    Console.WriteLine(part + ": " + temp.distance.ToString());
                    finded.Add(temp);
                }

                {
                    part = new String(text.ToCharArray(), l, 2);
                    l++;
                }

            }


            

            //print
            //for (int i = 0; i < finded.Count; i++)
            //{
            //    Console.WriteLine(finded[i].name + ":\t" + finded[i].distance);
            //}

        }

        private void nod(int number,ref List<int> array)
        {
            while (number != 1)
            {
                int del = 2;
                while (number % del != 0) del++;
                number /= del;
                if(!array.Contains(del)) array.Add(del);
            }

        }

        private int search_len()
        {

            List<grams> finded = new List<grams>();
            find_grams(text, ref finded);

            finded = finded.OrderByDescending(x => x.distance).ToList();

            List<int> union = new List<int>();
            nod(finded.First().distance, ref union);

            List<int> current = new List<int>();

            for (int i = 0; i < union.Count; i++)
                Console.Write(union[i] + " ");
            Console.WriteLine();

            for (int i = 1; i < finded.Count; i++)
            {
                nod(finded[i].distance, ref current);
                union = union.Intersect(current).ToList();
                for (int j = 0; j < current.Count; j++)
                    Console.Write(current[j] + " ");
                Console.WriteLine();
            }

            int res = 1;
            foreach (int n in union)
                res *= n;

            return res;
        }

        private string text_n(int n, int p, string text)
        {
            string res = String.Empty;

            for (int i = p; i < text.Length; i += n + p)
            {
                res += text[i];
            }

            return res;
        }

        private void search_word(string text, ref find_word[] array)
        {
            int n = search_len();

            array = new find_word[n];
            char[] words;
            word[] array_words;

            for (int j = 0; j < n; j++)
            {
                array[j].text = text_n(n - j, j, text);
                words = array[j].text.Distinct().ToArray();
                array_words = new word[words.Length];

                for (int i = 0; i < words.Length; i++)
                {
                    array_words[i].symbol = words[i];
                    array_words[i].cols = array[j].text.Count(x => x == array_words[i].symbol);
                }

                array[j].words = array_words.OrderByDescending(x => x.cols).Where(x => x.cols >= array_words.Max(x => x.cols) - 1).ToArray();

                for (int i = 0; i < array[j].words.Count(); i++)
                {
                    array[j].words[i].symbol = table.decrypt_word(array[j].words[i].symbol, word_key);
                }
                
                //array_words = array_words.Where(x => x.cols >= array_words.Max(x => x.cols) - 1).ToArray();
                //ThenBy(x => x.cols >= array_words.Max(x => x.cols) - 1)
            }



        }

        public void decrypt()
        {
            find_word[] array = null;
            search_word(text, ref array);

            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(i + ":");
                for (int j = 0; j < array[i].words.Length; j++)
                    Console.Write("\t" + array[i].words[j].symbol);
                Console.WriteLine();
            }
        }
    }
}



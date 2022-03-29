using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace lib
{
    class Class1
    {

        struct grams
        {
            public string name;
            public int distance;
        };

        public void find_grams(string text, ref List<grams> finded)
        {
            int l = 1;
            int k = 0, count = 0;
            string part = new String(text.ToCharArray(), 0, 2);
            while (l < text.Length - 1)
            {
                count = new Regex(part).Matches(text).Count;

                if (count >= 2)
                    while (new Regex(part + text[l + 1].ToString()).Matches(text).Count >= 2)
                    {
                        l++;
                        part += text[l];
                        count = new Regex(part).Matches(text).Count;
                    }

                if (count >= 2 && !finded.Exists(x => x.name == part) && part.Length >= 4)
                {
                    grams temp; temp.name = part; temp.distance = text.LastIndexOf(part) - text.IndexOf(part);
                    finded.Add(temp);
                }

                {
                    part = new String(text.ToCharArray(), l, 2);
                    l++;
                }

            }

            for (int i = 0; i < finded.Count; i++)
            {
                Console.WriteLine(finded[i].name + ":\t" + finded[i].distance);
            }

            //for (int i = 0; i < text.Length - 1 ; i += 2)
            //{
            //    int k = 1;
            //    string gram = text[i].ToString() + text[i+k].ToString();
            //    int cols = new Regex(gram).Matches(text).Count;

            //    while (cols >= 2)
            //    {
            //        k++;
            //        if (new Regex(gram + text[i + k].ToString()).Matches(text).Count < 2) break;
            //        gram += text[i + k].ToString();
            //        cols = new Regex(gram).Matches(text).Count;

            //    }

            //    if (gram.Length > 4)
            //    {
            //        grams temp; temp.name = gram; temp.cols = cols;
            //        finded.Add(temp);
            //    }

            //}
        }

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

        public string text_n(int n, int p, string text)
        {
            string res = String.Empty;

            for (int i = p; i < text.Length; i += n + p)
            {
                res += text[i];
            }

            return res;
        }

        public void search(int n, string text, ref find_word[] array)
        {
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

                array_words = array_words.OrderByDescending(x => x.cols).Where(x => x.cols >= array_words.Max(x => x.cols) - 1).ToArray();

                array[j].words = array_words;

                //array_words = array_words.Where(x => x.cols >= array_words.Max(x => x.cols) - 1).ToArray();

                //ThenBy(x => x.cols >= array_words.Max(x => x.cols) - 1)

            }
        }

        public void decrypt(int n, string text)
        {
            find_word[] array = null;
            search(n, text, ref array);
        }
    }
}



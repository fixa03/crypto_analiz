using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib
{
    class Class1
    {
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



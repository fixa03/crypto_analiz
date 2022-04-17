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

            char[] symb = text.Distinct().ToArray();
            foreach (char c in symb)
                if (alphabet.IndexOf(c) == -1)
                    this.text = this.text.Replace(c.ToString(), "");

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

                if (count >= 2 && !finded.Exists(x => x.name == part) && part.Length >= 3)
                {
                    grams temp; temp.name = part; temp.distance = text.LastIndexOf(part) - text.IndexOf(part);
                    if(!finded.Exists(x => x.distance == temp.distance))
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
                array.Add(del);
            }

        }

        public bool pr_number(int number)
        {
            int count = 1;
            for (int i = 2; i <= number; i++)
            {
                if (number % i == 0) count++;
                if (count > 2) return false; 
            }

            return true;
        }

        class OptimalLen 
        {
            private class divider
            {
                public int number;
                public int cols;

                public divider(int number, int cols)
                {
                    this.number = number;
                    this.cols = cols;
                }

                public void ChangeCols(int cols)
                { this.cols = cols; }
            };
            private List<divider> array;
            private const int STOP_NUMBER = 15;

            public OptimalLen() 
            {
                array = new List<divider>();
            }

            public void Add(List<int> array_input)
            {
                int[] numbers = array_input.Distinct().ToArray();
                int[] cols = new int[numbers.Count()]; 

                for (int i = 0; i < numbers.Length; i++)
                {
                    cols[i] = array_input.Count(x => x == numbers[i]);
                    int index = array.FindIndex(x => x.number == numbers[i]);
                    if (index != -1)
                    {
                        array[index].ChangeCols( Convert.ToInt16(Math.Ceiling((double)((array[index].cols + cols[i]) / 2.0))) );

                    }
                    else
                    {
                        if(numbers[i] <= STOP_NUMBER)
                        array.Add(new divider(numbers[i], cols[i]));
                    }
                }

                
                DeleteDivider(numbers);
                    
            }

            private void DeleteDivider(int[] numbers) 
            {

                for (int i = 0; i < array.Count; i++)
                    if (numbers.Count(x => x == array[i].number) == 0)
                        if (array[i].cols == 1)
                            array.RemoveAt(i);
                        else
                            array[i].ChangeCols(array[i].cols - 1);
            }

            public override string ToString()
            {
                string str = String.Empty;

                for (int i = 0; i < array.Count; i++)
                    str += array[i].number + ": " + array[i].cols + " ";

                return str;
            }

        }

        private void search_len()
        {

            List<grams> finded = new List<grams>();
            find_grams(text, ref finded);

            finded = finded.OrderBy(x => x.distance).ToList();
            OptimalLen obj = new OptimalLen();

            List<int> current;
            const int MAX_VIEW_COUNT = 100;

            for (int i = 1; i < finded.Count; i++)
            {

                if (pr_number(finded[i].distance)) continue;

                current = new List<int>();
                nod(finded[i].distance, ref current);
                
                if (i < MAX_VIEW_COUNT)
                {
                    Console.Write(finded[i].distance + ": ");
                    for (int j = 0; j < current.Count; j++)
                        Console.Write(current[j] + " ");
                    Console.WriteLine();
                }

                obj.Add(current);
            }
            Console.WriteLine("Результат алгоритма: " + obj.ToString());

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
            search_len();
            Console.Write("Введите предпологаемую длину: "); int n = Convert.ToInt16( Console.ReadLine() );

            

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



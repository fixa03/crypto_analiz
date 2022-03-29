using System;
using System.Collections.Generic;
using System.Text;

namespace table_viginer
{
    class table_v
    {
        private string alphabet;

        public table_v(string alphabet)
        {
            this.alphabet = alphabet;
        }

        public char encrypt_word(char c, char k)
        {
            int index = (alphabet.IndexOf(c) + alphabet.IndexOf(k)) % alphabet.Length;
            return alphabet[index];
        }

        public char decrypt_word(char c, char k)
        {
            int index = (alphabet.IndexOf(c) - alphabet.IndexOf(k)) % alphabet.Length;
            if (index < 0)
                index = alphabet.Length + index;
            char w = alphabet[index];
            return alphabet[index];
        }

    }
}

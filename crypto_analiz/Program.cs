using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace crypto_analiz
{
    class Program
    {
        struct grams 
        {
            public string name;
            public int cols;
        };

        static void find_grams(string text, ref List<grams> finded)
        {
            for (int i = 0; i < text.Length - 1 ; i += 2)
            {
                int k = 1;
                string gram = text[i].ToString() + text[i+k].ToString();
                int cols = new Regex(gram).Matches(text).Count;

                while (cols >= 2)
                {
                    k++;
                    if (new Regex(gram + text[i + k].ToString()).Matches(text).Count < 2) break;
                    gram += text[i + k].ToString();
                    cols = new Regex(gram).Matches(text).Count;

                }

                if (gram.Length > 4)
                {
                    grams temp; temp.name = gram; temp.cols = cols;
                    finded.Add(temp);
                }
                
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string text = "ЧЮТНЧЦЯЫДФЕЖУШКЬДХОРНЮУНКНЬШЗИЮЮХПФЦДЯРРРЦНХЧЮЕУННХЮОХУЪСЯЕЩКЬЯЪИУАФДЬЕНЕМХМШЛИСДББЯЕШОЭТЯИЧДАБКОЬАСЕЬРНСЕШРОПХРХЪИЧПХХССЮДЫОСЖТПРДРННЦМШКИЮНЪОХЕЮФЛМСИНФБОУЛТФШЦЪВФГБАСЛХХЮЕЩЕЩЯЮШАКХСЭУХНХЫХИЦНТЖШКИЛЩДФТСМЩКСАУОЩНЖЛМОЯЛХПШОШКЬЛКАПЯАУТОРТЬЛКАЩКПЦХОРТЬУЛСТЦРПСНТНЧСОРХРЭЗГСЪОЭИЦСЮНДУЧОЧМВЮСМХУЭТОМЭКЯАФИЪЕЪЕТАШНЩЕТТХЩЭЦЧОЦТШТЦИЮТЯМОСЮЖХТХОЭТТЦОМЩКБЕТКНОПЖОЖНПЮУНУШДЪПХЯФАРТНРТНЬЛЪАЦОРПФЫОКЦЕУИЦФШЬИЛХНФХИЛХХМЦЦВЭКЫТОТНОЩЕТСЪМЪУКЛТСЬАФИХНАФЫГНСЬАФИЧНЕЕФИЮДИОУИСФВИЫДЬЧУЕКАЩСШПЬРНСЕШРИНФБОУЛТФШЦЪСЪЖАЛФНТЦРПЧОЧМВЧЦБЧДЩЕЧОКЦЭСЫВЗХЭПЦМЯЕХЦТОЩКЖТЦМЯСХЖЫКНОЦЛЗНТЖШКИЛЫФХМНЕКЦЭИЦВЗХЭПЦГЪСХЖИИЦДЩДЩЧНХБРСВГЦЭШРННПХИЦННОЭТОЦСДТЦОПЯХБУОВЭКЭЖФАЩОЯУФЕКЦЭИЦБТХЩУХЕГСЭИЦНТЕРТСЧТЗЭТСЧТЗЭТОТЦФЭСОЕРТЬУСТЪЗЭКИЖТСХЧХИГКУУХЕЮОЯУФЕЮНЗОХЫЯХЮУТОТСШДССЧДТЕКОРЧ";

            text = text.ToLower();
            List<grams> finded = new List<grams>();
            find_grams(text, ref finded);
        }
    }
}

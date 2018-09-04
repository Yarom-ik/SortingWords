using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SortingWords
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = null;
            string output = null;
            //Console.WriteLine(args[0]);
            try
            {
                input = args[0];
                output = args[1];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " Enter path ");
            }

            Dictionary<string, int> wordsAll = new Dictionary<string, int>();
            try
            {
                StreamReader file = new StreamReader(input, Encoding.Default);
                string s;
                string[] str;
                while ((s = file.ReadLine()) != null)
                {
                    //str = s.Split(new char[] { ' ', '?', '!', ':', '@' }, StringSplitOptions.RemoveEmptyEntries);
                    str = Regex.Split(s, @"\W+");
                    foreach (string s1 in str)
                    {
                        string j = s1.ToLower();

                        if (!wordsAll.ContainsKey(j))
                        {
                            wordsAll.Add(j, 1);
                        }
                        else
                            wordsAll[j]++;

                    }
                }
                file.Close();
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            wordsAll = wordsAll.OrderBy(k => k.Key).ToDictionary(process => process.Key, process => process.Value);

            Dictionary<string, IEnumerable<KeyValuePair<string, int>>> wordsGroup = new Dictionary<string, IEnumerable<KeyValuePair<string, int>>>();

            for (char i = 'a'; i <= 'z'; i++)
            {
                var filteredWordsGroup = wordsAll.Where(item => item.Key.StartsWith(i.ToString()));
                var filteredWordsOrder = filteredWordsGroup.OrderByDescending(pair => pair.Value).ToArray();
                if (filteredWordsOrder.Length > 0)
                {
                    wordsGroup.Add(i.ToString(), filteredWordsOrder);
                }
            }

            try
            {
                StreamWriter fileOut = new StreamWriter(output, false, Encoding.Default);
                foreach (var group in wordsGroup)
                {
                    fileOut.WriteLine("\r\n" + group.Key + "\r\n");
                    foreach (var word in group.Value)
                    {
                        fileOut.WriteLine(word.Key + " " + word.Value);
                    }

                }
                fileOut.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
    


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTest
{
    public class FileUtil
    {
        public static List<List<string>> ReadAllLinesWithDelimiter(string path, string delimiter)
        {
            List<List<string>> lstRtn = new List<List<string>>();

            foreach (string lineText in File.ReadAllLines(path))
            {
                List<string> tmpList = new List<string>();

                foreach (string item in lineText.Split(new string[] { delimiter }, StringSplitOptions.None))
                {
                    tmpList.Add(item);
                }

                lstRtn.Add(tmpList);
            }

            return lstRtn;
        }

        public static List<string> GetAllFiles(string path)
        {
            List<string> lst = new List<string>();

            foreach (string f in Directory.GetFiles(path))
            {
                lst.Add(f);
            }

            foreach (string d in Directory.GetDirectories(path))
            {
                foreach (string f in GetAllFiles(d))
                {
                    lst.Add(f);
                }
            }

            return lst;
        }
    }
}

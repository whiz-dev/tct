using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Util
            string str1 = Path.Combine(@"SUB1\INFILE", @"LOCATION.txt"); //SUB1\INFILE\LOCATION.txt
            string str2 = Path.GetDirectoryName(@"SUB1\INFILE\LOCATION.txt"); //SUB1\INFILE
            string str3 = Path.GetFileName(@"SUB1\INFILE\LOCATION.txt"); //LOCATION.txt
            string str4 = Path.GetFileNameWithoutExtension(@"SUB1\INFILE\LOCATION.txt"); //LOCATION
            string str5 = Path.GetExtension(@"SUB1\INFILE\LOCATION.txt"); //.txt
            string str6 = Path.GetFullPath(@"SUB1\INFILE\LOCATION.txt"); // C:\SUB1\INFILE\LOCATION.txt

            //Read
            string str = File.ReadAllText(@"SUB1\INFILE\LOCATION.txt");
            string[] lst1 = File.ReadAllLines(@"SUB1\INFILE\LOCATION.txt");
            List<List<string>> lst2 = FileUtil.ReadAllLinesWithDelimiter(@"SUB1\INFILE\LOCATION.txt", "#");

            //Directory
            Directory.CreateDirectory(@"SUB1\OUTFILE"); //경로에 있는 모든 디렉토리를 만들어준다. (존재하는 디렉토리는 Skip)
            string[] lst3 = Directory.GetFiles(@"SUB1", "*.*", SearchOption.AllDirectories);
            List<string> lst4 = new List<string>(); FileUtil.GetAllFiles(@"SUB1", lst4);

            //Copy, Move
            Directory.Move(@"SUB1\INFILE", @"SUB1\INFILE1"); //신규 디렉토리명 존재 시 오류
            File.Copy(@"SUB1\INFILE\LOCATION.txt", @"SUB1\INFILE1\LOCATION.txt", true); //신규 파일명 존재 시 덮어쓰기
            File.Move(@"SUB1\INFILE\LOCATION.txt", @"SUB1\INFILE1\LOCATION1.txt"); //신규 파일명 존재 시 오류

            //Write
            File.WriteAllText(@"SUB1\OUTFILE\CMP_PREPOST.txt", "test1" + Environment.NewLine + "test2" + Environment.NewLine); //파일 없을경우 신규생성, 있을경우 덮어쓰기
            File.WriteAllLines(@"SUB1\OUTFILE\CMP_PREPOST.txt", new string[] { "test1", "test2" });
            File.AppendAllText(@"SUB1\OUTFILE\CMP_PREPOST.txt", "test3" + Environment.NewLine + "test4" + Environment.NewLine); //파일 없을경우 신규생성, 있을경우 Append
            File.AppendAllLines(@"SUB1\OUTFILE\CMP_PREPOST.txt", new string[] { "test5", "test6" });

            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Assembly 파일 로드
            Assembly assembly = Assembly.LoadFrom("MyLibrary.dll");

            // 타입 정보 가져오기
            Type typeClass = assembly.GetType("MyLibrary.MyClass");
            Type typeDelegate = assembly.GetType("MyLibrary.MyDelegate");

            // 인스턴스 생성
            object objClass = assembly.CreateInstance("MyLibrary.MyClass");
            object objDelegate = Delegate.CreateDelegate(typeDelegate, new Handlers(), "Test");

            // 매서드 호출
            object result = typeClass.GetMethod("MyMethod").Invoke(objClass, new object[] { "Employee", objDelegate });

            // 결과 출력
            Console.WriteLine(result.ToString());

            Console.ReadLine();
        }

        static string Test(string id, string name)
        {
            return $"{id} ({name})";
        }
    }

    public class Handlers
    {
        public string Test(string id, string name)
        {
            return $"{id} ({name})";
        }
    }
}

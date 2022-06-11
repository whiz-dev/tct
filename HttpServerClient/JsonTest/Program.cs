using JsonTest.Entity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Member member1 = new Member("1", "park", "abcd@email.com", "abcd");
            Member member2 = new Member("2", "lee", "abcde@email.com", "abcde");


            // JObject
            JObject jobj1 = new JObject();
            jobj1.Add("Id", "1");
            jobj1.Add("Name", "park");
            jobj1.Add("Email", "abcd@email.com");
            jobj1.Add("Password", "abcd");

            JObject jobj2 = new JObject();
            jobj2.Add("Id", "2");
            jobj2.Add("Name", "lee");
            jobj2.Add("Email", "abcde@email.com");
            jobj2.Add("Password", "abcde");

            JObject jobj3 = JObject.FromObject(member1);

            Console.WriteLine(jobj1.ToString());
            Console.WriteLine(jobj3.ToString());
            Console.WriteLine("jobj1 == jobj3 : {0}", jobj1.ToString() == jobj3.ToString());
            Console.WriteLine();


            // JArray
            JArray jarray1 = new JArray();
            jarray1.Add(jobj1);
            jarray1.Add(jobj2);

            List<Member> members = new List<Member>();
            
            members.Add(member1);
            members.Add(member2);
            JArray jarray2 = JArray.FromObject(members);

            Console.WriteLine(jarray1.ToString());
            Console.WriteLine(jarray2.ToString());
            Console.WriteLine("jarray1 == jarray2 : {0}", jarray1.ToString() == jarray2.ToString());
            Console.WriteLine();


            // JObject with JArray
            JObject jobj4 = new JObject();
            jobj4.Add("Id", "1");
            jobj4.Add("Name", "development");
            jobj4.Add("Description", "software development");
            jobj4.Add("Members", jarray1);

            Department department = new Department("1", "development", "software development");
            department.Members.Add(member1);
            department.Members.Add(member2);
            JObject jobj5 = JObject.FromObject(department);

            Console.WriteLine(jobj4.ToString());
            Console.WriteLine(jobj5.ToString());
            Console.WriteLine("jobj4 == jobj5 : {0}", jobj4.ToString() == jobj5.ToString());
            Console.WriteLine();


            // JObject, JArray to ClassObject
            department = jobj5.ToObject(typeof(Department)) as Department;
            member1 = ((JArray)jobj5["Members"])[0].ToObject(typeof(Member)) as Member;
            member2 = ((List<Member>)jobj5["Members"].ToObject(typeof(List<Member>)))[1];


            // JsonString to JObject
            string jsonString = "{ \"Id\":\"1\", \"Name\":\"park\", \"Email\":\"abcd@email.com\", \"Password\":\"abcd\", }";
            JObject jobj6 = JObject.Parse(jsonString);
            member1 = jobj6.ToObject(typeof(Member)) as Member;


            Console.ReadLine();
        }
    }
}

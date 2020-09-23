using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace TreeView
{
    class Program
    {
        static void Main(string[] args)
        {
            Entity entity1 = new Entity("1");
            Entity entity2 = new Entity("2");
            Entity entity3 = new Entity("3");
            Entity entity4 = new Entity("4");
            Entity entity5 = new Entity("5");

            entity1.AddChild(entity2);
            entity1.AddChild(entity3);
            entity2.AddChild(entity4);
            entity4.AddChild(entity5);

            // fullpath
            string fullpath = "";

            foreach (Entity entity in entity5.GetFullPath())
            {
                fullpath += entity.Id;
                fullpath += Environment.NewLine;
            }

            Console.WriteLine(fullpath);

            // al children
            string allChildren = "";

            foreach (Entity entity in entity5.GetAllChildren())
            {
                allChildren += entity.Id;
                allChildren += Environment.NewLine;
            }

            Console.WriteLine(allChildren);

            Console.ReadLine();
        }
    }

    class Entity
    {
        string _id = null;
        public string Id
        {
            get => _id;
            set => _id = value;
        }

        Entity _parent = null;
        public Entity Parent
        {
            get => _parent;
            private set => _parent = value;
        }

        List<Entity> _children = new List<Entity>();
        public List<Entity> Children
        {
            get => _children;
        }

        public Entity(string id)
        {
            _id = id;
        }

        public void AddChild(Entity child)
        {
            child.Parent = this;
            _children.Add(child);
        }

        public List<Entity> GetFullPath()
        {
            List<Entity> parents = new List<Entity>();
            parents.Add(this);

            while (true)
            {
                Entity p = parents[0].Parent;

                if (p == null)
                {
                    break;
                }

                parents.Insert(0, p);
            }

            return parents;
        }

        public List<Entity> GetAllChildren()
        {
            List<Entity> children = new List<Entity>();

            foreach (Entity entity in this.Children)
            {
                children.Add(entity);

                foreach (Entity entity1 in entity.GetAllChildren())
                {
                    children.Add(entity1);
                }
            }

            return children;
        }

        //public void GetAllChildren(ref List<Entity> lstChildren)
        //{
        //    foreach (Entity entity in this.Children)
        //    {
        //        lstChildren.Add(entity);

        //        entity.GetAllChildren(ref lstChildren);
        //    }
        //}
    }
}

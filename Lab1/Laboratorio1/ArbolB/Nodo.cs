using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio1.ArbolB
{
   
    public class Node<T>
    {
        public List<T> Data { get; set; }
        public List<Node<T>> Children { get; set; }
        public Node<T> Father { get; set; }
        public int order;

        public Node(int order)
        {
            Data = new List<T>();
            Children = new List<Node<T>>();
            this.order = order;
        }

        public Node(int order, Node<T> nodeFather)
        {
            Data = new List<T>();
            Children = new List<Node<T>>();
            Father = nodeFather;
            this.order = order;
        }

        internal bool IsLeaf => Children.Count == 0;

        internal bool Full => Data.Count == (order - 1);

       
    }
}
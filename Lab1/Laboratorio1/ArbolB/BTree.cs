using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Laboratorio1.ArbolB
{
    public class keyComparer<T> : IComparer<T>
    {
        public delegate int compare(T aux, T aux2);
        compare compareElements;
        public void compareElementsDelegate(compare cmp)
        {
            compareElements = cmp;
        }
        //comparación en base al delegate enviado
        public int Compare(T x, T y)
        {
            return compareElements(x, y);
        }
    }
    public class BTree<T, K>
    {
        public List<T> elements = new List<T>();
        public delegate int search(K ob1, T ob2);
        search searchElements;
        public keyComparer<T> keyComparer = new keyComparer<T>();
        public Node<T> root;
        int order;

        public BTree(int order)
        {
            this.root = new Node<T>(order);
            this.order = order;
        }
        public void searchElementsDelegate(search cmp)
        {
            searchElements = cmp;
        }

        public void Add(T element)
        {
            Insert(this.root, element);
        }

        private void Insert(Node<T> root, T element)
        {

            //if (!(root.PositionInNode(element) != -1)) // el dato no está incluido en el nodo
            //{
            if (root.IsLeaf) // si es un nodo hoja 
            {
                InsertData(root, element);
            }
            else
            {
                int position = AproxChild(root, element);
                Insert(root.Children[position], element);
            }
            //}            
        }

        internal void InsertData(Node<T> root, T data)
        {
            if (!root.Full)
            {
                root.Data.Add(data);
                root.Data.Sort(keyComparer); //ordenar datos con comparación de objetos especificada                
            }
            else
            {
                SplitNode(root, data);
            }
        }

        private void SplitNode(Node<T> root, T data)
        {
            if (root.Father == null) // si el nodo a dividir es la raiz 
            {
                List<Node<T>> auxChildren = root.Children;
                Node<T> left = new Node<T>(order, root);
                Node<T> rigth = new Node<T>(order, root);
                List<T> temp = root.Data;
                temp.Add(data); // agregar el valor extra al nodo
                temp.Sort(keyComparer); // ordenar los valores
                T auxData = temp[order / 2];

                //dividir el nodo 
                for (int i = 0; i < (order / 2); i++)
                {
                    InsertData(left, temp[i]);
                    InsertData(rigth, temp[(order / 2) + i + 1]);
                }

                //dividir hijos
                for (int i = 0; i < ((order / 2) + 1); i++)
                {
                    if (auxChildren.Count != 0)
                    {
                        left.Children.Add(auxChildren[i]);
                        rigth.Children.Add(auxChildren[(order / 2) + i + 1]);
                    }
                }

                root.Data = new List<T>();
                root.Data.Add(auxData);//dejar el valor de en medio en el nuevo nodo raiz 
                root.Children.Add(left);//agregar referenia a los hijos
                root.Children.Add(rigth);
            }
            else // si el nodo que se va a dividir tiene padre
            {
                Node<T> Father = root.Father;
                List<Node<T>> auxChildren = root.Children;
                List<Node<T>> fatherChildren = Father.Children;
                Node<T> left = new Node<T>(order, Father);
                Node<T> rigth = new Node<T>(order, Father);
                List<T> temp = root.Data;
                temp.Add(data); // agregar el valor extra al nodo
                temp.Sort(keyComparer); // ordenar los valores
                T auxData = temp[order / 2];

                InsertData(Father, auxData); // insertar en el nodo padre el dato de en medio

                //dividir el nodo 
                for (int i = 0; i < (order / 2); i++)
                {
                    InsertData(left, temp[i]);
                    InsertData(rigth, temp[(order / 2) + i + 1]);
                }

                //dividir hijos
                for (int i = 0; i < ((order / 2) + 1); i++)
                {
                    if (auxChildren.Count != 0)
                    {
                        left.Children.Add(auxChildren[i]);
                        rigth.Children.Add(auxChildren[(order / 2) + i + 1]);
                    }
                }

                //asignar el nodo divido a los hijos del nodo padre     
                Father.Children = new List<Node<T>>();
                for (int i = 0; i < fatherChildren.Count; i++)
                {
                    if (fatherChildren[i] == root)
                    {
                        Father.Children.Add(left);
                        Father.Children.Add(rigth);
                    }
                    else
                    {
                        Father.Children.Add(fatherChildren[i]);
                    }
                }
            }
        }


        public int AproxChild(Node<T> root, T data)
        {
            int position = 0;
            for (int i = 0; i < root.Data.Count; i++)
            {
                if ((keyComparer.Compare(data, root.Data[i]) < 0) || root.Data[i] == null)
                {
                    position = i;
                    i = root.Data.Count;
                }
                if (i == root.Data.Count - 1)
                {
                    position = i + 1;
                    i = root.Data.Count;
                }
            }
            return position;
        }

        public int SearchAproxChild(Node<T> root, K data)
        {
            int position = 0;
            for (int i = 0; i < root.Data.Count; i++)
            {
                if ((searchElements(data, root.Data[i]) < 0) || root.Data[i] == null)
                {
                    position = i;
                    i = root.Data.Count;
                }
                if (i == root.Data.Count - 1)
                {
                    position = i + 1;
                    i = root.Data.Count;
                }
            }
            return position;
        }

        public T getElement(K element)
        {
            return ViewData(this.root, element);
        }

        public T ViewData(Node<T> root, K data)
        {
            T response = default(T);
            //recorrer el arreglo de datos del nodo 
            for (int i = 0; i < root.Data.Count; i++)
            {
                if (searchElements(data, root.Data[i]) == 0)
                {
                    response = root.Data[i];
                }
            }

            if (response == null) // si es un nodo hoja 
            {
                int position = SearchAproxChild(root, data);
                return ViewData(root.Children[position], data);
            }
            else
            {
                return response;
            }

        }

        public List<T> getList()
        {
            elements = new List<T>();
            Traverse(this.root);
            return elements;
        }

        public void Traverse(Node<T> root)
        {
            for (int i = 0; i < root.Children.Count; i++)
            {
                Traverse(root.Children[i]);
            }
            elements.AddRange(root.Data);
        }


    }
}
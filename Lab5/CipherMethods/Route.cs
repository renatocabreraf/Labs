using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace CipherMethods
{
    public class Route
    {
        int m { get; set; }
        int n { get; set; }
        string text { get; set; }
        string fileName { get; set; }

        char[,] matrix; 

        public Route(int m, int n, string text, string fileName) {
            this.m = m;
            this.n = n;            
            this.fileName = fileName; 
            matrix = new char[m, n];

            //eliminar el caracter \r\n que agrega el stringbuilder al final de todo el texto
            text = text.Remove(text.Length - 1);
            text = text.Remove(text.Length - 1);
            this.text = text;
        }

        public void vertical() {
            //fill matrix
            int cont = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (cont != text.Length)
                        matrix[j, i] = text[cont];
                    else
                        matrix[j, i] = '#';

                    cont++;
                }
            }

            string outPut = "";
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    outPut += matrix[i, j];
                }
            }

            //escribir archivo 
            string folder = @"C:\Lab4\";
            string fullPath = folder + fileName;
            // crear el directorio
            DirectoryInfo directory = Directory.CreateDirectory(folder);

            using (StreamWriter file = new StreamWriter(fullPath))
            {
                file.WriteLine(outPut);
                file.Close();
            }
        }

        public void decipherVertical() {
            int cont = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = text[cont];
                    cont++;
                }
            }

            string outPut = "";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    outPut += matrix[j, i];
                }
            }

            outPut = outPut.Replace('#', ' ');

            //escribir archivo 
            string folder = @"C:\Lab4\";
            string fullPath = folder + fileName;
            // crear el directorio
            DirectoryInfo directory = Directory.CreateDirectory(folder);

            using (StreamWriter file = new StreamWriter(fullPath))
            {
                file.WriteLine(outPut);
                file.Close();
            }
        }

        public void spiral() 
        {
            int cont = 0;

            bool right = true, down = false, left = false, up = false;
            int x = 0, y = 0;
            for (int i = 0; i < m*n; i++)
            {
                if (right)
                {
                    if (y < n && matrix[x, y] == '\0')
                    {
                        matrix[x, y] = (cont < text.Length) ? text[cont] : '#';
                        cont++;
                        y++;
                    }
                    else
                    {
                        y--;
                        x++;
                        right = false;
                        down = true;
                        i--;
                    }
                }
                else if (down)
                {
                    if (x < m && matrix[x, y] == '\0')
                    {
                        matrix[x, y] = (cont < text.Length) ? text[cont] : '#';
                        cont++;
                        x++;
                    }
                    else
                    {
                        x--;
                        y--;
                        down = false;
                        left = true;
                        i--;
                    }
                }
                else if (left)
                {
                    if (y > -1 && matrix[x, y] == '\0')
                    {
                        matrix[x, y] = (cont < text.Length) ? text[cont] : '#';
                        cont++;
                        y--;
                    }
                    else
                    {
                        y++;
                        x--;
                        left = false;
                        up = true;
                        i--;
                    }
                }
                else if (up)
                {
                    if (x > -1 && matrix[x, y] == '\0')
                    {
                        matrix[x, y] = (cont < text.Length) ? text[cont] : '#';
                        cont++;
                        x--;
                    }
                    else
                    {
                        y++;
                        x++;
                        up = false;
                        right = true;
                        i--;
                    }
                }
            }

            string outPut = "";
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < m; i++)
                {
                    outPut += matrix[i, j];
                }
            }

            //escribir archivo 
            string folder = @"C:\Lab4\";
            string fullPath = folder + fileName;
            // crear el directorio
            DirectoryInfo directory = Directory.CreateDirectory(folder);

            using (StreamWriter file = new StreamWriter(fullPath))
            {
                file.WriteLine(outPut);
                file.Close();
            }
        }

        public void decipherSpiral()
        {
            int cont = 0;
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < m; i++)
                {
                    matrix[i, j] = text[cont];
                    cont++;
                }
            }

            string output = "";
            bool right = true, down = false, left = false, up = false;
            int x = 0, y = 0;
            for (int i = 0; i < m * n; i++)
            {
                if (right)
                {
                    if (y < n && matrix[x, y] != '\0')
                    {
                        output += matrix[x, y].ToString();
                        matrix[x, y] = '\0';
                        y++;
                    }
                    else
                    {
                        y--;
                        x++;
                        right = false;
                        down = true;
                        i--;
                    }
                }
                else if (down)
                {
                    if (x < m && matrix[x, y] != '\0')
                    {
                        output += matrix[x, y].ToString();
                        matrix[x, y] = '\0';
                        x++;
                    }
                    else
                    {
                        x--;
                        y--;
                        down = false;
                        left = true;
                        i--;
                    }
                }
                else if (left)
                {
                    if (y > -1 && matrix[x, y] != '\0')
                    {
                        output += matrix[x, y].ToString();
                        matrix[x, y] = '\0';
                        y--;
                    }
                    else
                    {
                        y++;
                        x--;
                        left = false;
                        up = true;
                        i--;
                    }
                }
                else if (up)
                {
                    if (x > -1 && matrix[x, y] != '\0')
                    {
                        output += matrix[x, y].ToString();
                        matrix[x, y] = '\0';
                        x--;
                    }
                    else
                    {
                        y++;
                        x++;
                        up = false;
                        right = true;
                        i--;
                    }
                }
            }

            //escribir archivo 
            string folder = @"C:\Lab4\";
            string fullPath = folder + fileName;
            // crear el directorio
            DirectoryInfo directory = Directory.CreateDirectory(folder);

            using (StreamWriter file = new StreamWriter(fullPath))
            {
                file.WriteLine(output);
                file.Close();
            }
        }
    }
}

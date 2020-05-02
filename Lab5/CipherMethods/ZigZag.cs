using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace CipherMethods
{
    public class ZigZag
    {
        string text;
        string fileName { get; set; }
        int rails { get; set; }
        public void calculate(int rails, StringBuilder input, string fileName) {
            this.fileName = fileName;
            this.rails = rails;
            text = input.ToString();
            //eliminar el caracter \r\n que agrega el stringbuilder al final de todo el texto
            text = text.Remove(text.Length - 1);
            text = text.Remove(text.Length - 1);
            float charQty = 1 + 1 + (2 * (rails - 2)); //cantidad de caracteres por ola 
            float unit = charQty / (charQty * charQty); //decimal correspondiente a un caracter
            float auxLength = text.Length;
            float waves = auxLength / charQty;
            double rest = waves  - Math.Truncate(waves); //parte decimal de las olas 
            
            rest = 1 - rest; //completar 
            rest = rest / unit; //cantidad de caracteres especiales que hay que completar

            for (int i = 0; i < rest; i++)
            {
                text += "#"; //para completar la ola
            }

            fillMatrix(rails, text);

        }
        public void fillMatrix(int rails, string text) {
            string[,] matrix = new string[rails, text.Length];
            int cont = 0;
            while (cont != text.Length) //recorrer toda la cadena
            {                
                for (int j = 0; j < rails; j++)
                {
                    matrix[j, cont] = text[cont].ToString();
                    cont++;
                }

                for (int i = rails - 2; i > 0; i--)
                {
                    matrix[i, cont] = text[cont].ToString();
                    cont++;
                }                                            
            }

            cipher(matrix);
        }

        public void cipher(string[,] matrix) {
            string encryptedText = "";
            
            for (int i = 0; i < rails; i++)
            {
                for (int j = 0; j < text.Length; j++)
                {
                    if (matrix[i, j] != "")
                    {
                        encryptedText += matrix[i, j];
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
                file.WriteLine(encryptedText);
                file.Close();
            }

        }

        public void decipher(int rails, StringBuilder input, string fileName)
        {
            this.text = input.ToString();
            this.rails = rails;

            //eliminar el caracter \r\n que agrega el stringbuilder al final de todo el texto
            text = text.Remove(text.Length - 1);
            text = text.Remove(text.Length - 1);

            int charQty = 2 + (2 * (rails - 2)); //cantidad de caracteres por ola 

            //cantidad de picos
            int picos = text.Length / charQty;

            string[,] matrix = new string[rails, (text.Length - (2 * picos)) / (rails - 2)];

            fillMatrix(text, picos, ref matrix);

            string result = "";

            int cont = 0;
            int colExt = 0;
            int colInt = 0;
            while (cont != text.Length)
            {
                result += matrix[0, colExt];
                cont++;

                for (int i = 1; i < (rails-1); i++)
                {
                    result += matrix[i, colInt];
                    cont++;
                }
                colInt++;

                result += matrix[rails-1, colExt];
                cont++;
                colExt++;

                for (int i = rails - 2; i > 0; i--)
                {
                    result += matrix[i, colInt];
                    cont++;
                }
                colInt++;
            }

            bool deleteAllExtra = false;
            while (!deleteAllExtra)
            {
                if (result[result.Length -1] == '#')
                {
                    result = result.Remove(result.Length - 1);
                }
                else
                {
                    deleteAllExtra = true;
                }
            }

            //escribir archivo 
            string folder = @"C:\Lab4\";
            string fullPath = folder + fileName;
            // crear el directorio
            DirectoryInfo directory = Directory.CreateDirectory(folder);

            using (StreamWriter file = new StreamWriter(fullPath))
            {
                file.WriteLine(result);
                file.Close();
            }
        }

        private void fillMatrix(string text, int picos, ref string[,] matrix)
        {            
            for (int i = 0; i < picos; i++)
            {
                matrix[0, i] = text[i].ToString();          // primera fila

                int pos = (text.Length - picos) + i;        // ultima fila
                matrix[rails - 1, i] = text[pos].ToString();
            }

            int split = (text.Length - (picos * 2)) / (rails - 2);
            int cont = picos;
            for (int i = 1; i < rails - 1; i++)
            {
                for (int j = 0; j < split; j++)
                {
                    matrix[i, j] = text[cont].ToString();
                    cont++;
                }
            }
        }
    }
}

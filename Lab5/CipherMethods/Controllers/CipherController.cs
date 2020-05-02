using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IO;

namespace CipherMethods.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CipherController : ControllerBase
    {
        // GET: api/Cipher
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Cipher/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        /// <summary>
        /// Request POST para cifrar texto.
        /// </summary>
        /// <param name="file">Archivo de texto original</param>
        /// <param name="name">Nombre del cifrado a utilizar</param>
        /// <param name="fileName">Nuevo nombre para el archivo de salida</param>
        /// <param name="param">Parámetro para el método de cifrado</param>
        /// <returns></returns>
        [HttpPost("{name}/{fileName}/{param}")]
        public string Post([FromForm(Name = "file")] IFormFile file, string name, string fileName, string param)
        {
            //lectura del archivo
            var result = new StringBuilder();
            string path = @"c:\Lab4";
            if (Directory.Exists(path))
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        result.AppendLine(reader.ReadLine());
                }

                if (name.ToLower().Equals("zigzag"))
                {
                    ZigZag zigzagCipher = new ZigZag();
                    zigzagCipher.calculate(int.Parse(param), result, fileName);
                    return "Texto encriptado, método: ZigZag";
                }
                else if (name.ToLower().Equals("caesar"))
                {
                    Caesar caesarCipher = new Caesar(param);
                    caesarCipher.buildAlphabet();
                    caesarCipher.cipher(result, fileName);
                    return "Texto encriptado, método: Caesar";
                }
                else if (name.ToLower().Equals("ruta"))
                {
                    string[] parameters = param.Split(','); // la forma de ingresar parametro "mxn,tipo de ruta"
                    string[] dimensiones = parameters[0].Split('x');
                    int m = int.Parse(dimensiones[0]);
                    int n = int.Parse(dimensiones[1]);
                    Route routeCipher = new Route(m, n, result.ToString(), fileName);

                    if (parameters[1].ToLower().Equals("vertical"))
                    {
                        routeCipher.vertical();
                    }
                    else
                    {
                        routeCipher.spiral();
                    }

                    return "Texto encriptado, método: Ruta";
                }
                else
                {
                    return "MÉTODO INCORRECTO";
                }
            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        result.AppendLine(reader.ReadLine());
                }

                if (name.ToLower().Equals("zigzag"))
                {
                    ZigZag zigzagCipher = new ZigZag();
                    zigzagCipher.calculate(int.Parse(param), result, fileName);
                    return "Texto encriptado, método: ZigZag";
                }
                else if (name.ToLower().Equals("caesar"))
                {
                    Caesar caesarCipher = new Caesar(param);
                    caesarCipher.buildAlphabet();
                    caesarCipher.cipher(result, fileName);
                    return "Texto encriptado, método: Caesar";
                }
                else if (name.ToLower().Equals("ruta"))
                {
                    string[] parameters = param.Split(','); // la forma de ingresar parametro "mxn,tipo de ruta"
                    string[] dimensiones = parameters[0].Split('x');
                    int m = int.Parse(dimensiones[0]);
                    int n = int.Parse(dimensiones[1]);
                    Route routeCipher = new Route(m, n, result.ToString(), fileName);

                    if (parameters[1].ToLower().Equals("vertical"))
                    {
                        routeCipher.vertical();
                    }
                    else
                    {
                        routeCipher.spiral();
                    }

                    return "Texto encriptado, método: Ruta";
                }
                else
                {
                    return "MÉTODO INCORRECTO";
                }
            
        }
        }

        // PUT: api/Cipher/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

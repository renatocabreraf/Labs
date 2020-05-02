using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laboratorio0_RenatoCabrera_1010617.Models;

namespace Laboratorio0_RenatoCabrera_1010617.Controllers
{
    public class RutaObjeto
    {
        private static RutaObjeto i = null; //i es el nombre que escogí para la Instancia

        public static RutaObjeto Instancia
        {
            get
            {
                if (i == null) i = new RutaObjeto();
                return i;
            }
        }

        private static WeatherForecast[] peliculas = {
            new WeatherForecast { ID = 1, Nombre = "Once Upon A Time... In Hollywood", Año = 2019, Director = "Quentin Tarantino"},
            new WeatherForecast { ID = 2, Nombre = "Avengers Engame", Año = 2019, Director = "Russo Brothers"},
            new WeatherForecast { ID = 3, Nombre = "Star Wars Episode IX The Rise Of Skywalker", Año = 2019, Director = "J.J. Abrahams"},
            new WeatherForecast { ID = 4, Nombre = "IT Chapter One", Año = 2017, Director = "Andy Musquieti"},
            new WeatherForecast { ID = 5, Nombre = "Taxi Driver", Año = 1976, Director = "Martin Scorsese"},
            new WeatherForecast { ID = 6, Nombre = "Apocalypse Now", Año = 1979, Director = "Francis Ford Coppola"},
            new WeatherForecast { ID = 7, Nombre = "The Dark Knight", Año = 2008, Director = "Christopher Nolan"},
            new WeatherForecast { ID = 8, Nombre = "Psicosis", Año = 1960, Director = "Alfred Hitchcock"},
            new WeatherForecast { ID = 9, Nombre = "Indiana Jones", Año = 1981, Director = "Steven Spielberg"},
            new WeatherForecast { ID = 10, Nombre = "Scent of a Woman", Año = 1992, Director = "Martin Brest"}

        };

        public Stack<WeatherForecast> Peliculas = new Stack<WeatherForecast>(peliculas);
    }
}
   
       

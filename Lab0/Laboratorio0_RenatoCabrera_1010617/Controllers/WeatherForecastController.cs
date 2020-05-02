using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Laboratorio0_RenatoCabrera_1010617.Models;


namespace Laboratorio0_RenatoCabrera_1010617.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class WeatherForecastController : ControllerBase
    {

        [HttpGet]
        public List<WeatherForecast> Get()
        {
            List<WeatherForecast> aux = new List<WeatherForecast>();
            int CuentaPeliculas = RutaObjeto.Instancia.Peliculas.Count;

            if(CuentaPeliculas > 0)
            { 
                if(CuentaPeliculas < 10)
                {
                    for (int i = 0; i < CuentaPeliculas; i++)
                    {
                        aux.Add(RutaObjeto.Instancia.Peliculas.Pop());
                    }
                    for(int i = CuentaPeliculas - 1; i >= 0; i--)
                    {
                        RutaObjeto.Instancia.Peliculas.Push(aux[i]);
                    }
                }
                else
                {
                    for(int i = 0; i < 10; i++)
                    {
                        aux.Add(RutaObjeto.Instancia.Peliculas.Pop());
                    }
                }
            }
            return aux;

        }
        [HttpGet("{id}", Name = "Get")]
        public WeatherForecast Get(int id)
        {
            if (id <= RutaObjeto.Instancia.Peliculas.Count())
                return RutaObjeto.Instancia.Peliculas.ElementAt(id);

            return null;
        }

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public WeatherForecast Post([FromBody]WeatherForecast peliculas)
        {
            if (peliculas.ID == 0)
                peliculas.ID = RutaObjeto.Instancia.Peliculas.Count() + 1;

            RutaObjeto.Instancia.Peliculas.Push(peliculas);
            return RutaObjeto.Instancia.Peliculas.Peek();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploFlujoAsync
{
    public static class CalculadoraHipotecaAsync
    {
        public static async Task<int> ObtenerAniosVidaLaboral()
        {
            Console.WriteLine("\n Obteniendo años de la vida laboral...");
            await Task.Delay(5000);
            return new Random().Next(1, 35);
        }
        public static async Task<bool> EsTipoContratoIndefinido()
        {
            Console.WriteLine("\n Verificando si el tipo de contrato es indefinido...");
            await Task.Delay(5000);
            return (new Random().Next(1, 10)) % 2 == 0;
        }
        public static async Task<int> ObtenerSueldoNeto()
        {
            Console.WriteLine("\n Obteniendo sueldo neto...");
            await Task.Delay(5000);
            return new Random().Next(800, 6000);
        }
        public static async Task<int> ObtenerGastosMensuales()
        {
            Console.WriteLine("\n Obteniendo gastos mensuales del usuario...");
            await Task.Delay(5000);
            return new Random().Next(200, 1000);
        }

        public static bool AnalizarInformacionParaConcederHipoteca(
           int aniosVidaLaboral,
           bool tipoContratoEsIndefinido,
           int sueldoNeto,
           int gastosMensuales,
           int cantidadSolicitada,
           int aniosPagar)
        {
            Console.WriteLine("\n Analizando informacion para conceder hipoteca...");

            if (aniosVidaLaboral < 2)
            {
                return false;
            }
            //obtener la cuota mensual a pagar
            var cuota = (cantidadSolicitada / aniosPagar) / 12;

            if (cuota >= sueldoNeto || cuota > (sueldoNeto / 2))
            {
                return false;
            }
            // obtener porcentaje de gastos sobre el sueldo neto del usuario
            var porcentajeGastosSobreSueldo = (gastosMensuales * 100) / sueldoNeto;

            if (porcentajeGastosSobreSueldo > 30)
            {
                return false;
            }
            if ((cuota + gastosMensuales) >= sueldoNeto)
            {
                return false;
            }
            if (!tipoContratoEsIndefinido)
            {
                if (((cuota + gastosMensuales) > (sueldoNeto / 3)))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
    }
}

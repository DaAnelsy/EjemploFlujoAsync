using EjemploFlujoAsync;
using System;
using System.Diagnostics;

//Iniciamos un contenedor de tiempo
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

Console.WriteLine("\n ************************************************************");
Console.WriteLine("\n Bienvenido a la calculadora de Hipotecas Sincrona");
Console.WriteLine("\n ************************************************************");

var aniosVidaLaboral = CalculadoraHipotecaSync.ObtenerAniosVidaLaboral();
Console.WriteLine($"\n Años de vida laboral obtenidos: {aniosVidaLaboral}");

var esTipoContratoIndefinido = CalculadoraHipotecaSync.EsTipoContratoIndefinido();
Console.WriteLine($"\n Tipo de contrato indefinido: {esTipoContratoIndefinido}");

var sueldoNeto = CalculadoraHipotecaSync.ObtenerSueldoNeto();
Console.WriteLine($"\n Sueldo neto obtenido: {sueldoNeto }Q");

var gastosMensuales = CalculadoraHipotecaSync.ObtenerGastosMensuales();
Console.WriteLine($"\nGastos mensuales obtenidos: {gastosMensuales}Q");

var hipotecaConcedida = CalculadoraHipotecaSync.AnalizarInformacionParaConcederHipoteca(aniosVidaLaboral, esTipoContratoIndefinido, sueldoNeto, gastosMensuales, cantidadSolicitada: 50000, aniosPagar: 30);

var resultado = hipotecaConcedida ? "APROBADA" : "DENEGADA";

Console.WriteLine($"\n Analisis Finalizado. su solicitud de hipoteca ha sido: {resultado}");

stopwatch.Stop();
Console.WriteLine($"\n La operacion ha durado: {stopwatch.Elapsed}");

//ReIniciar un contador de tiempo - ASINCRONO
stopwatch.Restart();
Console.WriteLine("\n ************************************************************");
Console.WriteLine("\n Bienvenido a la calculadora de Hipotecas Asincrona");
Console.WriteLine("\n ************************************************************");

Task<int> aniosVidaLaboralTask = CalculadoraHipotecaAsync.ObtenerAniosVidaLaboral();
Task<bool> esTipoContratoIndefinidoTask = CalculadoraHipotecaAsync.EsTipoContratoIndefinido();
Task<int> sueldoNetoTask = CalculadoraHipotecaAsync.ObtenerSueldoNeto();
Task<int> gastosMensualesTask = CalculadoraHipotecaAsync.ObtenerGastosMensuales();

var analisisHipotecaTasks = new List<Task>
{
    aniosVidaLaboralTask,
    esTipoContratoIndefinidoTask,
    sueldoNetoTask,
    gastosMensualesTask
};

while (analisisHipotecaTasks.Any())
{
    Task tareaFinalizada = await Task.WhenAny(analisisHipotecaTasks);
    if (tareaFinalizada == aniosVidaLaboralTask)
    {
        Console.WriteLine($"\n Años de vida laboral obtenidos: {aniosVidaLaboralTask.Result}");
    }
    else if (tareaFinalizada == esTipoContratoIndefinidoTask)
    {
        Console.WriteLine($"\n Tipo de contrato indefinido: {esTipoContratoIndefinidoTask.Result}");
    }
    else if (tareaFinalizada == sueldoNetoTask)
    {
        Console.WriteLine($"\n Sueldo neto obtenido: {sueldoNetoTask.Result}Q");
    }
    else if (tareaFinalizada == gastosMensualesTask)
    {
        Console.WriteLine($"\nGastos mensuales obtenidos: {gastosMensualesTask.Result}Q");
    }
    analisisHipotecaTasks.Remove( tareaFinalizada );
}


var hipotecaAsyncConcedida = CalculadoraHipotecaAsync.AnalizarInformacionParaConcederHipoteca
    (aniosVidaLaboralTask.Result, esTipoContratoIndefinidoTask.Result, sueldoNetoTask.Result, gastosMensualesTask.Result, cantidadSolicitada: 50000, aniosPagar: 30);

var resultadoAsync = hipotecaAsyncConcedida ? "APROBADA" : "DENEGADA";

Console.WriteLine($"\n Analisis Finalizado. su solicitud de hipoteca ha sido: {resultadoAsync}");

stopwatch.Stop();
Console.WriteLine($"\n La operacion Asincrona ha durado: {stopwatch.Elapsed}");
Console.Read();
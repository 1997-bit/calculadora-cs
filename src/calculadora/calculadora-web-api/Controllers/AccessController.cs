using calculadora_web_api.Models.WS.Historial;
using calculadora_web_api.Models.WS.Reply;
using calculadora_web_api.Services.SQLServer;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace calculadora_web_api.Controllers
{
    public class AccessController : ApiController
    {
        private SQLServer sql_server = new SQLServer();

        // GET: api/Access/GetAll
        [HttpGet]
        public Reply<List<Historial>> GetAll()
        {
            var oR = new Reply<List<Historial>>();
            List<Historial> list_historial = new List<Historial>();

            try
            {
                sql_server.SelectCalcHistorial();

                while (sql_server.Read())
                {
                    var historial = new Historial
                    {
                        Id = Convert.ToInt64(sql_server.List()["id"]),
                        Num1 = Convert.ToDouble(sql_server.List()["Num1"]),
                        Op = Convert.ToChar(sql_server.List()["Op"]),
                        Num2 = Convert.ToDouble(sql_server.List()["Num2"]),
                        Result = Convert.ToDouble(sql_server.List()["Result"])
                    };
                    list_historial.Add(historial);
                }

                oR.Result = 1;
                oR.Message = "[OK]: Todos los cálculos cargados";
                oR.Data = list_historial;
            }
            catch (Exception)
            {
                oR.Result = 0;
                oR.Message = "[Err]: Error al cargar historial";
                oR.Data = null;
            }
            finally
            {
                sql_server.Close();
            }

            return oR;
        }

        // GET: api/Access/GetByOperation/add
        [HttpGet]
        public Reply<List<Historial>> GetByOperation(string id)
        {
            var oR = new Reply<List<Historial>>();
            List<Historial> list_historial = new List<Historial>();
            string operacion = id;

            try
            {
                switch (SQLServer.HistorialTypeConverter(operacion))
                {
                    case HistorialType.ADD:
                        sql_server.SelectCalcHistorialSearch(HistorialType.ADD);
                        break;
                    case HistorialType.SUB:
                        sql_server.SelectCalcHistorialSearch(HistorialType.SUB);
                        break;
                    case HistorialType.MUL:
                        sql_server.SelectCalcHistorialSearch(HistorialType.MUL);
                        break;
                    case HistorialType.DIV:
                        sql_server.SelectCalcHistorialSearch(HistorialType.DIV);
                        break;
                    case HistorialType.MOD:
                        sql_server.SelectCalcHistorialSearch(HistorialType.MOD);
                        break;
                    case HistorialType.POW2:
                        sql_server.SelectCalcHistorialSearch(HistorialType.POW2);
                        break;
                    case HistorialType.Raiz:
                        sql_server.SelectCalcHistorialSearch(HistorialType.Raiz);
                        break;
                    default:
                        oR.Result = 0;
                        oR.Message = $"[Err]: Operación no válida: {operacion}";
                        return oR;
                }

                while (sql_server.Read())
                {
                    var historial = new Historial
                    {
                        Id = Convert.ToInt64(sql_server.List()["id"]),
                        Num1 = Convert.ToDouble(sql_server.List()["Num1"]),
                        Op = Convert.ToChar(sql_server.List()["Op"]),
                        Num2 = Convert.ToDouble(sql_server.List()["Num2"]),
                        Result = Convert.ToDouble(sql_server.List()["Result"])
                    };
                    list_historial.Add(historial);
                }

                oR.Result = 1;
                oR.Message = $"[OK]: {operacion.ToUpper()} cálculos cargados";
                oR.Data = list_historial;
            }
            catch (Exception)
            {
                oR.Result = 0;
                oR.Message = $"[Err]: Error al cargar {operacion}";
                oR.Data = null;
            }
            finally
            {
                sql_server.Close();
            }

            return oR;
        }

        // POST: api/Access/Calculate
        [HttpPost]
        public Reply<string> Calculate([FromBody] CalculoRequest calculo)
        {
            var oR = new Reply<string>();

            try
            {
                if (calculo == null)
                {
                    oR.Result = 0;
                    oR.Message = "[Err]: Datos de cálculo inválidos";
                    return oR;
                }

                char operacionChar;
                switch (calculo.Operacion.ToLower())
                {
                    case "add": operacionChar = '+'; break;
                    case "sub": operacionChar = '-'; break;
                    case "mul": operacionChar = '*'; break;
                    case "div": operacionChar = '/'; break;
                    case "mod": operacionChar = '%'; break;
                    case "pow2": operacionChar = '²'; break;
                    case "sqrt": operacionChar = '√'; break;
                    default:
                        oR.Result = 0;
                        oR.Message = $"[Err]: Operación no válida: {calculo.Operacion}";
                        return oR;
                }

                double resultado = CalcularResultado(calculo.Num1, calculo.Num2, operacionChar);

                sql_server.InsertarCalculo(calculo.Num1, operacionChar, calculo.Num2, resultado);

                oR.Result = 1;
                oR.Message = "[OK]: Cálculo guardado exitosamente";
                oR.Data = resultado.ToString();
            }
            catch (Exception ex)
            {
                oR.Result = 0;
                oR.Message = $"[Err]: Error en cálculo - {ex.Message}";
                oR.Data = null;
            }

            return oR;
        }

        private double CalcularResultado(double num1, double num2, char operacion)
        {
            switch (operacion)
            {
                case '+': return num1 + num2;
                case '-': return num1 - num2;
                case '*': return num1 * num2;
                case '/':
                    if (num2 == 0) throw new Exception("División por cero");
                    return num1 / num2;
                case '%': return num1 % num2;
                case '²': return Math.Pow(num1, num2);
                case '√': return Math.Sqrt(num1);
                default: throw new Exception("Operación no soportada");
            }
        }
    }

    public class CalculoRequest
    {
        public double Num1 { get; set; }
        public string Operacion { get; set; }
        public double Num2 { get; set; }
    }
}
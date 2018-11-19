using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.OracleClient;
using System.Data;

namespace WS_Prueba
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://WebService_tse_RFID/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        OracleConnection connection = new OracleConnection("Data Source=172.16.1.16:1521/RFID; User id=embalaje; password=embalaje01");
        //Inicio Comprobar que ruta de sede exista
        [WebMethod]
        public int comprobar_sede(int id_ruta)
        {

            String sqlConsulta_Seleccionar_caja_articulos = "select distinct ruta_sede from sede_logistica "+ 
                                                            " where RUTA_SEDE=:ID_RUTA";
            OracleCommand comando = new OracleCommand(sqlConsulta_Seleccionar_caja_articulos, connection);
            comando.Parameters.Add(":ID_RUTA", OracleType.VarChar, 32).Value = id_ruta;
            OracleDataReader lector2;
            connection.Open();
            lector2 = comando.ExecuteReader();
            if (lector2.HasRows)
            {
                connection.Close();
                return 1;
            }
            else
            {
                // MessageBox.Show("Esta sede logistica no se encuentra en la base de datos");

                connection.Close();
                return 0;
            }
        }// Fin Comprobar Sede

        //Inicio Metodo Cajas verificadas RUTA_Sede
        [WebMethod]
        public int CajasListasDespacho(int id_ruta)
        {
            // Descripcion: Comprobara si la Ruta_Sede seleccionada aun le faltan cajas por detectar

            string paquete = "select id_caja from caja c join sede_logistica s "+
                                " on C.ID_SEDE=S.ID_SEDE "+
                                 " where C.ID_ESTADO=1 and S.RUTA_SEDE=:RUTA_SEDE";
            OracleCommand command = new OracleCommand(paquete, connection);
            command.Parameters.Add(":RUTA_SEDE", OracleType.Char, 50).Value = id_ruta;
            OracleDataReader lector;
            connection.Open();
            lector = command.ExecuteReader();
            if (lector.HasRows)
            {
                connection.Close();
                return 1;
            }
            else
            {
                connection.Close();
                return 0;
            }

        }// Fin metodo CajasSede

              

        //Inicio Metodo Comprobar RFID
        [WebMethod]
        public int ComprobarRFID(string rfid)
        {
            //Descripcion:
            //Obtiene el codigo RFID como parametro, hace la consulta que devuela el id de la caja
            // Y lo guarda en una variable "id_caja" y eso retorna para agarrarlo en la aplicacion
            //si no se encontro el codigo RFID que pertenece a una caja, devuelve CERO 
            int id_caja;

            String sqlConsulta_Seleccionar_caja_articulos = "select c.id_caja caja, c.codebar " +
                                                                        " from detalle_caja det" +
                                                                        " join caja c on det.id_caja=c.id_caja" +
                                                                        " where det.RFID=:CODIGO_RFID_ARTICULO and c.id_estado = 1 and det.id_articulo=1";

            OracleCommand comando = new OracleCommand(sqlConsulta_Seleccionar_caja_articulos, connection);
            comando.Parameters.Add(":CODIGO_RFID_ARTICULO", OracleType.VarChar, 32).Value = rfid;
            OracleDataReader lector2;
            connection.Open();
            lector2 = comando.ExecuteReader();
            if (lector2.HasRows)
            {
                lector2.Read();
                id_caja = lector2.GetInt32(0);
                connection.Close();
                return id_caja;
                
            }
            else
            {
                // MessageBox.Show("Este codigo de rfid no se encuentra en la base de datos");
                connection.Close();
                return 0;
            }
        }// Fin comprobar RFID


        // Metodo para comprobobar que la caja pertenezca a la RUTA_Sede Seleccionada
        [WebMethod]
        public int Caja_pertenezca_sede(int id_caja, int id_ruta)
        {
            // Si la consulta trae una fila, es porque esa caja pertenece a la sede 
            // asi que mando la bandera como 1, Si no devuelve nada, mando la bandera a 0
            string queryString = "select id_caja from caja c join sede_logistica s "+
                                    " on C.ID_SEDE=S.ID_SEDE "+
                                    " where C.ID_CAJA=:IDCAJA and S.RUTA_SEDE=:RUTA_SEDE";
            OracleCommand command = new OracleCommand(queryString, connection);
            command.Parameters.Add(":RUTA_SEDE", OracleType.Char, 10).Value = id_ruta;
            command.Parameters.Add(":IDCAJA", OracleType.Char, 10).Value = id_caja;
            OracleDataReader lector;
            connection.Open();
            lector = command.ExecuteReader();

            if (lector.HasRows)
            {
                connection.Close();
                return 1;
            }
            else
            {
                connection.Close();
                return 0;
            }
        }


        //Metodo para comprobar que la caja este completa
        [WebMethod]
        public int comprobar_caja_completa(int id_caja)
        {
            // Si la consulta trae una fila, es porque esa caja ya esta completa
            // asi que mando la bandera como 1, Si no devuelve nada, mando la bandera a 0
            string queryString = "select id_caja from caja where ID_COMPLETO=1 and id_caja=:Caja";
            OracleCommand command = new OracleCommand(queryString, connection);
            command.Parameters.Add(":Caja", OracleType.Char, 10).Value = id_caja;
            OracleDataReader lector;
            connection.Open();
            lector = command.ExecuteReader();

            if (lector.HasRows)
            {
                connection.Close();
                return 1;
            }
            else
            {
                connection.Close();
                return 0;
            }
        }
        // Fin comprobar Caja Completa

        //Metodo para actualizar la caja como despachada
        [WebMethod]
        public void actualizar_caja(int id_caja)
        {
            
            // Actualiza la caja como despachada, para entrar aca tuvo que haber pasado por todas las 
            //verificaciones anteriores
            string queryString = "Update Caja set ID_ESTADO=2 where ID_CAJA=:CAJA";
            OracleCommand command = new OracleCommand(queryString, connection);
            command.Parameters.Add(":CAJA", OracleType.Char, 10).Value = id_caja;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        // Finaliza metodo para actualizar caja

        //Metodo para actualizar la caja como inconsistente, cuando se detecta en una RUTA_sede que no le pertenece
        [WebMethod]
        public void actualizar_caja_inconsistente(int id_caja, int id_ruta)
        {

            // Actualiza la caja como inconsistente porque se detecto en una ruta que no le pertenece
            // en el id_inconsistencia se pone el id de la ruta donde se encontro
            string queryString = "Update Caja set ID_INCON=:id_ruta where ID_CAJA=:CAJA";
            OracleCommand command = new OracleCommand(queryString, connection);
            command.Parameters.Add(":CAJA", OracleType.Char, 10).Value = id_caja;
            command.Parameters.Add(":id_ruta", OracleType.Char, 10).Value = id_ruta;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        // Finaliza metodo para actualizar caja Inconsistente

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        // Servicio Web para el Login
        [WebMethod]
        public int ComprobarUsuario(string nombre, String password)
        {
            string queryString = "SELECT * FROM vista_USERS WHERE NOMBRE_USER=:NOMBRE AND CONTRASENIA=:PASSWORD";
            OracleCommand command = new OracleCommand(queryString, connection);
            command.Parameters.Add(":NOMBRE", OracleType.Char, 10).Value = nombre;
            command.Parameters.Add(":PASSWORD", OracleType.Char, 10).Value = password;
            OracleDataReader lector;
            connection.Open();
            lector = command.ExecuteReader();

            if (lector.HasRows)
            {
                connection.Close();
                return 1;
            }
            else
                connection.Close();
                return 0;
        }
    }
}
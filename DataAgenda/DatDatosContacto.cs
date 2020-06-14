using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using TiDev.Entity.Agenda;

namespace TiDev.Data.Agenda
{
    public class DatDatosContacto
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ConnectionString);

        DatContacto dataC = new DatContacto();
        DatReferencia dataR = new DatReferencia();
        public EntDatosContacto Obtener(int Id)
        {
            EntDatosContacto Datos = new EntDatosContacto();

            EntContacto contacto = new EntContacto();
            DataRow fila = dataC.Obtener(Id);
            contacto.Id = Convert.ToInt32(fila["Id"]);
            contacto.Nombre = fila["Nombre"].ToString();
            contacto.Paterno = fila["Paterno"].ToString();
            contacto.Materno = fila["Materno"].ToString();
            contacto.Edad = Convert.ToInt32(fila["Edad"]);
            contacto.Nacimiento = Convert.ToDateTime(fila["Nacimiento"]);
            contacto.NomFoto = fila["NomFoto"].ToString();
            contacto.UserId = Convert.ToInt32(fila["UserId"]);
            contacto.Cumpleaños = FelizCumpleaños(contacto.Nombre);
            Datos.Contacto = contacto;

            List<EntReferencia> ls = new List<EntReferencia>();
            DataTable tab = new DataTable();
            tab = dataR.ObtenerPorContacto(Id);
            foreach (DataRow fil in tab.Rows)
            {
                EntReferencia r = new EntReferencia();
                r.Id = Convert.ToInt32(fil["Id"]);
                r.TipoId = Convert.ToInt32(fil["TipoId"]);
                r.ContactoId = Convert.ToInt32(fil["ContactoId"]);
                r.Dato = fil["Dato"].ToString();

                EntTipo t = new EntTipo();
                t.Id = Convert.ToInt32(fil["IdTipo"]);
                t.Nombre = fil["Nombre"].ToString();
                t.Estado = Convert.ToBoolean(fil["Estado"]);
                r.Tipo = t;
                ls.Add(r);
            }

            Datos.Referencias = ls;
            return Datos;
        }


        public Boolean FelizCumpleaños(String nombre)
        {
            SqlDataAdapter comando = new SqlDataAdapter($"select * from contacto where MONTH(Nacimiento) = MONTH(GETDATE()) and Day(Nacimiento) = Day(GetDate()) and Nombre ='{nombre}'", conexion);
            DataTable cumpleañeros = new DataTable();
            comando.Fill(cumpleañeros);
            if (cumpleañeros.Rows.Count == 1)
            {
                return true;
            }
            return false;
        }

    }
}

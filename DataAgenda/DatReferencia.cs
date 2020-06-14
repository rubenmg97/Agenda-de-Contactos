using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiDev.Data.Agenda
{
    public class DatReferencia
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ConnectionString);
        public DataTable Obtener()
        {
            SqlCommand comando = new SqlCommand("spObtenerReferencia", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable datos = new DataTable();
            data.Fill(datos);
            return datos;
        }

        public DataTable ObtenerPorContacto(int Id)
        {
            SqlCommand comando = new SqlCommand("spObtenerReferenciaContactoId", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", Id);

            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable datos = new DataTable();
            data.Fill(datos);
            return datos;
        }

        public DataRow Obtener(int id)
        {
            SqlCommand comando = new SqlCommand("spObtenerReferenciaId", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", id);
            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable datos = new DataTable();
            data.Fill(datos);
            return datos.Rows[0];
        }

        public int Delete(int Id)
        {
            int filasAfectadas = 0;
            SqlCommand comando = new SqlCommand("spDeleteReferencia", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", Id);
            try
            {
                conexion.Open();
                filasAfectadas = comando.ExecuteNonQuery();
                conexion.Close();
                return filasAfectadas;
            }
            catch (Exception)
            {
                conexion.Close();
                throw;
            }
        }

        public int Create(String dato, int tipo, int contacto)
        {
            int filasAfectadas = 0;
            SqlCommand comando = new SqlCommand("spCreateReferencia", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TipoId", tipo);
            comando.Parameters.AddWithValue("@ContactoId", contacto);
            comando.Parameters.AddWithValue("@Dato", dato);
            try
            {
                conexion.Open();
                filasAfectadas = comando.ExecuteNonQuery();
                conexion.Close();
                return filasAfectadas;
            }
            catch (Exception)
            {
                conexion.Close();
                throw;
            }
        }

        public int Edit(int id, String dato, int tipo, int contacto)
        {
            int filasAfectadas = 0;
            SqlCommand comando = new SqlCommand("spEditReferencia", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", id);
            comando.Parameters.AddWithValue("@TipoId", tipo);
            comando.Parameters.AddWithValue("@ContactoId", contacto);
            comando.Parameters.AddWithValue("@Dato", dato);
            try
            {
                conexion.Open();
                filasAfectadas = comando.ExecuteNonQuery();
                conexion.Close();
                return filasAfectadas;
            }
            catch (Exception)
            {
                conexion.Close();
                throw;
            }
        }

    }
}

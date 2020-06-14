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
    public class DatTipo
    {

        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ConnectionString);
        public DataTable Obtener()
        {
            SqlCommand comando = new SqlCommand("spObtenerTipo", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable datos = new DataTable();
            data.Fill(datos);
            return datos;
        }

        public DataRow Obtener(int Id)
        {
            SqlCommand comando = new SqlCommand("spObtenerTipoId", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable datos = new DataTable();
            data.Fill(datos);
            return datos.Rows[0];
        }

        public int Delete(int Id)
        {
            int filasAfectadas = 0;
            SqlCommand comando = new SqlCommand("spDeleteTipo", conexion);
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


        public int Create(String nombre, Boolean estado)
        {
            int filasAfectadas = 0;
            SqlCommand comando = new SqlCommand("spCreateTipo", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Estado", estado);
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

        public int Edit(int Id, String nombre, Boolean estado)
        {
            int filasAfectadas = 0;
            SqlCommand comando = new SqlCommand("spEditTipo", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", Id);
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Estado", estado);
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

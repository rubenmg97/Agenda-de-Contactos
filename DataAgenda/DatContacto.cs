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
    public class DatContacto
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ConnectionString);
        public DataRow Obtener(int Id)
        {
            SqlCommand comando = new SqlCommand("spObtenerContactoId", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", Id);

            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable datos = new DataTable();
            data.Fill(datos);
            return datos.Rows[0];
        }
        public DataTable ObtenerPorUsuario(int Id)
        {
            SqlCommand comando = new SqlCommand("SpObtenerIdUsuario", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", Id);

            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable datos = new DataTable();
            data.Fill(datos);
            return datos;
        }

        public DataTable Obtener(String nombre, int Id)
        {
            SqlCommand comando = new SqlCommand("spObtenerContactoNom", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Id", Id);

            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable datos = new DataTable();
            data.Fill(datos);
            return datos;
        }

        public DataRow Obtener(String nombre, String paterno, String materno)
        {
            SqlCommand comando = new SqlCommand("spContactoExistente", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Paterno", paterno);
            comando.Parameters.AddWithValue("@Materno", materno);

            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable datos = new DataTable();
            data.Fill(datos);
            return datos.Rows[0];
        }

        public int Delete(int Id)
        {
            int filasAfectadas = 0;
            SqlCommand comando = new SqlCommand("spDeleteContacto", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("Id", Id);
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

        public int Create(String nombre, String paterno, String materno, DateTime nacimiento,String nomFoto, int userId)
        {
            int filasAfectadas = 0;
            SqlCommand comando = new SqlCommand("spCreateContacto", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Paterno", paterno);
            comando.Parameters.AddWithValue("@Materno", materno);
            comando.Parameters.AddWithValue("@Nacimiento", nacimiento);
            comando.Parameters.AddWithValue("@NomFoto", nomFoto);
            comando.Parameters.AddWithValue("@UserId", userId);
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

        public int Edit(int Id, String nombre, String paterno, String materno, DateTime nacimiento, String nomFoto, int userId)
        {
            int filasAfectadas = 0;
            SqlCommand comando = new SqlCommand("spEditContacto", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", Id);
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Paterno", paterno);
            comando.Parameters.AddWithValue("@Materno", materno);
            comando.Parameters.AddWithValue("@Nacimiento", nacimiento);
            comando.Parameters.AddWithValue("@NomFoto", nomFoto);
            comando.Parameters.AddWithValue("@UserId",userId);
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

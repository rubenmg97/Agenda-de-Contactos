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
    public class DatUsuario
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ConnectionString);
        public DataTable Obtener()
        {
            SqlCommand comando = new SqlCommand("spObtenerUsuarios", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable datos = new DataTable();
            data.Fill(datos);
            return datos;
        }

        public DataRow Obtener(int Id)
        {
            SqlCommand comando = new SqlCommand("spObtenerUsuarioId", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", Id);
            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable datos = new DataTable();
            data.Fill(datos);
            return datos.Rows[0];
        }

        public Boolean Obtener(String nombre, String password)
        {
            SqlCommand comando = new SqlCommand("spValidarUsuario", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@NomUsuario", nombre);
            comando.Parameters.AddWithValue("@PasswordU", password);
            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable datos = new DataTable();
            data.Fill(datos);
            if (datos.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public DataRow Obtener(String nombre, String paterno, String materno)
        {
            SqlCommand comando = new SqlCommand("spUsuarioExistentePorNombre", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Paterno", paterno);
            comando.Parameters.AddWithValue("@Materno", materno);

            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable datos = new DataTable();
            data.Fill(datos);
            return datos.Rows[0];
        }

        public DataRow Obtener(String nomUsuario)
        {
            SqlCommand comando = new SqlCommand("SpObtenerNomUsuario", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@NomUsuario", nomUsuario);

            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable datos = new DataTable();
            data.Fill(datos);
            return datos.Rows[0];
        }

        public int Delete(int Id)
        {
            int filasAfectadas = 0;
            SqlCommand comando = new SqlCommand("spDeleteUsuario", conexion);
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


        public int Edit(int Id, String nombre, String paterno, String materno, String nomUsuario, DateTime nacimiento, String correo, String password, String facebook, String linkedin, String nomFoto)
        {
            int filasAfectadas = 0;
            SqlCommand comando = new SqlCommand("spEditUsuario", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", Id);
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Paterno", paterno);
            comando.Parameters.AddWithValue("@Materno", materno);
            comando.Parameters.AddWithValue("@NomUsuario", nomUsuario);
            comando.Parameters.AddWithValue("@Nacimiento", nacimiento);
            comando.Parameters.AddWithValue("@Correo", correo);
            comando.Parameters.AddWithValue("@PasswordU", password);
            comando.Parameters.AddWithValue("@Facebook", facebook);
            comando.Parameters.AddWithValue("@Linkedin", linkedin);
            comando.Parameters.AddWithValue("@NombreFoto", nomFoto);
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

        public int Create(String nombre, String paterno, String materno, String nomUsuario, DateTime nacimiento, String correo, String password, String facebook, String linkedin, String nomFoto)
        {
            int filasAfectadas = 0;
            SqlCommand comando = new SqlCommand("spCreateUsuario", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Paterno", paterno);
            comando.Parameters.AddWithValue("@Materno", materno);
            comando.Parameters.AddWithValue("@NomUsuario", nomUsuario);
            comando.Parameters.AddWithValue("@Nacimiento", nacimiento);
            comando.Parameters.AddWithValue("@Correo", correo);
            comando.Parameters.AddWithValue("@PasswordU", password);
            comando.Parameters.AddWithValue("@Facebook", facebook);
            comando.Parameters.AddWithValue("@Linkedin", linkedin);
            comando.Parameters.AddWithValue("@NombreFoto", nomFoto);
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

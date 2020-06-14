using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiDev.Data.Agenda;
using TiDev.Entity.Agenda;

namespace TiDev.Bussines.Agenda
{
    public class BusUsuario
    {
        DatUsuario data = new DatUsuario();
        public List<EntUsuario> Obtener()
        {
            List<EntUsuario> ls = new List<EntUsuario>();
            DataTable tabla = new DataTable();
            tabla = data.Obtener();

            foreach (DataRow fila in tabla.Rows)
            {
                EntUsuario user = new EntUsuario();
                user.Id = Convert.ToInt32(fila["Id"]);
                user.Nombre = fila["Nombre"].ToString();
                user.Paterno = fila["Paterno"].ToString();
                user.Materno = fila["Materno"].ToString();
                user.Nacimiento = Convert.ToDateTime(fila["Nacimiento"]);
                user.Edad = Convert.ToInt32(fila["Edad"]);
                user.Correo = fila["Correo"].ToString();
                user.Password = fila["PasswordU"].ToString();
                user.NomUsuario = fila["NomUsuario"].ToString();
                user.Facebook = fila["Facebook"].ToString();
                user.Linkedin = fila["Linkedin"].ToString();
                user.NomFoto = fila["NomFoto"].ToString();
                ls.Add(user);
            }
            return ls;
        }

        public EntUsuario Obtener(int Id)
        {
            DataRow fila = data.Obtener(Id);
            EntUsuario user = new EntUsuario();
            user.Id = Convert.ToInt32(fila["Id"]);
            user.Nombre = fila["Nombre"].ToString();
            user.Paterno = fila["Paterno"].ToString();
            user.Materno = fila["Materno"].ToString();
            user.NomUsuario = fila["NomUsuario"].ToString();
            user.Nacimiento = Convert.ToDateTime(fila["Nacimiento"]);
            user.Edad = Convert.ToInt32(fila["Edad"]);
            user.Correo = fila["Correo"].ToString();
            user.Password = fila["PasswordU"].ToString();
            user.Facebook = fila["Facebook"].ToString();
            user.Linkedin = fila["Linkedin"].ToString();
            user.NomFoto = fila["NomFoto"].ToString();
            return user;
        }

        public int Obtener(String nomUsuario)
        {
            DataRow fila = data.Obtener(nomUsuario);
            EntUsuario user = new EntUsuario();
            return Convert.ToInt32(fila["Id"]);
        }

        public EntUsuario ObtenerRepetido(EntUsuario usuario)
        {
            DataRow fila = data.Obtener(usuario.Nombre, usuario.Paterno, usuario.Materno);

            EntUsuario user = new EntUsuario();
            user.Id = Convert.ToInt32(fila["Id"]);
            user.Nombre = fila["Nombre"].ToString();
            user.Paterno = fila["Paterno"].ToString();
            user.Materno = fila["Materno"].ToString();
            user.Nacimiento = Convert.ToDateTime(fila["Nacimiento"]);
            user.Edad = Convert.ToInt32(fila["Edad"]);
            user.Correo = fila["Correo"].ToString();
            user.Password = fila["PasswordU"].ToString();
            user.NomUsuario = fila["NomUsuario"].ToString();
            user.Facebook = fila["Facebook"].ToString();
            user.Linkedin = fila["Linkedin"].ToString();
            user.NomFoto = fila["NomFoto"].ToString();

            return user;
        }

        public void Delete(EntUsuario usuario)
        {
            int filasAfectadas = data.Delete(usuario.Id);
            if (filasAfectadas != 1)
            {
                throw new ApplicationException("Error al Eliminar Usuario");
            }
        }

        public void Edit(EntUsuario usuario)
        {
            int filasAfectadas = data.Edit(usuario.Id, usuario.Nombre, usuario.Paterno, usuario.Materno, usuario.NomUsuario, usuario.Nacimiento, usuario.Correo, usuario.Password, usuario.Facebook, usuario.Linkedin, usuario.NomFoto);

            if (filasAfectadas != 1)
            {
                throw new ApplicationException("Error al Editar Usuario");
            }
        }


        public void Create(EntUsuario usuario)
        {
            if (data.Obtener(usuario.NomUsuario, usuario.Password) == true)
            {
                throw new ApplicationException("Error Usuario Existente");
            }
            else
            {
                int filasAfectadas = data.Create(usuario.Nombre, usuario.Paterno, usuario.Materno, usuario.NomUsuario, usuario.Nacimiento, usuario.Correo, usuario.Password, usuario.Facebook, usuario.Linkedin, usuario.NomFoto);
                if (filasAfectadas != 1)
                {
                    throw new ApplicationException("Error al Crear Usuario");
                }
            }
        }

        public Boolean Entrar(EntUsuario usuario)
        {
            if (data.Obtener(usuario.NomUsuario, usuario.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

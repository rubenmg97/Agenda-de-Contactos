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
    public class BusContacto
    {
        DatContacto data = new DatContacto();
        public EntContacto Obtener(int Id)
        {
            DataRow fila = data.Obtener(Id);

            EntContacto contacto = new EntContacto();
            EntTipo Tipo = new EntTipo();

            contacto.Id = Convert.ToInt32(fila["Id"]);
            contacto.Nombre = fila["Nombre"].ToString();
            contacto.Paterno = fila["Paterno"].ToString();
            contacto.Materno = fila["Materno"].ToString();
            contacto.Nacimiento = Convert.ToDateTime(fila["Nacimiento"]);
            contacto.Edad = Convert.ToInt32(fila["Edad"]);
            contacto.NomFoto = fila["NomFoto"].ToString();
            contacto.UserId = Convert.ToInt32(fila["UserId"]);
            return contacto;
        }

        public List<EntContacto> ObtenerPorUsuario(int Id)
        {
            List<EntContacto> ls = new List<EntContacto>();
            DataTable tabla = new DataTable();
            tabla = data.ObtenerPorUsuario(Id);
            foreach (DataRow fila in tabla.Rows)
            {
                EntContacto contacto = new EntContacto();

                contacto.Id = Convert.ToInt32(fila["Id"]);
                contacto.Nombre = fila["Nombre"].ToString();
                contacto.Paterno = fila["Paterno"].ToString();
                contacto.Materno = fila["Materno"].ToString();
                contacto.Edad = Convert.ToInt32(fila["Edad"]);
                contacto.Nacimiento = Convert.ToDateTime(fila["Nacimiento"]);
                contacto.NomFoto = fila["NomFoto"].ToString();
                contacto.UserId = Convert.ToInt32(fila["UserId"]);
                ls.Add(contacto);
            }
            return ls;
        }

        public List<EntContacto> Obtener(String nombre, int Id)
        {
            List<EntContacto> ls = new List<EntContacto>();
            DataTable tabla = new DataTable();
            tabla = data.Obtener(nombre, Id);

            foreach (DataRow fila in tabla.Rows)
            {
                EntContacto contacto = new EntContacto();
                contacto.Id = Convert.ToInt32(fila["Id"]);
                contacto.Nombre = fila["Nombre"].ToString();
                contacto.Paterno = fila["Paterno"].ToString();
                contacto.Materno = fila["Materno"].ToString();
                contacto.Edad = Convert.ToInt32(fila["Edad"]);
                contacto.Nacimiento = Convert.ToDateTime(fila["Nacimiento"]);
                contacto.NomFoto = fila["NomFoto"].ToString();
                contacto.UserId = Convert.ToInt32(fila["UserId"]);
                ls.Add(contacto);
            }
            return ls;
        }

        public void Delete(EntContacto contacto)
        {
            int filasAfectadas = data.Delete(contacto.Id);
            if (filasAfectadas != 1)
            {
                throw new ApplicationException("Error al Eliminar Contacto");
            }
        }
        public void Edit(EntContacto contacto)
        {
            int filasAfectadas = data.Edit(contacto.Id, contacto.Nombre, contacto.Paterno, contacto.Materno, contacto.Nacimiento, contacto.NomFoto,contacto.UserId);
            if (filasAfectadas != 1)
            {
                throw new ApplicationException("Error al Editar Contacto");
            }
        }

        public void Create(EntContacto contacto)
        {
            int filasAfectadas = data.Create(contacto.Nombre, contacto.Paterno, contacto.Materno, contacto.Nacimiento, contacto.NomFoto, contacto.UserId);
            if (filasAfectadas != 1)
            {
                throw new ApplicationException("Error al Crear Contacto");
            }
        }

        public void DeleteReferecias(EntContacto contacto)
        {
            BusReferencia comandoR = new BusReferencia();

            List<EntReferencia> referencias = new List<EntReferencia>();
            referencias = comandoR.ObtenerPorContacto(contacto.Id);
            foreach (EntReferencia r in referencias)
            {
                comandoR.Delete(r);
            }
        }
}
}

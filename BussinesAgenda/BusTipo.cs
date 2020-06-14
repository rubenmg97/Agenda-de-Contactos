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
    public class BusTipo
    {
        DatTipo data = new DatTipo();

        public List<EntTipo> Obtener()
        {
            List<EntTipo> ls = new List<EntTipo>();
            DataTable tabla = new DataTable();
            tabla = data.Obtener();
            foreach (DataRow fila in tabla.Rows)
            {
                EntTipo Tipo = new EntTipo();
                Tipo.Id = Convert.ToInt32(fila["Id"]);
                Tipo.Nombre = fila["Nombre"].ToString();
                Tipo.Estado = Convert.ToBoolean(fila["Estado"]);
                ls.Add(Tipo);
            }
            return ls;
        }

        public EntTipo Obtener(int id)
        {
            DataRow fila = data.Obtener(id);

            EntTipo Tipo = new EntTipo();
            Tipo.Id = Convert.ToInt32(fila["Id"]);
            Tipo.Nombre = fila["Nombre"].ToString();
            Tipo.Estado = Convert.ToBoolean(fila["Estado"]);

            return Tipo;
        }

        public void Delete(EntTipo tipo)
        {
            int filasAfectadas = data.Delete(tipo.Id);
            if (filasAfectadas != 1)
            {
                throw new ApplicationException("Error al Borrar Tipo de Contacto");
            }
        }

        public void Edit(EntTipo tipo)
        {
            int filasAfectadas = data.Edit(tipo.Id, tipo.Nombre, Convert.ToBoolean(tipo.Estado));
            if (filasAfectadas != 1)
            {
                throw new ApplicationException("Error al Editar Tipo de Contacto");
            }
        }
        public void Create(EntTipo tipo)
        {
            int filasAfectadas = data.Create(tipo.Nombre, Convert.ToBoolean(tipo.Estado));
            if (filasAfectadas != 1)
            {
                throw new ApplicationException("Error al Crear Tipo de Contacto");
            }
        }

    }
}

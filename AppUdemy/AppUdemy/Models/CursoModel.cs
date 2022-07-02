using System;
using System.Collections.Generic;
using System.Text;

namespace AppUdemy.Models
{
    public class CursoModel
    {
        // Propiedades
        public int ID { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Descripcion { get; set; }
        public string FechaDeCreacion { get; set; }
        public string FotoDelCursoBase64 { get; set; }
        public string NombreDelProfesor { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

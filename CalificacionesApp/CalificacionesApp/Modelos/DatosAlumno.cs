using System.Collections.Generic;

namespace CalificacionesApp.Modelos
{
    public class DatosAlumno
    {
        public Inputs Inputs { get; set; }
        public GlobalParameters GlobalParameters { get; set; }
    }

    public class Input1
    {
        public List<string> ColumnNames { get; set; }
        public List<List<string>> Values { get; set; }
    }

    public class Inputs
    {
        public Input1 input { get; set; }
    }

    public class GlobalParameters
    {
    }
}
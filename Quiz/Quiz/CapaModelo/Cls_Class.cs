using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz.CapaModelo
{
    public class Cls_Class
    {
        public int classId { get; set; }

        public int schoolId { get; set; }

        public string classNombre { get; set; }
        public string classDescripcion { get; set; }
        public string classEstado { get; set; }
    }
}
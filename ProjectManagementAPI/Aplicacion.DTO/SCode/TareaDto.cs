using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTO.SCode
{
    public class TareaDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
    public class TareaSaveDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Proyecto { get; set; }
    }
    public class TareaUpdateDto
    {
        public string Status { get; set; }
    }
}

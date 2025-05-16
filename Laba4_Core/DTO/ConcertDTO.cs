using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laba4_Core.Class;
using Laba4_Core.DTO;
using Laba4_Core.Interface;

namespace Laba4_Core.DTO
{
    public class ConcertDTO
    {
        public string Organizer { get; set; }
        public DateTime Date { get; set; }
        public List<PerformanceDTO> Performances { get; set; } = new();
    }
}

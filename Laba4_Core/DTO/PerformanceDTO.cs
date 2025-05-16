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
    public class PerformanceDTO
    {
        public Work Work { get; set; }
        public PerformerDTO Performer { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
    }
}

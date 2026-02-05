using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectedTurefCoordinatesToGeographic.Dtos
{
    public class InputEntityDto
    {
        public string ProjectedWKT { get; set; }
        public int CentralMeridian { get; set; }
    }
}

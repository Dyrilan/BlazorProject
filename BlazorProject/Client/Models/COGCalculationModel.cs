using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BlazorProject.Client.Models
{
    public class COGCalculationModel
    {        
        public double RearWheelBaseWeight { get; set; }
        public double CarWeight { get; set; }
        public double WheelBase { get; set; }

        public double RightSideWeight { get; set; }
        public double CarWeight2 { get; set; }
        public double Track { get; set; }

    }
}

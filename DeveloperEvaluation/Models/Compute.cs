using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeveloperEvaluation.Models
{
    public class Compute
    {
        public int? Average { get; set; }
        public int? Median { get; set; }
        public List<int?> Mode { get; set; }
        //[Required]
        public string NumbersWithComma { get; set; }
    }
}
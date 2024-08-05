using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api.Models
{
    public class Maps
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Run> Runs {get; set;} = new List<Run>();
    }
}
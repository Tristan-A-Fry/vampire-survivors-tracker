using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api.Models
{
    public class RunTool
    {
        public int Id {get; set;}
        public int RunId {get; set;}
        public int ToolId {get; set;}
        public int Level {get; set;}

        public Run Run {get; set;}
        public Tools Tool {get; set;}
    }
}
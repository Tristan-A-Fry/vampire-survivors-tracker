using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using web_api.Models;

namespace web_api.DTOS
{
    public class RunDto
    {
       public int Id {get; set;} 
       public int MapId {get; set;}
       public int CharacterId{get; set;}
       public int GoldEarned {get; set;}
       public DateTime EntryDate {get; set;}

       public List<WeaponsDto> Weapons {get; set;} = new List<WeaponsDto>();
       public List<ToolsDto> Tools {get; set;} = new List<ToolsDto>();
    }
}
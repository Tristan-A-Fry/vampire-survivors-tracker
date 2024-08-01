using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api.Models
{
    //Represents a single "Run" in a game
    public class Run
    {
        public int Id {get; set;}     
        public int MapId{get; set;}
        public int CharacterId {get; set;}

        public int GoldEarned {get; set;}
        public DateTime EntryDate {get; set;}

        public Maps Map {get; set;}
        public Characters Character {get; set;}
        public ICollection<RunWeapon> RunWeapons {get; set;}
        public ICollection<RunTool> RunTools {get; set;}
 
    }
}
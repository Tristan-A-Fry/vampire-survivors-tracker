using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api.Models
{
    public class RunWeapon
    {
        public int Id {get; set;}
        public int RunId {get; set;}
        public int WeaponId{get; set;}

        public int Level {get; set;}
        public bool IsEvolved {get; set;}

        public Run Run {get; set;}
        public Weapons Weapon {get ; set;}
    }
}
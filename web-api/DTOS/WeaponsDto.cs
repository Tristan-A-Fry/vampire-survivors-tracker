using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api.DTOS
{
    public class WeaponsDto
    {
       public int WeaponId {get; set;} 
       public int Level {get; set;}
       public bool IsEvolved {get; set;}
    }
}
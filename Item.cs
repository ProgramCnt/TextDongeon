using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDongeon
{
    public class Item
    {
        //장비명
        public string Name { get; set; }
        //장비 설명
        public string Description { get; set; }
        //무기 방어구 여부
        public bool isWeapon { get; set; }
        //장비 능력치
        public string Stat { get; set; }
    }
}

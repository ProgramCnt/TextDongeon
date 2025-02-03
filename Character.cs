using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TextDongeon
{
    public class Character
    {
        public int Level { get; set; }
        public string Name { get; private set; }
        public string Class { get; private set; }
        public float AttackPower { get; set; }
        public float Defense { get; set; }
        public float Health { get; set; }
        public float Gold { get; set; }
        public List<Item> Items { get; set; }

        public Character()
        {
            Level = 1;
            AttackPower = 10;
            Defense = 5;
            Health = 100;
            Gold = 1500;
            Items = new List<Item>();
        }
        public Character(string userName, string className)
        {
            Level = 1;
            Name = userName;
            Class = className;
            AttackPower = 10;
            Defense = 5;
            Health = 100;
            Gold = 1500;
            Items = new List<Item>();
        }

    }
}

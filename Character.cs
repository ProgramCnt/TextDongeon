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
        //도전에 사용할 것
        public bool WeaponEqipment { get; set; }
        public bool ArmerEqipment { get; set; }

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

        public void Additem(Item item)
        {
            Items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        public bool BuyItem(Item item)
        {
            if (Gold - item.Price >= 0)
            {
                Gold -= item.Price;
                Additem(item);
                item.isSold = true;
                return true;
            }else
            {
                return false;
            }
        }
        //판매기능은 도전기능에서 구현하기
        public void SellItem(Item item)
        {
            RemoveItem(item);
        }

        //장비 장착시키기
        //도전단계에서 무기,방어구 하나씩 끼울 수 있게 구현
        public void EquipItem(Item item)
        {
            if (!item.IsEquip)
            {
                item.IsEquip = true;
            }
        }

        public void UnEquipItem(Item item)
        {
            if (item.IsEquip)
            {
                item.IsEquip = false;
            }
        }
    }
}

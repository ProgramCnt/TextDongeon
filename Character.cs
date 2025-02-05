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
        public string Name { get; set; }
        public string Class { get; set; }
        public float AttackPower { get; set; }
        public float Defense { get; set; }
        public float Health { get; set; }
        public float Gold { get; set; }
        public List<Item> Items { get; set; }
        //도전에 사용할 것
        public bool WeaponEqipment { get; set; }
        public bool ArmerEqipment { get; set; }
        public int DongeonClear { get; set; }

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
            item.IsEquip = false;
            item.isSold = false;
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
            Gold += (float)(item.Price * 0.85);
            RemoveItem(item);
        }

        //장비 장착시키기
        //도전단계에서 무기,방어구 하나씩 끼울 수 있게 구현
        public void EquipItem(Item item)
        {
            if (item.Type.Equals("공격력"))
            {
                foreach (Item weapon in Items)
                {
                    if (weapon.IsEquip && weapon.Type.Equals("공격력"))
                    {
                        weapon.IsEquip = false;
                    }
                }
                AttackPower += item.Stat;
            }else if (item.Type.Equals("방어력"))
            {
                foreach (Item armer in Items)
                {
                    if (armer.IsEquip && armer.Type.Equals("방어력"))
                    {
                        armer.IsEquip = false;
                    }
                }
                Defense += item.Stat;
            }
            item.IsEquip = true;
        }

        public void UnEquipItem(Item item)
        {
            if (item.IsEquip)
            {
                item.IsEquip = false;
                if (item.Type.Equals("공격력"))
                {
                    AttackPower -= item.Stat;
                }
                else if (item.Type.Equals("방어력"))
                {
                    Defense -= item.Stat;
                }
            }
        }

        public void Rest()
        {
            Health = 100;
        }

        public bool CheckLevelUp()
        {
            if (Level == DongeonClear)
            {
                DongeonClear = 0;
                Level += 1;
                AttackPower += (float)0.5;
                Defense += 1;
                return true;
            }
            return false;
        }

        public float Damage(int difficulty, bool isFailed)
        {
            Random random = new Random();
            int recommandArmer = difficulty * 5 + (difficulty - 1);
            if (isFailed)
            {
                Health /= 2;
            }
            else
            {
                Health -= random.Next(20 - ((int)Defense - recommandArmer), 36 - ((int)Defense - recommandArmer));
            }
            return Health;
        }

        public float GetReward(int money)
        {
            Random random = new Random();
            Gold += money + (money * ((float)random.Next((int)AttackPower,(int)(AttackPower * 2 + 1))/100));
            return Gold;
        }
    }
}

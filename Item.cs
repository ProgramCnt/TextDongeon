using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDongeon
{
    public class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Stat { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsEquip { get; set; }

        public Item(string name, string description, string type, int stat, int price)
        {
            Name = name;
            Type = type;
            Stat = stat;
            Description = description;
            Price = price;
            IsEquip = false;
        }

        public Item(string itemInfo)
        {
            string[] itemInfos = itemInfo.Split('|');
            if (itemInfos.Length == 5)
            {
                Name = itemInfos[0];
                Type = itemInfos[1];
                Stat = int.Parse(itemInfos[2]);
                Description = itemInfos[3];
                Price = int.Parse(itemInfos[4]);
                IsEquip = false;
            }
            else
            {
                Console.WriteLine("오류! 아이템의 선언방식이 잘못되었습니다.");
            }
        }
    }
}

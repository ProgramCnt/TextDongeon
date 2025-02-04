using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDongeon
{
    class Util
    {
        public bool SelectRange(int start, int end, int selected)
        {
            if (start <= selected && selected <= end)
            {
                return true;
            }

            return false;
        }

        public void PrintWelcome()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
        }

        public void PrintUserChoice()
        {
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
        }

        public string GetMenuNameKorean(string enumMenu)
        {
            return enumMenu
                .Replace("Status", "상태 보기")
                .Replace("Inventory", "인벤토리")
                .Replace("ItemShop", "상점")
                .Replace("GoDongeon", "던전입장")
                .Replace("Rest", "휴식하기")
                .Replace("Save", "저장하기");
        }

        public bool IsWeapon(string type)
        {
            if (type.Equals("공격력"))
            {
                return true;
            }
            else if (type.Equals("방어력"))
            {
                return false;
            }
            return false;
        }

        public string ItemStatUtil(Item item)
        {
            if (item.Stat <= 0)
            {
                return item.Stat.ToString();
            }
            else
            {
                return "+" + item.Stat.ToString();
            }
        }

        public int UserSelectUtil(int start, int end)
        {
            while (true)
            {
                int userSelect = 0;
                int.TryParse(Console.ReadLine().ToString(), out userSelect);
                if (userSelect >= start && userSelect <= end)
                {
                    return userSelect;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.Write(">>");
                }
            }
        }

        public string DifficultyToKorean(string difficulty)
        {
            return difficulty
                .Replace("Easy", "쉬운")
                .Replace("Normal", "일반")
                .Replace("Hard", "어려운");
        }
    }
}

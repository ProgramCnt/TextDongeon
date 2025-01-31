using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDongeon
{
    internal class Util
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

        public void PrintUserChoice(int type)
        {
            if (type == 1)
            {
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
            }
        }

        public string GetMenuNameKorean(string enumMenu)
        {
            return enumMenu
                .Replace("Status", "상태 보기")
                .Replace("Inventory", "인벤토리")
                .Replace("ItemShop", "상점")
                .Replace("GoDongeon", "던전입장")
                .Replace("Rest", "휴식하기");
        }
    }
}

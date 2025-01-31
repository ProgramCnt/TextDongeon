using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDongeon
{
    internal class Menu
    {
        Util util = new Util();

        public void MainMenuList(Character character)
        {
            int menuCount = 0;
            int userSelect = 0;
            Console.Clear();
            util.PrintWelcome();
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            foreach (string enumName in Enum.GetNames(typeof(MenuList)))
            {
                Console.WriteLine($"{++menuCount}. {util.GetMenuNameKorean(enumName)}");
            }
            Console.WriteLine("");
            util.PrintUserChoice(0);

            while (true)
            {
                try
                {
                    userSelect = int.Parse(Console.ReadLine().ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("숫자를 입력해주세요.\n");
                    Console.Write(">>");
                    continue;
                }
                if (util.SelectRange(1, menuCount, userSelect))
                {
                    switch (userSelect)
                    {
                        case 1:CheckStatus(character);
                            break;
                        case 2:break;
                        case 3:break;
                    }
                } 
                else
                {
                    Console.WriteLine("지정된 범위를 벗어났습니다.\n");
                    Console.Write(">>");
                    continue;
                }
            }
        }

        public void CheckStatus(Character character)
        {
            int userSelect = 0;
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {character.Level}");
            Console.WriteLine($"{character.Name} ( {character.Class} )");
            Console.WriteLine($"공격력 : {character.AttackPower}");
            Console.WriteLine($"방어력 : {character.Defense}");
            Console.WriteLine($"체 력 : {character.Health}");
            Console.WriteLine($"Gold : {character.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            util.PrintUserChoice(0);
            while (true)
            {
                try
                {
                    userSelect = int.Parse(Console.ReadLine().ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("숫자를 입력해주세요.\n");
                    Console.Write(">>");
                    continue;
                }
                if (userSelect == 0)
                {
                    MainMenuList(character);
                }
                else
                {
                    Console.WriteLine("지정된 범위를 벗어났습니다.\n");
                    Console.Write(">>");
                    continue;
                }
            }
        }

        public void CheckInventory(Character character)
        {

        }
    }

    enum MenuList
    {
        Status = 1,
        Inventory,
        ItemShop,
        GoDongeon,
        Rest
    }
}
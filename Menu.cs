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

        public void CheckStatus(Character character)
        {
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
        }

        public void CheckInventory(Character character)
        {

        }
    }
}
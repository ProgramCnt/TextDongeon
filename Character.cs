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

        public Character setCharacter()
        {
            bool isFirst = true;
            string userName = "";
            string className = "";

            Util util = new Util();

            Character character;
            util.PrintWelcome();
            Console.WriteLine("원하시는 이름을 설정해주세요.\n");
            userName = Console.ReadLine().ToString();

            while (true)
            {
                int choiceName = 0;
                int choiceClass = 0;
                Console.WriteLine($"입력하신 이름은 {userName}입니다.\n");
                Console.WriteLine("1.저장");
                Console.WriteLine("2.취소");
                util.PrintUserChoice(1);
                try
                {
                    choiceName = int.Parse(Console.ReadLine().ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                    continue;
                }

                if (util.SelectRange(1, 2, choiceName) && choiceName == 1)
                {
                    //1번클릭시 직업 선택으로 넘어가기
                    while (true)
                    {
                        Console.Clear();
                        util.PrintWelcome();
                        Console.WriteLine("원하시는 직업을 선택해주세요.\n");
                        Console.WriteLine("1.전사");
                        Console.WriteLine("2.도적");
                        util.PrintUserChoice(1);
                        try
                        {
                            choiceClass = int.Parse(Console.ReadLine().ToString());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("숫자를 입력해주세요.");
                            continue;
                        }
                        if (choiceClass == 1)
                        {
                            className = "전사";
                        }
                        else if (choiceClass == 2)
                        {
                            className = "도적";
                        }
                        else if (!util.SelectRange(1, 2, choiceClass))
                        {
                            continue;
                        }
                        break;
                    }
                    character = new Character(userName, className);
                    break;
                }
                else if (choiceName == 2)
                {
                    //2번클릭시 이름 다시 설정하기
                    Console.WriteLine("원하시는 이름을 설정해주세요.\n");
                    userName = Console.ReadLine().ToString();
                    continue;
                }
                else if (!util.SelectRange(1, 2, choiceName))
                {
                    Console.WriteLine("범위를 벗어난 선택입니다. 다시 입력해주세요.\n");
                }
            }

            return character;
        }

    }
}

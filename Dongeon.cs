using System.Reflection.PortableExecutable;

internal class TextDongeon
{

    private static void Main(string[] args)
    {
        Character character = setCharacter();
    }

    public static void PrintWelcome()
    {
        Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
    }

    public static void PrintUserChoice(int type)
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

    public static Character setCharacter()
    {
        bool isFirst = true;
        string userName = "";
        string className = "";

        Character character;
        PrintWelcome();
        Console.WriteLine("원하시는 이름을 설정해주세요.\n");
        userName = Console.ReadLine().ToString();

        while (true)
        {
            int choiceName = 0;
            int choiceClass = 0;
            Console.WriteLine($"입력하신 이름은 {userName}입니다.\n");
            Console.WriteLine("1.저장");
            Console.WriteLine("2.취소");
            PrintUserChoice(1);
            try
            {
                choiceName = int.Parse(Console.ReadLine().ToString());
            } catch (Exception e) 
            {
                Console.WriteLine("숫자를 입력해주세요.");
                continue;
            }
            
            if (SelectRange(1,2,choiceName) && choiceName == 1)
            {
                //1번클릭시 직업 선택으로 넘어가기
                while (true)
                {
                    Console.Clear();
                    PrintWelcome();
                    Console.WriteLine("원하시는 직업을 선택해주세요.\n");
                    Console.WriteLine("1.전사");
                    Console.WriteLine("2.도적");
                    PrintUserChoice(1);
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
                    }else if (!SelectRange(1, 2, choiceClass))
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
            }else if (!SelectRange(1, 2, choiceName))
            {
                Console.WriteLine("범위를 벗어난 선택입니다. 다시 입력해주세요.\n");
            }
        }

        return character;
    }

    public void GoStatus()
    {
        Console.WriteLine("");
    }

    public static bool SelectRange(int start, int end, int selected)
    {
        if(start <= selected && selected <= end)
        {
            return true;
        }

        return false;
    }
}

public class Character
{
    public int Level { get; set; }
    public string Name { get; set; }
    public string Class { get; set; }
    public float AttackPower { get; set; }
    public float Defense { get; set; }
    public float Health { get; set; }
    public float Gold { get; set; }

    public Character()
    {
        Level = 1;
        AttackPower = 10;
        Defense = 5;
        Health = 100;
        Gold = 1500;
    }
    public Character(string userName,string className)
    {
        Level = 1;
        Name = userName;
        Class = className;
        AttackPower = 10;
        Defense = 5;
        Health = 100;
        Gold = 1500;
    }

}


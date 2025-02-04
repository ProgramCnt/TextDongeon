﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TextDongeon
{
    public class Menu
    {
        Util util = new Util();
        Character character;
        List<Item> shopItems = new List<Item>();

        public void MainMenuList()
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
            util.PrintUserChoice();

            while (true)
            {
                try
                {
                    userSelect = int.Parse(Console.ReadLine().ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.Write(">>");
                    continue;
                }
                if (util.SelectRange(1, menuCount, userSelect))
                {
                    switch (userSelect)
                    {
                        case 1:
                            CheckStatus();
                            break;
                        case 2:
                            CheckInventory();
                            break;
                        case 3:
                            ShoppingWeapons();
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.Write(">>");
                    continue;
                }
            }
        }

        public Character setCharacter()
        {
            bool isFirst = true;
            string userName = "";
            string className = "";

            Util util = new Util();

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
                util.PrintUserChoice();
                try
                {
                    choiceName = int.Parse(Console.ReadLine().ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("잘못된 입력입니다.");
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
                        util.PrintUserChoice();
                        try
                        {
                            choiceClass = int.Parse(Console.ReadLine().ToString());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            continue;
                        }
                        switch (choiceClass)
                        {
                            case 1:
                                className = "전사";
                                break;
                            case 2:
                                className = "도적";
                                break;
                            default:
                                Console.WriteLine("잘못된 입력입니다.");
                                break;
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

        public void CheckStatus()
        {
            int userSelect = 0;
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {character.Level}");
            Console.WriteLine($"{character.Name} ( {character.Class} )");
            if (character.Items.Count != 0)
            {
                int addAttackPower = 0;
                int addDefense = 0;
                var equipedItems = from items in character.Items where items.IsEquip select items;
                foreach (Item item in equipedItems)
                {
                    //무기인지 방어구인지 구분
                    if (util.IsWeapon(item.Type))
                    {
                        addAttackPower += item.Stat;
                    }
                    else
                    {
                        addDefense += item.Stat;
                    }
                }

                Console.WriteLine($"공격력 : {character.AttackPower + addAttackPower} ({(addAttackPower > 0 ? "+" : "")}{addAttackPower})");
                Console.WriteLine($"방어력 : {character.Defense + addDefense} ({(addDefense > 0 ? "+" : "")}{addDefense})");
                Console.WriteLine($"체 력 : {character.Health}");
            }
            else
            {
                Console.WriteLine($"공격력 : {character.AttackPower}");
                Console.WriteLine($"방어력 : {character.Defense}");
                Console.WriteLine($"체 력 : {character.Health}");
            }
            Console.WriteLine($"Gold : {character.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            util.PrintUserChoice();
            while (true)
            {
                try
                {
                    userSelect = int.Parse(Console.ReadLine().ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.Write(">>");
                    continue;
                }
                if (userSelect == 0)
                {
                    MainMenuList();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.Write(">>");
                    continue;
                }
            }
        }

        public void CheckInventory()
        {
            int userSelect = 0;
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");
            foreach (Item item in character.Items)
            {
                string status = util.ItemStatUtil(item);
                Console.WriteLine($" -  {(item.IsEquip ? "[E]" : "")}{item.Name,-8} | {item.Type} {status} | {item.Description}");
            }
            Console.WriteLine("");
            if (character.Items.Count != 0)
            {
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
            }
            else
            {
                Console.WriteLine("1. 장착 관리(장비 없음)");
                Console.WriteLine("0. 나가기");
            }
            Console.WriteLine("");
            util.PrintUserChoice();
            while (true)
            {
                try
                {
                    userSelect = int.Parse(Console.ReadLine().ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.Write(">>");
                    continue;
                }
                if (userSelect == 0)
                {
                    MainMenuList();
                }
                else if (userSelect == 1)
                {
                    EquipmentManagement();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.Write(">>");
                    continue;
                }
            }
        }

        public void EquipmentManagement()
        {
            int userSelect = 0;
            int itemIndex = 0;
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");
            foreach (Item item in character.Items)
            {
                string status = util.ItemStatUtil(item);
                Console.WriteLine($" - {itemIndex + 1} {(item.IsEquip ? "[E]":"")} {item.Name,-8} | {item.Type} {status} | {item.Description}");
                itemIndex++;
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            util.PrintUserChoice();
            while (true)
            {
                try
                {
                    userSelect = int.Parse(Console.ReadLine().ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.Write(">>");
                    continue;
                }
                if (userSelect == 0)
                {
                    CheckInventory();
                    break;
                }
                else if (userSelect <= character.Items.Count)
                {
                    if (character.Items[userSelect - 1].IsEquip)
                    {
                        character.UnEquipItem(character.Items[userSelect - 1]);
                    }else
                    {
                        character.EquipItem(character.Items[userSelect - 1]);
                    }
                    EquipmentManagement();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.Write(">>");
                    continue;
                }
            }
        }

        public void ShoppingWeapons()
        {
            int userSelect = 0;
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{character.Gold} G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            foreach (Item item in shopItems)
            {
                string status = util.ItemStatUtil(item);
                Console.WriteLine($" - {item.Name,-8} | {item.Type} {status} | {item.Description,-30} | {item.Price}");
            }
            Console.WriteLine("");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            util.PrintUserChoice();
            while (true)
            {
                try
                {
                    userSelect = int.Parse(Console.ReadLine().ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.Write(">>");
                    continue;
                }
                if (userSelect == 0)
                {
                    MainMenuList();
                    break;
                }
                else if(userSelect == 1)
                {
                    BuyItems(0);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.Write(">>");
                    continue;
                }
            }
        }

        public void SetShopItemList(List<Item> items)
        {
            foreach (Item item in items)
            {
                shopItems.Add(item);
            }
        }

        public void BuyItems(int buyStat)
        {
            int userSelect = 0;
            int itemCount = 0;
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{character.Gold} G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            foreach (Item item in shopItems)
            {
                string status = util.ItemStatUtil(item);
                Console.WriteLine($" - {++itemCount} {item.Name,-8} | {item.Type} {status} | {item.Description,-30} | {(item.isSold ? "구매완료" : item.Price + " G")}");
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");

            switch (buyStat)
            {
                case 1:
                    Console.WriteLine("");
                    Console.WriteLine("구매를 완료했습니다.");
                    break;
                case 2:
                    Console.WriteLine("");
                    Console.WriteLine("Gold가 부족합니다.");
                    break;
            }

            util.PrintUserChoice();
            while (true)
            {
                try
                {
                    userSelect = int.Parse(Console.ReadLine().ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.Write(">>");
                    continue;
                }
                if (userSelect == 0)
                {
                    ShoppingWeapons();
                    break;
                }
                else if (userSelect <= shopItems.Count)
                {
                    if (character.BuyItem(shopItems[userSelect - 1]))
                    {
                        BuyItems(1);
                    }
                    else
                    {
                        BuyItems(2);
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.Write(">>");
                    continue;
                }
            }
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

    enum BuyItemStatus
    {
        Default = 0,    //처음 들어왔을 때
        Buy,            //구매를 진행했을 때
        NoGold          //돈이 부족할때
    }
}
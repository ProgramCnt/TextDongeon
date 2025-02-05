using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;

namespace TextDongeon
{
    public class Menu
    {
        Util util = new Util();
        Character character;
        List<Item> shopItems = new List<Item>();
        string[] classList = { "전사", "도적" };
        private static string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string folderPath = Path.Combine(folder, "characterData");
        private static string filePath = Path.Combine(folderPath, "character.json");
        bool isSaved = false;
        bool isLoad = false;

        Dictionary<string, int> difficultyGold = new Dictionary<string, int>
        {
            { "Easy", 1000 },
            { "Normal", 1700 },
            { "Hard", 2500 }
        };

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
            if (isSaved)
            {
                Console.WriteLine("");
                Console.WriteLine("저장되었습니다.");
                isSaved = false;
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
                            EnterDongeon();
                            break;
                        case 5:
                            RestCharacter(0);
                            break;
                        case 6:
                            SaveData();
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

        public void SetCharacterName()
        {
            string userName = "";
            int userSelect = 0;
            Util util = new Util();
            character = new Character();

            Console.Clear();
            util.PrintWelcome();
            if (File.Exists(filePath))
            {
                Console.WriteLine("저장된 데이터가 존재합니다. 불러오시겠습니까?\n");
                Console.WriteLine("1. 예");
                Console.WriteLine("2. 아니오");
                Console.WriteLine("");
                util.PrintUserChoice();

                userSelect = util.UserSelectUtil(1,2);
                switch (userSelect)
                {
                    case 1:
                        character = LoadData();
                        SetShopItemList();
                        break;
                    case 2:
                        File.Delete(filePath);
                        SetCharacterName();
                        break;
                }
            }
            else
            {
                Console.WriteLine("원하시는 이름을 설정해주세요.\n");
                userName = Console.ReadLine().ToString();

                Console.WriteLine($"입력하신 이름은 {userName}입니다.\n");
                Console.WriteLine("1. 저장");
                Console.WriteLine("2. 취소");
                util.PrintUserChoice();

                userSelect = util.UserSelectUtil(1, 2);
                switch (userSelect)
                {
                    case 1:
                        SetCharacterClass(userName);
                        break;
                    case 2:
                        SetCharacterName();
                        break;
                }
            }
            
        }

        public void SetCharacterClass(string userName)
        {
            int classIndex = 0;
            int userSelect = 0;
            Console.Clear();
            util.PrintWelcome();
            Console.WriteLine("원하시는 직업을 설정해주세요.\n");
            foreach (string className in classList)
            {
                Console.WriteLine($"{++classIndex}. {className}");
            }
            util.PrintUserChoice();

            userSelect = util.UserSelectUtil(1, classList.Length);
            character = new Character(userName, classList[userSelect - 1]);

            SetShopItemList();
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

                Console.Write($"공격력 : {character.AttackPower + addAttackPower} ");
                if (addAttackPower != 0)
                {
                    Console.WriteLine($"({(addAttackPower > 0 ? "+" : "")}{addAttackPower})");
                }
                else
                {
                    Console.WriteLine("");
                }
                Console.Write($"방어력 : {character.Defense + addDefense} ");
                if (addDefense != 0)
                {
                    Console.WriteLine($"({(addDefense > 0 ? "+" : "")}{addDefense})");
                }
                else
                {
                    Console.WriteLine("");
                }
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

            userSelect = util.UserSelectUtil(0, 0);
            if (userSelect == 0)
            {
                MainMenuList();
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
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
            }
            Console.WriteLine("");
            util.PrintUserChoice();

            userSelect = util.UserSelectUtil(0, 1);
            switch (userSelect)
            {
                case 0:
                    MainMenuList();
                    break;
                case 1:
                    EquipmentManagement();
                    break;
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

            userSelect = util.UserSelectUtil(0, character.Items.Count);
            if (userSelect == 0)
            {
                CheckInventory();
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
                Console.WriteLine($" - {item.Name,-8} | {item.Type} {status} | {item.Description,-30} | {(item.isSold ? "구매완료" : item.Price + " G")}");
            }
            Console.WriteLine("");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            util.PrintUserChoice();

            userSelect = util.UserSelectUtil(0, 2);
            switch (userSelect)
            {
                case 0:
                    MainMenuList();
                    break;
                case 1:
                    BuyItems(0);
                    break;
                case 2:
                    SellItems(false);
                    break;
            }
        }

        public void SetShopItemList()
        {
            string[] itemList = 
            { 
                "수련자 갑옷|방어력|+5|수련에 도움을 주는 갑옷입니다.|1000", 
                "무쇠갑옷|방어력|+9|무쇠로 만들어져 튼튼한 갑옷입니다.|2000", 
                "스파르타의 갑옷|방어력|+15|스파르타의 전사들이 사용했다는 전설의 갑옷입니다.|3500", 
                "낡은 검|공격력|+2|쉽게 볼 수 있는 낡은 검 입니다.|600", 
                "청동 도끼|공격력|+5|어디선가 사용됐던거 같은 도끼입니다.|1500", 
                "스파르타의 창|공격력|+7|스파르타의 전사들이 사용했다는 전설의 창입니다.|2100", 
                "나무몽둥이|공격력|+1|어디선가 본 기본 무기입니다.|300" 
            };
            List<Item> shopList = new List<Item>();
            foreach (string itemStr in itemList)
            {
                shopList.Add(new Item(itemStr));
            }
            foreach (Item item in shopList)
            {
                if (isLoad)
                {
                    for (int i = 0; i < character.Items.Count; i++)
                    {
                        if (character.Items[i].Name.Equals(item.Name))
                        {
                            item.isSold = true;
                        }
                    }
                }
                shopItems.Add(item);
            }

            MainMenuList();
        }

        public void BuyItems(int buyStatus)
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

            switch (buyStatus)
            {
                case 1:
                    Console.WriteLine("");
                    Console.WriteLine("구매를 완료했습니다.");
                    break;
                case 2:
                    Console.WriteLine("");
                    Console.WriteLine("Gold가 부족합니다.");
                    break;
                case 3:
                    Console.WriteLine("");
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    break;
            }

            util.PrintUserChoice();
            
            userSelect = util.UserSelectUtil(0, shopItems.Count);
            if (userSelect == 0)
            {
                ShoppingWeapons();
            }
            else if (userSelect <= shopItems.Count)
            {
                if (shopItems[userSelect - 1].isSold)
                {
                    BuyItems(3);
                }
                else if (character.BuyItem(shopItems[userSelect - 1]))
                {
                    BuyItems(1);
                }
                else
                {
                    BuyItems(2);
                }
            }
        }

        public void SellItems(bool isSold)
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
            foreach (Item item in character.Items)
            {
                string status = util.ItemStatUtil(item);
                Console.WriteLine($" - {++itemCount} {item.Name,-8} | {item.Type} {status} | {item.Description,-30} | {item.Price}");
            }

            if (isSold)
            {
                Console.WriteLine("아이템을 판매하였습니다.");
            }

            Console.WriteLine("");
            Console.WriteLine("0. 나가기");

            util.PrintUserChoice();

            userSelect = util.UserSelectUtil(0, character.Items.Count);
            switch (userSelect)
            {
                case 0:
                    ShoppingWeapons();
                    break;
                default :
                    character.SellItem(character.Items[userSelect - 1]);
                    SellItems(true);
                    break;
            }
        }

        public void RestCharacter(int buyStatus)
        {
            int userSelect = 0;
            Console.Clear();
            Console.WriteLine("휴식하기");
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {character.Gold} G)\n");
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");

            switch (buyStatus)
            {
                case 1:
                    Console.WriteLine("");
                    Console.WriteLine("휴식을 완료했습니다.");
                    break;
                case 2:
                    Console.WriteLine("");
                    Console.WriteLine("Gold가 부족합니다.");
                    break;
            }
            util.PrintUserChoice();

            userSelect = util.UserSelectUtil(0, 1);
            switch (userSelect)
            {
                case 0:
                    MainMenuList();
                    break;
                case 1:
                    if (character.Gold >= 500)
                    {
                        character.Gold -= 500;
                        character.Rest();
                        RestCharacter(1);
                    }
                    else
                    {
                        RestCharacter(2);
                    }
                    break;
            }
        }

        public void EnterDongeon()
        {
            int userSelect = 0;
            int dongeonCount = 0;
            Console.Clear();
            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            foreach (string enumName in Enum.GetNames(typeof(DongeonDifficulty)))
            {
                int difficultyLevel = (int)Enum.Parse(typeof(DongeonDifficulty), enumName);
                int difficultyArmer = difficultyLevel * 5 + (difficultyLevel - 1);
                Console.WriteLine($"{++dongeonCount}. {util.DifficultyToKorean(enumName)} 던전     | 방어력 {difficultyArmer} 이상 권장");
            }
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            util.PrintUserChoice();

            userSelect = util.UserSelectUtil(0,dongeonCount);
            if(userSelect == 0)
            {
                MainMenuList();
            }else
            {
                IsDongeonClear(userSelect);
            }
        }

        public void IsDongeonClear(int difficulty)
        {
            Random random = new Random();
            int RecommandDefense = difficulty * 5 + (difficulty - 1);
            Console.WriteLine($"요구 방어력{RecommandDefense}");
            if (character.Defense >= RecommandDefense)
            {
                DongeonClear(difficulty);
            }
            else
            {
                int randomClear = random.Next(9);
                if (randomClear > 3)
                {
                    DongeonClear(difficulty);
                }
                else
                {
                    DongeonFailed(difficulty);
                }
            }
        }

        public void DongeonClear(int difficulty)
        {
            Random random = new Random();
            int RecommandDefense = difficulty * 5 + (difficulty - 1);
            int userSelect = 0;
            int damage = 0;
            string difficultyName = Enum.GetName(typeof(DongeonDifficulty), difficulty);
            int reward = difficultyGold[difficultyName];

            Console.Clear();
            Console.WriteLine("던전 클리어");
            Console.WriteLine("축하합니다!!");
            Console.WriteLine($"{util.DifficultyToKorean(difficultyName)}던전을 클리어 하였습니다.\n");

            character.DongeonClear++;
            if (character.CheckLevelUp())
            {
                Console.WriteLine("축하합니다. 레벨업 하셨습니다!!");
                Console.WriteLine($"현재 레벨은 {character.Level}입니다.");
            }

            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {character.Health} -> {character.Damage(difficulty,false)}");
            Console.WriteLine($"Gold {character.Gold} -> {character.GetReward(reward)}");
            Console.WriteLine("");

            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            util.PrintUserChoice();

            userSelect = util.UserSelectUtil(0, 0);
            if (userSelect == 0)
            {
                MainMenuList();
            }
        }

        public void DongeonFailed(int difficulty)
        {
            int userSelect = 0;
            Console.Clear();
            Console.WriteLine("던전 클리어 실패");
            Console.WriteLine("아깝습니다!!");
            Console.WriteLine($"{util.DifficultyToKorean(Enum.GetName(typeof(DongeonDifficulty), difficulty))}던전 클리어에 실패하였습니다.\n");

            character.Damage(difficulty, true);

            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            util.PrintUserChoice();

            userSelect = util.UserSelectUtil(0, 0);
            if (userSelect == 0)
            {
                MainMenuList();
            }
        }

        public void SaveData()
        {
            try
            {
                string json = JsonSerializer.Serialize(character, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                Directory.CreateDirectory(folderPath);

                File.WriteAllText(filePath, json);
                isSaved = true;
                MainMenuList();
            }
            catch
            {
                isSaved = false;
                MainMenuList();
            }
        }

        public Character LoadData()
        {
            string jsonStr = File.ReadAllText(filePath);
            Character character = JsonSerializer.Deserialize<Character>(jsonStr);
            isLoad = true;
            return character;
        }

        public void GameStart()
        {
            SetCharacterName();
        }
    }

    enum MenuList
    {
        Status = 1,
        Inventory,
        ItemShop,
        GoDongeon,
        Rest,
        Save
    }

    enum BuyStatus
    {
        Default = 0,    //처음 들어왔을 때
        Buy,            //구매를 진행했을 때
        NoGold,         //돈이 부족할때
        Sold            //이미 팔렸을때
    }

    enum DongeonDifficulty
    {
        Easy = 1,
        Normal,
        Hard
    }

}
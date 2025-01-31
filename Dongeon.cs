using System.Reflection.PortableExecutable;

namespace TextDongeon
{
    internal class TextDongeon
    {
        private static void Main(string[] args)
        {
            Character character = new Character();
            Menu menu = new Menu();
            character = character.setCharacter();
            menu.MainMenuList(character);
        }
    }
}
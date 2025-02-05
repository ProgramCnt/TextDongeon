using System.Reflection.PortableExecutable;

namespace TextDongeon
{
    internal class TextDongeon
    {
        private static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.GameStart();
        }
    }
}
using System.Reflection.PortableExecutable;

namespace TextDongeon
{
    internal class TextDongeon
    {

        private static void Main(string[] args)
        {
            Character character = new Character();
            character = character.setCharacter();
        }

        public void GoStatus()
        {
            Console.WriteLine("");
        }

    }

}
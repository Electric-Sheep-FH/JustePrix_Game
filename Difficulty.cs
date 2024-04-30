using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_JustePrix_Serialized
{
    public class Difficulty
    {
        public static int DifficultyChoice()
        {
            int toReturn;

            Console.WriteLine("Quel niveau de difficulté ?\n");
            Console.WriteLine("1 - Nombre aléatoire entre 1 et 100");
            Console.WriteLine("2 - Nombre aléatoire entre 1 et 1000 (points/2)");
            Console.WriteLine("3 - Nombre aléatoire entre 1 et 10000 (points/3)\n");
            Console.Write("Votre choix : ");

            bool wrongChoice = true;
            string choice = Console.ReadLine();

            while(wrongChoice)
            {
                if(choice.Trim() == "1" || choice.Trim() == "2" || choice.Trim() == "3")
                {
                    wrongChoice = false;
                } else
                {
                    Console.Write("Il semble y avoir une erreur dans votre choix ! Veuillez recommencer : ");
                    choice = Console.ReadLine();
                }
            }

            int parsedChoice;
            int.TryParse(choice, out parsedChoice);

            return parsedChoice;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TP_JustePrix_Serialized
{
    public class ScoreBoard
    {
        public string Player { get; set; }
        public int Score { get; set; }

        public ScoreBoard() { }
        public ScoreBoard(string player)
        {
            Player = player;
            Score = 0;
        }

        public ScoreBoard(string player, int score)
        {
            Player = player;
            Score = score;
        }

        public override string ToString()
        {
            return Player + " a deviné son nombre en " + Score + " coups.";
        }

        public static List<ScoreBoard> GetScoreList()
        {
            List<ScoreBoard> listToReturn = new List<ScoreBoard>();

            string jsonString = "";
            string jsonFileName = "Score.json";
            string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\" + jsonFileName;

            StreamReader sr = new StreamReader(path);
            jsonString = sr.ReadLine();
            sr.Close();
            if (!string.IsNullOrWhiteSpace(jsonString))
            {
                listToReturn = JsonSerializer.Deserialize<List<ScoreBoard>>(jsonString).OrderBy(x => x.Score).ToList();
            }
            return listToReturn;
        }

        public static void DisplayScoreboard()
        {
            List<ScoreBoard> listScore = GetScoreList();
            Console.WriteLine("############### 5 BEST SCORES ###############");
            for(int i = 0; i < listScore.Count; i++)
            {
                Console.WriteLine($"{i+1} - {listScore[i]}");
            }
            Console.WriteLine("#############################################");
        }

        public static void RegisterScore(GameManager game) 
        {
            ScoreBoard score = new ScoreBoard(game.player.Name, game.player.Score);
            List<ScoreBoard> CurrentScore = ScoreBoard.GetScoreList();
            CurrentScore = CurrentScore.OrderBy(x => x.Score).ToList();

            if (CurrentScore.Count() < 5)
                CurrentScore.Add(score);
            else if (CurrentScore.Count == 5 && CurrentScore[4].Score >= score.Score)
                CurrentScore[4] = score;

            string jsonString = JsonSerializer.Serialize(CurrentScore);
            string jsonFileName = "Score.json";
            string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\" + jsonFileName;

            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(jsonString);
            sw.Close();
        }
    }
}

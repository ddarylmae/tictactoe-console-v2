using System.Collections.Generic;

namespace TictactoeVer2
{
    public class ScoreBoard
    {
        private Dictionary<Player, int> Scores;

        public ScoreBoard()
        {
            Scores = new Dictionary<Player, int>
            {
                {Player.X, 0},
                {Player.O, 0},
            };
        }

        public int GetScoreOf(Player player)
        {
            return Scores[player];
        }

        public string GetScores()
        {
            return $"Current Scores:\nPlayer X - {Scores[Player.X]} \nPlayer O - {Scores[Player.O]} \n";
        }

        public void UpdateScore(Player player, int score)
        {
            Scores[player] = score;
        }
    }
}
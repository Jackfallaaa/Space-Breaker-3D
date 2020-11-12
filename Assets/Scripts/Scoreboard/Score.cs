namespace Scoreboard {
    public struct Score {
        public readonly string playerName;
        public readonly int score;
        public readonly int seed;

        public Score(string playerName, int score, int seed) {
            this.playerName = playerName;
            this.score = score;
            this.seed = seed;
        }
    }
}
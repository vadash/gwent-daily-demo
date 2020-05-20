namespace gwent_daily_reborn.Model.Cards.Extra
{
    internal class Token : Card
    {
        public Token(int x, int y, int width, int height, float confidence, int value = 0)
            : base(x, y, width, height, confidence, "Token", value)
        {
        }
    }
}

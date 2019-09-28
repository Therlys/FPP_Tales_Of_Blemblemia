namespace Game
{
    //Author: Mike Bédard
    public class HumanPlayer : CharacterOwner
    {
        public override void Win()
        {
            base.Win();
        }

        public override void Lose()
        {
            base.Lose();
        }

        public HumanPlayer() : base(Constants.PLAYER_NAME)
        {
        }
    }
}
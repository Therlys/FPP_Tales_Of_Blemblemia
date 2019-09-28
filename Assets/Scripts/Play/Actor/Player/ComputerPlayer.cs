namespace Game
{
    //Author: Mike Bédard
    public class ComputerPlayer : CharacterOwner
    {
        public override void Win()
        {
            base.Win();
        }

        public override void Lose()
        {
            base.Lose();
        }

        public ComputerPlayer() : base(Constants.AI_NAME)
        {
        }
    }
}
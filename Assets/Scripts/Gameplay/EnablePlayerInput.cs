using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;

namespace Platformer.Gameplay
{
    /// <summary>
    /// This event is fired when user input should be enabled.
    /// </summary>
    public class EnablePlayerInput : Simulation.Event<EnablePlayerInput>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public PlayerController mPlayer;

        public override void Execute()
        {
            PlayerController player;
            if (model.player == null)
            {
                player = mPlayer;
            }
            else
            {
                player = model.player;
            }
            player.controlEnabled = true;
        }
    }
}
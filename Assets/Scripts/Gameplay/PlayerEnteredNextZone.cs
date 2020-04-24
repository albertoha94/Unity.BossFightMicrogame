using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine.SceneManagement;

namespace Platformer.Gameplay
{

    /// <summary>
    /// This event is triggered when the player character enters a trigger with a NextSceneZone component.
    /// </summary>
    /// <typeparam name="PlayerEnteredNextZone"></typeparam>
    public class PlayerEnteredNextZone : Simulation.Event<PlayerEnteredNextZone>
    {
        public NextSceneZone nextSceneZone;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            model.player.controlEnabled = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
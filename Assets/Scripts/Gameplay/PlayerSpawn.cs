using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player is spawned after dying.
    /// </summary>
    public class PlayerSpawn : Simulation.Event<PlayerSpawn>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public PlayerController mPlayer;

        public override void Execute()
        {
            PlayerController player;
            if (model == null)
            {
                player = mPlayer;
            }
            else
            {
                player = model.player;
                if (model.player == null)
                {
                    player = mPlayer;
                }
            }
            player.collider2d.enabled = true;
            player.controlEnabled = false;
            if (player.audioSource && player.respawnAudio)
                player.audioSource.PlayOneShot(player.respawnAudio);
            player.health.Increment();
            player.Teleport(model.spawnPoint.transform.position);
            player.jumpState = PlayerController.JumpState.Grounded;
            player.animator.SetBool("dead", false);
            model.virtualCamera.m_Follow = player.transform;
            model.virtualCamera.m_LookAt = player.transform;

            var ev = Simulation.Schedule<EnablePlayerInput>(2f);
            ev.mPlayer = player;
        }
    }
}
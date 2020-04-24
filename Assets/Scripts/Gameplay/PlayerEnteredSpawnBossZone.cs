using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.Gameplay
{

    /// <summary>
    /// This event is triggered when the player character enters a trigger with a NextSceneZone component.
    /// </summary>
    /// <typeparam name="PlayerEnteredNextZone"></typeparam>
    public class PlayerEnteredSpawnBossZone : Simulation.Event<PlayerEnteredSpawnBossZone>
    {
        public GameObject boss;
        public PlayerController playerController;
        public GameObject spawnBossZoneGameObject;

        public override void Execute()
        {

            #region Stop player, spawn the boss and then move the player.
            playerController.controlEnabled = false;
            spawnBossZoneGameObject.SetActive(false);
            BossController bossController = boss.GetComponent<BossController>();
            if (bossController != null)
            {
                bossController.playerController = playerController;
                bossController.gameObject.SetActive(true);
            }
            #endregion

        }
    }
}
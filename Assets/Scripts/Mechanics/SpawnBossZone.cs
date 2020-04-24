using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

public class SpawnBossZone : MonoBehaviour
{

    public GameObject boss;

    void OnTriggerEnter2D(Collider2D collider)
    {
        var playerController = collider.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            var ev = Schedule<PlayerEnteredSpawnBossZone>();
            ev.playerController = playerController;
            ev.boss = boss;
            ev.spawnBossZoneGameObject = gameObject;
        }
    }
}

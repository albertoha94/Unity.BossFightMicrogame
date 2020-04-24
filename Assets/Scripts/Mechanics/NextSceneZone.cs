using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

public class NextSceneZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        var p = collider.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            var ev = Schedule<PlayerEnteredNextZone>();
            ev.nextSceneZone = this;
        }
    }
}

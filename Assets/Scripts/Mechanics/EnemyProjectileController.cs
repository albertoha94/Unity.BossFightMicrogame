using Platformer.Gameplay;
using Platformer.Mechanics;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using static Platformer.Core.Simulation;

public class EnemyProjectileController : MonoBehaviour
{

    #region Variables.
    private bool canMove = false;
    private bool isFast = false;
    private Vector3 positionToMove;
    #endregion

    internal void MoveTowards(Vector3 selectedPosition, bool isFast = false)
    {
        positionToMove = selectedPosition;
        canMove = true;
    }

    private void Update()
    {
        if (canMove && positionToMove != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, positionToMove, Time.deltaTime * (isFast ? 10f : 5f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            var ev = Schedule<PlayerDeath>();
            ev.mPlayer = player;
        }
    }
}

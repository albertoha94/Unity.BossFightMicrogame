using Platformer.Mechanics;
using System;
using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject enemyInstance;
    public GameObject projectilePrefab;
    private bool canAttack = false;
    private bool isAttacking = false;

    private void Awake()
    {
        StartCoroutine(CreateBoss());
    }

    private IEnumerator BossActions()
    {
        if (canAttack)
        {
            canAttack = false;
            yield return new WaitForSeconds(1);
            yield return PerformAttack();
        }
        else
        {
            yield return new WaitForSeconds(2);
            canAttack = true;
            yield return BossActions();
        }
    }

    /// <summary>
    /// Enables the boss gameObject.
    /// Enables the player and starts the attacks.
    /// </summary>
    public IEnumerator CreateBoss()
    {
        yield return new WaitForSeconds(2);
        if (enemyInstance != null) { enemyInstance.SetActive(true); }
        yield return new WaitForSeconds(3);
        playerController.controlEnabled = true;
        canAttack = true;
        yield return BossActions();
    }

    /// <summary>
    /// Chooses at random one of the attacks.
    /// </summary>
    private IEnumerator PerformAttack()
    {
        int attack = UnityEngine.Random.Range(0, 5);
        switch (attack)
        {
            default:
                return LaunchProjectiles();
        }
    }

    /// <summary>
    /// Code used to launch projectiles to the player.
    /// </summary>
    /// <returns>An IEnumerator.</returns>
    IEnumerator LaunchProjectiles()
    {

        // Get initial position of the projectiles and instatiate them.

        var projectileSpawnTransform = transform.GetChild(1).transform.GetChild(0);
        var bulletsArray = new GameObject[] {
        Instantiate(projectilePrefab, projectileSpawnTransform),
        Instantiate(projectilePrefab, projectileSpawnTransform),
        Instantiate(projectilePrefab, projectileSpawnTransform)
        };
        yield return new WaitForSeconds(1);

        // Put them in their respective position.
        var bulletsNewPositions = new Vector3[] {
            GetItemPosition(projectileSpawnTransform.GetChild(1).transform),
            GetItemPosition(projectileSpawnTransform.GetChild(2).transform),
            GetItemPosition(projectileSpawnTransform.GetChild(3).transform),
        };

        for (int i = 0; i <= bulletsNewPositions.Length - 1; i++)
        {
            var selectedPosition = bulletsNewPositions[i];
            var selectedProjectile = bulletsArray[i].GetComponent<EnemyProjectileController>();
            if (selectedProjectile != null)
            {
                selectedProjectile.MoveTowards(selectedPosition);
            }
        }

        yield return new WaitForSeconds(1);

        // Each bullet will attack the player and cause damage.
        foreach (var bullet in bulletsArray)
        {
            var rigidBody2D = bullet.GetComponent<BoxCollider2D>();
            if (rigidBody2D != null)
            {
                rigidBody2D.enabled = true;
            }

            var selectedProjectile = bullet.GetComponent<EnemyProjectileController>();
            if (selectedProjectile != null)
            {
                selectedProjectile.MoveTowards(playerController.transform.position, true);
            }
        }

        // Destroy the bullet aftewards.
        yield return new WaitForSeconds(2);
        foreach (var bullet in bulletsArray)
        {
            Destroy(bullet.transform.gameObject);
        }

        // Continue with boss actions.
        yield return BossActions();
    }

    private Vector3 GetItemPosition(Transform transform)
    {
        return transform.TransformPoint(transform.localPosition);
    }
}

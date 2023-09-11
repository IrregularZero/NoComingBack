using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWizard_l1_Behaviour : StandardProjectileShooter_l1_Behaviour
{
    private bool teleported = false;
    private bool shieldInitiated = false;

    [SerializeField]
    private GameObject shieldPrefab;
    private GameObject actualShield;
    [SerializeField]
    private GameObject LineToSheildedAlly;
    private GameObject actualLineToShieldedAlly;

    protected override void Update()
    {
        if (vitality.Health <= 0)
            Death();

        if (canFire)
        {
            if (!teleported)
            {
                TeleportToFurthestPoint();
            }
            else
            {
                if (!shieldInitiated)
                {
                    Fire();
                }
                else if (shieldInitiated && actualShield == null)
                {
                    Destroy(actualLineToShieldedAlly);
                    StartCoroutine(Reload());
                }
            }
        }
    }
    private void TeleportToFurthestPoint()
    {
        try
        {
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
            if (spawnPoints == null || spawnPoints.Length <= 0)
                throw new System.Exception($"{name}: No spawn point found");

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject furthestPoint = new GameObject();
            float distanceToFuthestPoint = -1f;
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (spawnPoints[i].GetComponent<EnemySpawnPoint>().IsTaken)
                    continue;

                float distanceFromSpawnToPlayer = new Vector3(player.transform.position.x - spawnPoints[i].transform.position.x,
                    player.transform.position.y - spawnPoints[i].transform.position.y,
                    player.transform.position.z - spawnPoints[i].transform.position.z).magnitude;

                if (distanceFromSpawnToPlayer > distanceToFuthestPoint)
                {
                    furthestPoint = spawnPoints[i];
                    distanceToFuthestPoint = distanceFromSpawnToPlayer;
                }
            }

            if (furthestPoint == new GameObject())
                throw new System.Exception($"{name}: All spawn points taken"); 
            
            transform.parent.parent.GetComponent<EnemySpawnPoint>().IsTaken = false;
            furthestPoint.GetComponent<EnemySpawnPoint>().IsTaken = true;

            transform.parent = furthestPoint.transform.GetChild(0);
            transform.localPosition = Vector3.zero;
            teleported = true;

            Debug.Log($"{name}: Teleported to the farthest point from the player");
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    // Firing Wizard initiating shield
    public override void Fire()
    {
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject targetedAlly = new GameObject();
        for (int i = 0; i < allies.Length; i++)
        {
            if (allies[i].GetComponent<NPCVitality>().Health <= 0)
                continue;

            if (allies[i].name.IndexOf("Seeker_1l") >= 0)
            {
                targetedAlly = allies[i];
            }
            else if (allies[i].name.IndexOf("FullAutoShooter_1l") >= 0)
            {
                if (targetedAlly.name.IndexOf("Seeker_1l") >= 0)
                    continue;
                targetedAlly = allies[i];
            }
            else if (allies[i].name.IndexOf("SemiAutoShooter_1l") >= 0)
            {
                if (targetedAlly.name.IndexOf("FullAutoShooter_1l") >= 0)
                    continue;
                targetedAlly = allies[i];
            }
            else if (allies[i].name.IndexOf("SlowShooter_1l") >= 0)
            {
                if (targetedAlly.name.IndexOf("SemiAutoShooter_1l") >= 0)
                    continue;
                targetedAlly = allies[i];
            }
            else if (allies[i].name.IndexOf("Sniper_1l") >= 0)
            {
                if (targetedAlly.name.IndexOf("SlowShooter_1l") >= 0)
                    continue;
                targetedAlly = allies[i];
            }
        }

        actualShield = Instantiate(shieldPrefab, targetedAlly.transform);
        actualLineToShieldedAlly = Instantiate(LineToSheildedAlly);

        actualLineToShieldedAlly.GetComponent<LineRenderer>().SetPosition(0, transform.position);
        actualLineToShieldedAlly.GetComponent<LineRenderer>().SetPosition(1, actualShield.transform.position);

        shieldInitiated = true;
    }
    public override IEnumerator Reload()
    {
        canFire = false;

        yield return new WaitForSeconds(reloadTime);
        teleported = false;
        shieldInitiated = false;

        canFire = true;
    }
    public override void Death()
    {
        base.Death();
        Destroy(actualLineToShieldedAlly);
        Destroy(actualShield);
    }
}

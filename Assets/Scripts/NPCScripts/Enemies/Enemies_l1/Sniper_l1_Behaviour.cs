using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper_l1_Behaviour : StandardProjectileShooter_l1_Behaviour
{
    private Transform playerPosition;

    private bool seesPlayer = false;

    private float reloadTimer;
    [SerializeField]
    private float braceTime;

    [SerializeField]
    private Color AimedUpColor;
    [SerializeField]
    private Color braceColor;

    [SerializeField]
    private GameObject sightTrail;
    private GameObject actualSightTrail;

    protected override void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        actualSightTrail = Instantiate(sightTrail, Vector3.zero, Quaternion.identity);
        reloadTimer = reloadTime;

        base.Start();
    }
    protected override void Update()
    {
        if (vitality.Health <= 0)
        {
            Death();
            return;
        }

        Vector3 directionToPlayer = new Vector3(playerPosition.position.x - transform.position.x, 
            playerPosition.position.y - transform.position.y, 
            playerPosition.position.z - transform.position.z);
        Ray rayToThePlayer = new Ray(transform.position, directionToPlayer);
        RaycastHit hitInfo;
        if (Physics.Raycast(rayToThePlayer, out hitInfo, directionToPlayer.magnitude * 2, 1))
        {
            seesPlayer = hitInfo.collider.tag == "Player";

            actualSightTrail.GetComponent<LineRenderer>().SetPosition(0, transform.position);
            actualSightTrail.GetComponent<LineRenderer>().SetPosition(1, hitInfo.transform.position);
        }

        if (canFire)
        {
            if (seesPlayer)
            {
                reloadTimer -= Time.deltaTime;

                if (reloadTimer <= braceTime)
                {
                    actualSightTrail.GetComponent<LineRenderer>().material.color = braceColor;
                }
                else
                {
                    actualSightTrail.GetComponent<LineRenderer>().material.color = AimedUpColor;
                }

                if (reloadTimer <= 0)
                {
                    Fire();
                }
            }
            else
            {
                reloadTimer = reloadTime;
            }
        }
    }

    public override IEnumerator Reload()
    {
        actualSightTrail.GetComponent<LineRenderer>().enabled = false;
        canFire = false;
        yield return new WaitForSeconds(reloadTime);
        reloadTimer = reloadTime;
        canFire = true;
        actualSightTrail.GetComponent<LineRenderer>().enabled = true;
    }
    public override void Death()
    {
        base.Death();
        Destroy(actualSightTrail);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutoShooter_l1_Behaviour : StandardProjectileShooter_l1_Behaviour
{
    [SerializeField]
    private int chain;
    private int leftInChain;

    [SerializeField]
    private float spacingBetweenShots;

    protected override void Update()
    {
        if (vitality.Health <= 0)
            Death();

        if (canFire)
        {
            canFire = false;
            leftInChain = chain;
            InvokeRepeating("Fire", 0, spacingBetweenShots);
        }
    }

    public override void Fire()
    {
        GameObject activeProjectile = Instantiate(projectile, transform.GetChild(1).position, transform.rotation);
        activeProjectile.GetComponent<EnemiesProjectile>().SetupProjectile(speed, damage, projectileColor);

        leftInChain--;
        if (leftInChain <= 0)
        {
            StartCoroutine(Reload());
            CancelInvoke();
        }
    }
}
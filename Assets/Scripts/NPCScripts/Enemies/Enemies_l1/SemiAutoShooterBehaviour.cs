using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutoShooterBehaviour : StandardProjectileShooterBehaviour
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
        activeProjectile.GetComponent<EnemiesProjectile>().SetupProjectile(speed, damage);

        leftInChain--;
        if (leftInChain <= 0)
        {
            StartCoroutine(Reload());
            CancelInvoke();
        }
    }
}
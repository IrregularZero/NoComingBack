using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class StandardProjectileShooterBehaviour : MonoBehaviour
{
    private NPCVitality vitality;
    private bool canFire = false;
    [SerializeField]
    private float reloadTime;

    [SerializeField]
    private float damage;
    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject projectile;

    private void Start()
    {
        vitality = GetComponent<NPCVitality>();

        ConstraintSource cs = new ConstraintSource();
        cs.sourceTransform = GameObject.FindGameObjectWithTag("Player").transform;
        cs.weight = 1.0f;
        GetComponent<LookAtConstraint>().AddSource(cs);
        GetComponent<LookAtConstraint>().constraintActive = true;

        StartCoroutine(Reload());
    }
    private void Update()
    {
        if (vitality.Health <= 0)
            Death();

        if (canFire)
        {
            GameObject activeProjectile = Instantiate(projectile, transform.GetChild(1).position, transform.rotation);
            activeProjectile.GetComponent<EnemiesProjectile>().SetupProjectile(speed, damage);
            canFire = false;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        canFire = true;
    }

    private void Death()
    {
        StopAllCoroutines();
        canFire = false;
        GetComponent<LookAtConstraint>().constraintActive = false;
    }
}

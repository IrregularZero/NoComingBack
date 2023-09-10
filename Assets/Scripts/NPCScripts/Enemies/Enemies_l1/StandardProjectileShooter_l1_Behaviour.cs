using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class StandardProjectileShooter_l1_Behaviour : MonoBehaviour
{
    protected NPCVitality vitality;

    protected bool canFire = false;
    [SerializeField]
    protected float reloadTime;

    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected Color projectileColor;

    [SerializeField]
    protected GameObject projectile;

    protected virtual void Start()
    {
        vitality = GetComponent<NPCVitality>();

        ConstraintSource cs = new ConstraintSource();
        cs.sourceTransform = GameObject.FindGameObjectWithTag("Player").transform;
        cs.weight = 1.0f;
        GetComponent<LookAtConstraint>().AddSource(cs);
        GetComponent<LookAtConstraint>().constraintActive = true;

        StartCoroutine(Reload());
    }
    protected virtual void Update()
    {
        if (vitality.Health <= 0)
            Death();

        if (canFire)
            Fire();
    }
    
    public virtual void Fire()
    {
        GameObject activeProjectile = Instantiate(projectile, transform.GetChild(1).position, transform.rotation);
        activeProjectile.GetComponent<EnemiesProjectile>().SetupProjectile(speed, damage, projectileColor); 
        StartCoroutine(Reload());
    }

    public virtual IEnumerator Reload()
    {
        canFire = false;
        yield return new WaitForSeconds(reloadTime);
        canFire = true;
    }

    public virtual void Death()
    {
        StopAllCoroutines();
        canFire = false;
        GetComponent<LookAtConstraint>().constraintActive = false;
    }
}

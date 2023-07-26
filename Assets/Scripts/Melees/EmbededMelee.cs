using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbededMelee : Melee
{

    protected override void Start()
    {
        base.Start();
    }

    public override void Damage()
    {
        if (!isAttacking)
            return;

        Ray ray = new Ray(GameObject.FindGameObjectWithTag("MainCamera").transform.position, GameObject.FindGameObjectWithTag("MainCamera").transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, 1))
        {
            if (hitInfo.collider.tag != "Player" && hitInfo.collider.GetComponent<VitalitySystem>() != null)
            {
                if (hitInfo.collider.GetComponent<VitalitySystem>().Health - damage <= 0)
                {
                    // Finisher
                    GameObject.FindGameObjectWithTag("Player").GetComponent<VitalitySystem>().RestoreHealth(finisherHPBonus);
                    hitInfo.collider.GetComponent<VitalitySystem>().TakeDamage(damage);
                }
                hitInfo.collider.GetComponent<VitalitySystem>().TakeDamage(damage);
            }
        }
        isAttacking = false;
    }

    public override void Use()
    {
        if (animator.GetInteger("Hit") + 1 > maxHits)
            return;
        if (hits + 1 > animator.GetInteger("Hit") + 1)
            return;

        if (!isAttacking)
            attackDuration = maxAttackDuration;

        isAttacking = true;
        hits++;
    }
    public override void SpecialUse()
    {
        throw new System.NotImplementedException();
    }
}

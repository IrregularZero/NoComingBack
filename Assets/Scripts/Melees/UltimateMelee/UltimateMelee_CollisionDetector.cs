using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateMelee_CollisionDetector : MonoBehaviour
{
    private UltimateMelee melee;

    private void Start()
    {
        melee = transform.parent.gameObject.GetComponent<UltimateMelee>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (melee.IsAttacking)
        {
            if (other.tag == "Enemy")
            {
                switch (melee.LastAttackPerformed)
                {
                    case "horizontalhit": other.gameObject.GetComponent<VitalitySystem>().TakeDamage(melee.HorizontalHitDamage); break;
                    case "verticalhit": other.gameObject.GetComponent<VitalitySystem>().TakeDamage(melee.VerticalHitDamage); break;
                }
            }
        }
    }
}

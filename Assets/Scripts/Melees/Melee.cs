using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Melee : MonoBehaviour
{
    [SerializeField]
    protected bool isAttacking;
    [SerializeField]
    protected bool hasAttacked;

    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float distance;
    [SerializeField]
    protected float maxAttackDuration;
    protected float attackDuration;
    [SerializeField]
    protected float impactFrame;
    [SerializeField]
    protected float finisherHPBonus;

    [SerializeField]
    protected int maxHits;
    protected int hits;
    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        if (attackDuration > 0)
        {
            attackDuration -= Time.deltaTime;

            if (attackDuration <= impactFrame && !hasAttacked)
            {
                hasAttacked = true;
                Damage();
            }
            if (attackDuration <= 0)
            {
                if (hits > animator.GetInteger("Hit"))
                {
                    hasAttacked = false;
                    animator.SetInteger("Hit", hits);
                    attackDuration = maxAttackDuration;
                }
                else
                {
                    isAttacking = false;
                    hasAttacked = false;
                    hits = 0;
                    animator.SetInteger("Hit", hits);
                }
            }
            
        }
    }

    public abstract void Damage();

    public abstract void Use();
    public abstract void SpecialUse();
}

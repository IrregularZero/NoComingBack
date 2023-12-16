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

    #region Protected
    public bool IsAttacking 
    {
        get
        {
            return isAttacking;
        } 
    }
    public bool HasAttacked 
    {
        get
        {
            return hasAttacked;
        }
    }

    public float DamageModificator 
    {
        get
        {
            return damage;
        }
        set
        {
            if (value >= 0)
            {
                damage = value;
            }
            else
            {
                damage = 0;
            }
        }
    }
    public float Distance 
    {
        get
        {
            return distance;
        }
        set
        {
            if (value >= 0)
            {
                distance = value;
            }
            else
            {
                distance = 0;
            }
        }
    }
    public float MaxAttackDuration 
    {
        get
        {
            return maxAttackDuration;
        }
        set
        {
            if (value >= 0)
            {
                maxAttackDuration = value;
            }
            else
            {
                maxAttackDuration = 0;
            }
        }
    }
    public float ImpactFrame 
    {
        get
        {
            return impactFrame;
        }
        set
        {
            if (value >= 0)
            {
                impactFrame = value;
            }
            else
            {
                impactFrame = 0;
            }
        }
    }
    public float FinisherHPBonus 
    {
        get
        {
            return finisherHPBonus;
        }
        set
        {
            finisherHPBonus = value;
        }
    }
    #endregion

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
                    animator.SetInteger("Hit", animator.GetInteger("Hit") + 1);
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

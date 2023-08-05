using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateMelee : MonoBehaviour
{
    private bool isAttacking;
    private string lastAttackPerformed;

    [SerializeField]
    private float horizontalHitDamage = 50f;
    [SerializeField]
    private float horizontalHitDuration = 0.3f;
    [SerializeField]
    private float verticalHitDamage = 100f;
    [SerializeField]
    private float verticalHitDuration = 0.3f;

    private Animator animator;

    #region Properties
    public bool IsAttacking 
    {
        get
        {
            return isAttacking;
        } 
    }
    public string LastAttackPerformed 
    {
        get
        {
            return lastAttackPerformed;
        } 
    }
    public float HorizontalHitDamage 
    {
        get
        {
            return horizontalHitDamage;
        }
        set
        {
            if (value >= 0)
            {
                horizontalHitDamage = value;
            }
        }
    }
    public float HorizontalHitDuration 
    {
        get
        {
            return horizontalHitDuration;
        }
        set
        {
            if (value > 0) 
            {
                horizontalHitDuration = value;
            }
        }
    }
    public float VerticalHitDamage 
    {
        get
        {
            return verticalHitDamage;
        }
        set
        {
            if (value >= 0)
            {
                verticalHitDamage = value;
            }
        }
    }
    public float VerticalHitDuration 
    {
        get
        {
            return verticalHitDuration;
        }
        set
        {
            if (value > 0) 
            {
                verticalHitDuration = value;
            }
        }
    }
    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();
        lastAttackPerformed = string.Empty;
    }

    private void OnEnable()
    {
        GetComponent<Animator>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        GetComponent<Animator>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void PerformHorizontalHit()
    {
        if (isAttacking) 
            return;

        isAttacking = true;
        animator.SetTrigger("HitHorizontaly");
        lastAttackPerformed = "horizontalhit";
        StartCoroutine(ResetHitDuration(horizontalHitDuration));
    }
    public void PerformVerticalHit()
    {
        if (isAttacking) 
            return;

        isAttacking = true;
        animator.SetTrigger("HitVerticaly");
        lastAttackPerformed = "verticalhit";
        StartCoroutine(ResetHitDuration(verticalHitDuration));
    }

    private IEnumerator ResetHitDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        isAttacking = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Animations;

public sealed class NPCVitality : VitalitySystem
{
    [SerializeField]
    private bool useAnimations = true;

    private Animator animator; // used to animate HP & death

    [SerializeField]
    private float maxDamagedAnimDuration;
    [SerializeField]
    private bool staggered = false;
    private float staggeredDuration;
    private string weaponTypeDamaged;

    [SerializeField]
    private float expirationTimer = 15f;


    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();

        if (frontHealthBar != null)
        {
            ConstraintSource cs = new ConstraintSource();
            cs.sourceTransform = GameObject.FindGameObjectWithTag("Player").transform;
            cs.weight = 1.0f;
            transform.GetChild(0).GetComponent<LookAtConstraint>().AddSource(cs);
            transform.GetChild(0).GetComponent<LookAtConstraint>().constraintActive = true;
        }

        weaponTypeDamaged = string.Empty;
    }

    protected override void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (frontHealthBar != null)
            UpdateHealthUI();

        if (health <= 0)
            Death(true);

    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (useAnimations)
            animator.SetTrigger("Damaged");
        StartCoroutine(StaggeredState());
    }
    public void TakeDamage(float damage, string weaponTypeDamaged)
    {
        base.TakeDamage(damage);

        this.weaponTypeDamaged = weaponTypeDamaged;
        if (useAnimations)
            animator.SetTrigger("Damaged");
        StartCoroutine(StaggeredState());
    }

    public IEnumerator StaggeredState()
    {
        staggered = true;
        yield return new WaitForSeconds(staggeredDuration);
        staggered = false;
    }

    public override void RestoreHealth(float healAmount)
    {
        base.RestoreHealth(healAmount);
    }

    public override IEnumerator Death(bool isDead)
    {
        // For enemies with AI, it should be disabled

        if (useAnimations)
            animator.SetBool("IsDead", isDead);

        switch (weaponTypeDamaged)
        {
            case "Gun": GameObject.FindGameObjectWithTag("Player").GetComponent<UltimateSystem>().AddUltimateCharge(10f); break;
            case "Melee": GameObject.FindGameObjectWithTag("Player").GetComponent<UltimateSystem>().AddUltimateCharge(30f); break;
        }

        yield return new WaitForSeconds(expirationTimer);
        if (health <= 0)
            Destroy(gameObject);
    }
}

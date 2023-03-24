using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public sealed class NPCVitality : VitalitySystem
{
    [SerializeField]
    private GameObject npc;

    private Animator animator; // used to animate HP & death

    [SerializeField]
    private float maxDamagedAnimDuration;
    private float damagedDur;

    [SerializeField]
    private float expirationTimer = 15;


    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();

        // Making HealthBar look at player by retrieving player reference from SceneReferenceCenter
        ConstraintSource cs = new ConstraintSource();
        cs.sourceTransform = transform.parent.GetComponent<SceneReferenceCenter>().Player.transform;
        cs.weight = 1.0f;
        transform.GetChild(0).GetComponent<LookAtConstraint>().AddSource(cs);
        transform.GetChild(0).GetComponent<LookAtConstraint>().constraintActive = true;
    }

    protected override void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        UpdateHealthUI();

        Death(health <= 0);

        if (animator.GetBool("IsDamaged"))
        {
            damagedDur -= Time.deltaTime;

            if (damagedDur <= 0)
            {
                animator.SetBool("IsDamaged", false);
            }
        }

        if (animator.GetBool("IsDead"))
        {
            expirationTimer -= Time.deltaTime;

            if (expirationTimer <= 0)
            {
                Destroy(npc);
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        animator.SetBool("IsDamaged", false);
        animator.SetBool("IsDamaged", true);
        damagedDur = maxDamagedAnimDuration;
    }
    public override void RestoreHealth(float healAmount)
    {
        base.RestoreHealth(healAmount);
    }

    public override void Death(bool isDead)
    {
        // For enemies with AI, it should be disabled

        animator.SetBool("IsDead", isDead);
    }
}

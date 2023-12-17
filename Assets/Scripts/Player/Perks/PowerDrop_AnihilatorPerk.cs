using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDrop_AnihilatorPerk : BasePerk
{
    [SerializeField]
    private bool canBeUsed = false;
    private bool dropping = false;

    [SerializeField]
    private float damageRadius;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private float effectsDuration;
    [SerializeField]
    private float ultimateChargePrice;

    [SerializeField]
    private GameObject effects;

    private PlayerMotor playerMotor;
    private PlayerUltimateMotor ultimatePlayerMotor;

    private void Start()
    {
        playerMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        ultimatePlayerMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUltimateMotor>();

        type = "PowerDrop_AnihilatorPerk";
    }
    private void Update()
    {
        if (active && !dropping)
        {
            if (canBeUsed)
            {
                if (playerMotor.IsKnockingDown || ultimatePlayerMotor.IsKnockingDown)
                {
                    dropping = true;
                }
            }
        }

        if (dropping)
        {
            if (playerMotor.GetComponent<CharacterController>().isGrounded)
            {
                dropping = false;

                if (playerMotor.GetComponent<UltimateSystem>().Energy >= ultimateChargePrice)
                {
                    playerMotor.GetComponent<UltimateSystem>().Energy -= ultimateChargePrice;

                    Collider[] struckTargets = Physics.OverlapSphere(playerMotor.transform.position, damageRadius);
                    foreach (Collider target in struckTargets)
                    {
                        if (target.gameObject.tag == "Enemy")
                        {
                            target.GetComponent<NPCVitality>().TakeDamage(damage);
                        }
                    }

                    Destroy(Instantiate(effects, playerMotor.transform.position, Quaternion.identity), effectsDuration);

                    StartCoroutine(SetOnCooldown());
                }
            }
        }
    }

    private IEnumerator SetOnCooldown()
    {
        canBeUsed = false;

        yield return new WaitForSeconds(cooldown);

        canBeUsed = true;
    }

    public override void EnableEffect()
    {
        if (!active)
        {
            active = true;

            canBeUsed = true;
        }
    }
    public override void DisableEffect()
    {
        active = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussRevolver : GunSystem
{
    [SerializeField]
    private float chargeHighBorder = 2.5f; // Time it takes to fully charge revolver's special fire
    [SerializeField]
    private float charge = 0f;
    [SerializeField]
    private int specialAmmoCost = 6;
    [SerializeField]
    private float highestSpecialDamage = 100f; // Highest posible damage with gauss alone (highestSpecialDamage * charge)
    [SerializeField]
    private float maxRecoveryTime = 0.25f; // Recovery after shot
    private float recoveryTime;

    [SerializeField]
    private Transform SpecialBarellEnd;

    private bool chargingUp = false;

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();

        if (chargingUp && recoveryTime <= 0f)
        {
            if (charge < chargeHighBorder)
            {
                charge += Time.deltaTime;
            }
        }

        if (recoveryTime > 0f)
            recoveryTime -= Time.deltaTime;
    }

    public override void Fire()
    {
        // Cannot be fired while reloading
        if (isReloading)
            return;
        else if (recoveryAfterShot > 0)
            return;
        else if (magazine <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        base.Fire();

        Ray shot = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hitObject;
        if (Physics.Raycast(shot, out hitObject, 500f, 1))
        {
            if (hitObject.collider.tag == "Enemy")
            {
                hitObject.collider.GetComponent<VitalitySystem>().TakeDamage(damage * (Random.Range(1, 101) >= critChance ? critMult : 1));
            }
        }
    }
    public override void SpecialFire()
    {
        // Cannot be fired while reloading
        if (isReloading)
            return;
        else if (recoveryAfterShot > 0)
            return;
        else if (recoveryTime > 0f)
            return;
        else if (magazine <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (chargingUp)
        {
            float chargeMultiplier = charge / chargeHighBorder;

            Ray shot = new Ray(cameraTransform.position, cameraTransform.forward);
            RaycastHit hitObject;
            if (Physics.Raycast(shot, out hitObject, 500f, 1))
            {
                if (hitObject.collider.tag == "Enemy")
                {
                    hitObject.collider.GetComponent<VitalitySystem>().TakeDamage(highestSpecialDamage * chargeMultiplier * Mathf.Clamp(magazine / specialAmmoCost, 0, 1));
                }
            }

            recoveryTime = maxRecoveryTime;
            magazine -= specialAmmoCost;
            charge = 0;
            chargingUp = false;
        }
        else
        {
            chargingUp = true;
        }
    }
}

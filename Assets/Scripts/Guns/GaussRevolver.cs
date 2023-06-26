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
    private Transform specialBarellEnd;

    [SerializeField]
    private ParticleSystem specialShootingSystem;
    [SerializeField]
    private LineRenderer specialShotEffect;

    [SerializeField]
    private float rayDuration = 0.75f;

    private bool chargingUp = false;

    protected override void Start()
    {
        base.Start();
        specialBarellEnd = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1);
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

    public void ShootEffects(RaycastHit hit)
    {
        TrailRenderer trail = Instantiate(bulletTrail, barell.position, Quaternion.identity);
        StartCoroutine(SpawnTrail(trail, hit));
    }
    public void SpecialShootEffects(RaycastHit hit)
    {
        LineRenderer line = Instantiate(specialShotEffect, specialBarellEnd.position, Quaternion.identity);

        line.SetPosition(0, specialBarellEnd.position);
        line.SetPosition(1, hit.point);

        Destroy(line, rayDuration);
    }

    public override void Fire()
    {
        // Cannot be fired while reloading
        if (isReloading)
            return;
        else if (recoveryAfterShot > 0)
            return;
        else if (chargingUp)
            return;
        else if (magazine <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        base.Fire();

        Vector3 direction = new Vector3(cameraTransform.forward.x + Random.Range(-spread, spread),
            cameraTransform.forward.y + Random.Range(-spread, spread),
            cameraTransform.forward.z + Random.Range(-spread, spread));
        Ray shot = new Ray(cameraTransform.position, direction);
        RaycastHit hitObject;
        if (Physics.Raycast(shot, out hitObject, 500f, 1))
        {
            ShootEffects(hitObject);

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
        else if (magazine < specialAmmoCost)
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
                specialShootingSystem.Play();
                SpecialShootEffects(hitObject);
                
                if (hitObject.collider.tag == "Enemy")
                {
                    hitObject.collider.GetComponent<VitalitySystem>().TakeDamage(highestSpecialDamage * chargeMultiplier);
                }
            }

            recoveryTime = maxRecoveryTime;
            magazine = Mathf.Clamp(magazine - specialAmmoCost, 0, maxMagazine);
            charge = 0;
            chargingUp = false;
        }
        else
        {
            chargingUp = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SMGranade : GunSystem
{
    private bool firing = false;

    [SerializeField]
    private GameObject specialProjectile;
    [SerializeField]
    private float specialProjectileDamage;
    [SerializeField]
    private float forceForSpecialProjectile;
    public void Shoot()
    {
        if (firing && magazine > 0)
        {
            animator.SetBool("IsFiring", true);

            Vector3 direction = new Vector3(cameraTransform.forward.x + Random.Range(-spread, spread),
                cameraTransform.forward.y + Random.Range(-spread, spread),
                cameraTransform.forward.z + Random.Range(-spread, spread));
            Ray shot = new Ray(cameraTransform.position, direction);
            RaycastHit hitObject;
            if (Physics.Raycast(shot, out hitObject, 500f, 1))
            {
                if (hitObject.collider.tag == "Enemy")
                {
                    hitObject.collider.GetComponent<VitalitySystem>().TakeDamage(damage * (Random.Range(1, 101) >= critChance ? critMult : 1));
                }
            }

            shotEffects.SetActive(true);
            recoveryAfterShot = TimeBetweenShots;
            shotEffectsDuration = maxShotEffectsDuration;

            magazine--;

            fireAnimationDur = timeBetweenShots;
        }
        else
        {
            firing = false;
            CancelInvoke();
        }
    }
    public override void Fire()
    {
        // Cannot be fired while reloading
        if (isReloading)
            return;
        else if (magazine <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        firing = !firing;
        if (firing)
        {
            InvokeRepeating("Shoot", 0, timeBetweenShots);
        }
        else
        {
            CancelInvoke("Shoot");
        }
    }

    public override void SpecialFire()
    {
        if (isReloading)
            return;
        else if (magazine <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        Transform playersCam = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);
        GameObject projectile = Instantiate(specialProjectile, playersCam.position + playersCam.TransformDirection(new Vector3(0f, 0f, 0.75f)), playersCam.rotation);

        projectile.GetComponent<SmgProjectile>().Fire(specialProjectileDamage * ((float)magazine / (float)maxMagazine), forceForSpecialProjectile);

        magazine = 0;
    }
}

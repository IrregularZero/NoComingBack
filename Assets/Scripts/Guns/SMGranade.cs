using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGranade : GunSystem
{
    [SerializeField]
    private bool firing = false;

    private GameObject specialProjectile;
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
        throw new System.NotImplementedException();
    }
}

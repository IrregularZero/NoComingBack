using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Musket : GunSystem
{
    [SerializeField]
    private int maxFireChain = 3;
    private int fireChain = 3;
    [SerializeField]
    private float maxTimeBetweenShotsInChain = 0.1f;
    [SerializeField]
    private float specialSpread = 1.5f;
    [SerializeField]
    private int specialAmmocost = 6;
    [SerializeField]
    private float specialEffectiveRange = 10f;

    protected override void Start()
    {
        base.Start(); 
        barell = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0);
    }

    private void Shoot()
    {
        if (fireChain >= 0 && magazine > 0)
        {
            shootingSystem.Play();
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
            magazine--;
            fireChain--;
        }
        else
        {
            fireChain = maxFireChain;
            CancelInvoke();
        }
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
        magazine++;

        InvokeRepeating("Shoot", 0, maxTimeBetweenShotsInChain);
    }
    public override void SpecialFire()
    {
        // Shotgun mode
        if (isReloading)
            return;
        else if (recoveryAfterShot > 0)
            return;
        else if (magazine < specialAmmocost)
            return;
        else if (magazine <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // The bigger special cost the more bullets will go
        for (int i = 0; i < specialAmmocost; i++)
        {
            shootingSystem.Play();
            Vector3 direction = new Vector3(cameraTransform.forward.x + Random.Range(-specialSpread, specialSpread),
                cameraTransform.forward.y + Random.Range(-specialSpread, specialSpread),
                cameraTransform.forward.z + Random.Range(-specialSpread, specialSpread));
            Ray shot = new Ray(cameraTransform.position, direction);
            RaycastHit hitObject;
            Debug.DrawRay(cameraTransform.position, direction, Color.red, 5f);
            if (Physics.Raycast(shot, out hitObject, 500f, 1))
            {
                ShootEffects(hitObject);

                if (hitObject.collider.tag == "Enemy")
                {
                    hitObject.collider.GetComponent<VitalitySystem>().TakeDamage(damage * 
                        (Random.Range(1, 101) >= critChance ? critMult : 1) * 
                        (specialEffectiveRange / Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, hitObject.transform.position)));
                }
            }
        }

        magazine -= specialAmmocost;
    }
}

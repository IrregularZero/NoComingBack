using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musket : GunSystem
{
    [SerializeField]
    private int fireChain = 3;
    [SerializeField]
    private float maxTimeBetweenShotsInChain = 0.1f;
    [SerializeField]
    private float timeBetweenShotsInChain = 0.1f;
    [SerializeField]
    private float spread = 1.5f;
    [SerializeField]
    private int specialAmmocost = 6;
    [SerializeField]
    private float specialEffectiveRange = 10f;

    private IEnumerable shootChain()
    {
        for (int i = 0; i < fireChain; i++)
        {
            Ray shot = new Ray(cameraTransform.position, cameraTransform.forward);
            RaycastHit hitObject;
            if (Physics.Raycast(shot, out hitObject, 500f, 1))
            {
                if (hitObject.collider.tag == "Enemy")
                {
                    hitObject.collider.GetComponent<VitalitySystem>().TakeDamage(damage * (Random.Range(1, 101) >= critChance ? critMult : 1));
                }
            }
            yield return new WaitForSeconds(maxTimeBetweenShotsInChain);
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

        for (int i = 0; i < fireChain; i++)
        {
            Ray shot = new Ray(cameraTransform.position, cameraTransform.forward);
            RaycastHit hitObject;
            if (Physics.Raycast(shot, out hitObject, 500f, 1))
            {
                if (hitObject.collider.tag == "Enemy")
                {
                    hitObject.collider.GetComponent<VitalitySystem>().TakeDamage(damage * (Random.Range(1, 101) >= critChance ? critMult : 1));
                }
            }

            magazine--;

            timeBetweenShotsInChain = maxTimeBetweenShotsInChain;
            while (timeBetweenShotsInChain > 0)
            {
                timeBetweenShotsInChain -= Time.deltaTime;
            }
        }
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
            Vector3 direction = new Vector3(cameraTransform.forward.x + Random.Range(-spread, spread), cameraTransform.forward.y + Random.Range(-spread, spread), cameraTransform.forward.z + Random.Range(-spread, spread));
            Ray shot = new Ray(cameraTransform.position, direction);
            RaycastHit hitObject;
            Debug.DrawRay(cameraTransform.position, direction, Color.red, 5f);
            if (Physics.Raycast(shot, out hitObject, 500f, 1))
            {
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

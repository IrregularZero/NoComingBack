using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private bool shotSpecial = false;
    private bool reloadSocketTaken = false;
    [SerializeField]
    private float ammoPerSecond = 1f;
    private float recoveringMagazine = 0f;

    [SerializeField]
    protected TextMeshProUGUI recoveringMagazineMeter;

    protected override void Start()
    {
        base.Start(); 
        barell = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0);

    }
    protected override void Update()
    {
        base.Update();

        if (reloadSocketTaken)
        {
            if (recoveringMagazine < maxMagazine)
            {
                recoveringMagazine += ammoPerSecond * (Time.deltaTime / 1);

                UpdateInterface();
            }
        }
    }

    public void Shoot()
    {
        if (firing && magazine > 0)
        {
            animator.SetBool("IsFiring", false);
            animator.SetBool("IsFiring", true);

            shootingSystem.Play();

            Vector3 direction = new Vector3(cameraTransform.forward.x + Random.Range(-spread, spread),
                cameraTransform.forward.y + Random.Range(-spread, spread),
                cameraTransform.forward.z + Random.Range(-spread, spread));
            Ray shot = new Ray(cameraTransform.position, direction);
            RaycastHit hitObject;
            if (Physics.Raycast(shot, out hitObject, 500f, 1))
            {
                ShootEffects(hitObject);

                if (hitObject.collider.GetComponent<VitalitySystem>() != null && hitObject.collider.tag != "Player")
                {
                    hitObject.collider.GetComponent<NPCVitality>().TakeDamage(damage * (Random.Range(1, 100) >= critChance ? critMult : 1), "Gun");
                }
            }

            shootingSystem.Play();
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
            animator.SetBool("IsFiring", false);
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

        animator.SetBool("IsSpecialFiring", true);

        Transform playersCam = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);
        GameObject projectile = Instantiate(specialProjectile, playersCam.position + playersCam.TransformDirection(new Vector3(0f, 0f, 0.75f)), playersCam.rotation);

        projectile.GetComponent<SmgProjectile>().Fire(specialProjectileDamage * ((float)magazine / (float)maxMagazine), forceForSpecialProjectile);

        shotSpecial = true;
        magazine = 0;
        
        animator.SetBool("IsSpecialFiring", false);
        animator.SetBool("SpecialShot", true);
    }

    public override IEnumerator Reload()
    {
        firing = false;
        CancelInvoke("Shoot");

        if (magazine >= maxMagazine && ammoInStorage > 0)
            return base.Reload();
        if (ammoInStorage <= 0)
            return base.Reload();
        if (shotSpecial)
        {
            shotSpecial = false;
            reloadSocketTaken = false;
            recoveringMagazine = 0;

            animator.SetBool("SpecialShot", false);
            animator.SetBool("SlotTaken", false); 
            
            if (reloadSocketTaken && recoveringMagazine >= maxMagazine)
            {
                ammoInStorage += (int)Mathf.Floor(recoveringMagazine);
            }

            return base.Reload();
        }
        else if (reloadSocketTaken && recoveringMagazine >= maxMagazine)
        {
            ammoInStorage += (int)Mathf.Floor(recoveringMagazine);
        }

        ammoInStorage -= magazine;
        recoveringMagazine = magazine;
        reloadSocketTaken = true;

        animator.SetBool("SlotTaken", true);

        return base.Reload();
    }

    public override void UpdateInterface()
    {
        base.UpdateInterface();

        recoveringMagazineMeter.text = $"{Mathf.Floor(recoveringMagazine)}";
    }
}

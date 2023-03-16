using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class TestGun : GunSystem
{
    [SerializeField]
    private float maxDelayBetweenShots = 0.1f;
    private float delayBetweenShots;
    private bool activatedSpecial;
    [SerializeField]
    private int maxSpecialShotsLeft = 2;
    private int specialShotsLeft;

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();

        delayBetweenShots -= Time.deltaTime;

        if (activatedSpecial && delayBetweenShots <= 0)
        {
            Fire();

            delayBetweenShots = maxDelayBetweenShots;

            if (--specialShotsLeft <= 0)
                activatedSpecial = false;
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

        Ray shot = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hitObject;
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward, Color.red, 500f);
        if (Physics.Raycast(shot, out hitObject, 500f, 1))
        {
            if (hitObject.collider.tag == "Enemy")
            {
                hitObject.collider.GetComponent<VitalitySystem>().TakeDamage(damage * Random.Range(1, 101) >= critChance ? critMult : 1);
            }
        }
    }

    public override void SpecialFire()
    {
        // Cannot be fired while reloading
        if (isReloading)
            return;

        if (!activatedSpecial)
        {
            activatedSpecial = true;

            specialShotsLeft = maxSpecialShotsLeft;
        }
    }
}

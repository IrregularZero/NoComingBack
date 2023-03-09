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
        Debug.Log("Fired");
        base.Fire();

        Ray shot = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hitObject;
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward, Color.red, 500f);
        if (Physics.Raycast(shot, out hitObject, 500f, 1))
        {
            if (hitObject.collider.tag == "Enemy")
            {
                hitObject.collider.GetComponent<VitalitySystem>().TakeDamage(damage);
                Debug.Log("Enemy got hit!");
            }
            Debug.Log("Enviroment got hit....");
        }
    }

    public override void SpecialFire()
    {
        base.SpecialFire();

        if (!activatedSpecial)
        {
            activatedSpecial = true;

            specialShotsLeft = maxSpecialShotsLeft;
        }
    }
}

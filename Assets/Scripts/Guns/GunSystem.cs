using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class GunSystem : MonoBehaviour
{
    [SerializeField]
    protected float damage = -1f;
    [SerializeField]
    protected float timeBetweenShots = -1f;
    protected float recoveryAfterShot;
    [SerializeField]
    protected float accuracy = -1f;
    [SerializeField]
    protected int maxMagazine = -1;
    [SerializeField]
    protected int magazine = -1;
    [SerializeField]
    protected int ammoInStorage = -1;
    [SerializeField]
    protected float reloadSpeed = -1f;
    protected bool isReloading;
    [SerializeField]
    protected int critChance;
    [SerializeField]
    protected float critMult;

    [SerializeField]
    protected Vector3 barell;
    [SerializeField]
    protected Transform cameraTransform;

    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected GameObject shotEffects;
    protected float maxShotEffectsDuration = 0.1f;
    protected float shotEffectsDuration;

    public GunSystem()
    {
        damage = -1;
        timeBetweenShots = -1;
        accuracy = -1;
        maxMagazine = -1;
        magazine = maxMagazine;
        ammoInStorage = -1;
        reloadSpeed = -1;
        critChance = -1;
        critMult = -1;

        barell = Vector3.zero;
    }
    public GunSystem(float damage, float timeBetweenShots, float accuracy, int maxMagazine, int magazine, int ammoInStorage, float reloadSpeed, int critChance, float critMult, Vector3 barell, Transform cameraTransform)
    {
        this.damage = damage;
        this.timeBetweenShots = timeBetweenShots;
        this.accuracy = accuracy;
        this.maxMagazine = maxMagazine;
        this.magazine = magazine;
        this.ammoInStorage = ammoInStorage;
        this.reloadSpeed = reloadSpeed;
        this.critChance = critChance;
        this.critMult = critMult;

        this.barell = barell;
        this.cameraTransform = cameraTransform;
    }

    #region Properties
    public float Damage 
    {
        get
        {
            return damage;
        }
        set
        {
            if (value >= 0)
            {
                damage = value;
            }
        }
    }
    public float TimeBetweenShots 
    {
        get
        {
            return timeBetweenShots;
        } 
        set
        {
            if (value >= 0)
            {
                timeBetweenShots = value;
            }
        }
    }
    public float Accuracy 
    {
        get
        {
            return accuracy;
        }
        set
        {
            if (value >= 0 && value <= 1)
            {
                accuracy = value;
            }
        }
    }
    public int MaxMagazine 
    {
        get
        {
            return maxMagazine;
        }
        set
        {
            if (value > 0)
            {
                maxMagazine = value;
            }
        }
    }
    public float ReloadSpeed 
    {
        get
        {
            return reloadSpeed;
        }
        set
        {
            if (value >= 0)
            {
                reloadSpeed = value;
            }
        }
    }
    public int CritChance 
    {
        get
        {
            return critChance;
        }
        set
        {
            if (value >= 0 && value <= 100)
            {
                critChance = value;
            }
        }
    }
    public float CritMultiplier 
    {
        get
        {
            return critMult;
        }
        set
        {
            if (value >= 0)
            {
                critMult = value;
            }
        }
    }
    #endregion

    #region Methods
    protected virtual void Update()
    {
        recoveryAfterShot -= Time.deltaTime;

        if (shotEffects.activeSelf)
        {
            shotEffectsDuration -= Time.deltaTime;
            if (shotEffectsDuration <= 0)
                shotEffects.SetActive(false);
        }
    }

    public virtual void Fire()
    {
        // Cannot be fired while reloading
        if (isReloading)
            return;
        if (recoveryAfterShot > 0)
            return;

        shotEffects.SetActive(true);
        recoveryAfterShot = TimeBetweenShots;
        shotEffectsDuration = maxShotEffectsDuration;
    }
    public virtual void SpecialFire()
    {
        // Cannot be fired while reloading
        if (isReloading)
            return;
    }
    public virtual IEnumerator Reload()
    {
        if (ammoInStorage > 0)
        {
            // play anim

            isReloading = true;
            yield return new WaitForSeconds(reloadSpeed);

            magazine = Mathf.Clamp(ammoInStorage - maxMagazine, 0, maxMagazine);
            isReloading = false;
        }
    }
    public virtual void Overview()
    {
        // stop previous anim
        // play anim
    }
    #endregion
}

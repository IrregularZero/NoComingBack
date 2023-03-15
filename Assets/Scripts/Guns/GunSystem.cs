using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    protected const int maxAmmoInStorage = 1000;
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

    [SerializeField]
    protected TextMeshProUGUI ammoMeter;

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

        UpdateInterface();
    }

    public virtual void Fire()
    {

        shotEffects.SetActive(true);
        recoveryAfterShot = TimeBetweenShots;
        shotEffectsDuration = maxShotEffectsDuration;

        magazine--;
    }
    public abstract void SpecialFire();
    public virtual IEnumerator Reload()
    {
        if (ammoInStorage > 0)
        {
            // play anim

            isReloading = true;
            yield return new WaitForSeconds(reloadSpeed);

            int magazineLeftovers = maxMagazine - magazine;

            magazine = Mathf.Clamp(maxMagazine - Mathf.Clamp(ammoInStorage - magazineLeftovers, maxMagazine * -1, 0), 0, maxMagazine);
            ammoInStorage = Mathf.Clamp(ammoInStorage - magazineLeftovers, 0, maxAmmoInStorage);

            isReloading = false;
        }
    }
    public virtual void Overview()
    {
        // stop previous anim
        // play anim
    }

    public virtual void UpdateInterface()
    {
        ammoMeter.text = $"{magazine} | {ammoInStorage}";
    }
    #endregion
}

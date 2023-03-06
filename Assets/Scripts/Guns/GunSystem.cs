using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunSystem : MonoBehaviour
{
    protected float damage;
    protected float fireRate;
    protected float accuracy;
    protected int maxMagazine;
    protected int magazine;
    protected int ammoInStorage;
    protected float reloadSpeed;
    protected int critChance;
    protected float critMult;

    protected Vector3 barell;
    protected Transform cameraTransform;

    protected Animator animator;

    public GunSystem()
    {
        damage = -1;
        fireRate = -1;
        accuracy = -1;
        maxMagazine = -1;
        magazine = maxMagazine;
        ammoInStorage = -1;
        reloadSpeed = -1;
        critChance = -1;
        critMult = -1;

        barell = Vector3.zero;
    }
    public GunSystem(float damage, float fireRate, float accuracy, int maxMagazine, int magazine, int ammoInStorage, float reloadSpeed, int critChance, float critMult, Vector3 barell, Transform cameraTransform)
    {
        this.damage = damage;
        this.fireRate = fireRate;
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
    public float FireRate 
    {
        get
        {
            return fireRate;
        } 
        set
        {
            if (value >= 0)
            {
                fireRate = value;
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
    public abstract void Fire();
    public abstract void SpecialFire();
    public IEnumerator Reload()
    {
        if (ammoInStorage > 0)
        {
            // play anim

            yield return new WaitForSeconds(reloadSpeed);

            magazine = Mathf.Clamp(ammoInStorage - maxMagazine, 0, maxMagazine);
        }
    }
    public void Overview()
    {
        // stop previous anim
        // play anim
    }
    #endregion
}

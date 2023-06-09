using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class GunSystem : MonoBehaviour
{
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float timeBetweenShots;
    protected float recoveryAfterShot;
    [SerializeField]
    protected float spread;
    [SerializeField]
    protected int maxMagazine;
    [SerializeField]
    protected int magazine;
    [SerializeField]
    protected const int maxAmmoInStorage = 500;
    [SerializeField]
    protected int ammoInStorage;
    [SerializeField]
    protected float reloadSpeed;
    protected bool isReloading;
    [SerializeField]
    protected int critChance;
    [SerializeField]
    protected float critMult;
    [SerializeField]
    protected float overviewDuration;
    protected float fireAnimationDur;

    [SerializeField]
    protected Transform barell;
    protected Transform cameraTransform;

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
        spread = -1;
        maxMagazine = -1;
        magazine = maxMagazine;
        ammoInStorage = -1;
        reloadSpeed = -1;
        critChance = -1;
        critMult = -1;
    }
    public GunSystem(float damage, float timeBetweenShots, float spread, int maxMagazine, int magazine, int ammoInStorage, float reloadSpeed, int critChance, float critMult, Transform barell, Transform cameraTransform)
    {
        this.damage = damage;
        this.timeBetweenShots = timeBetweenShots;
        this.spread = spread;
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
    public float Spread 
    {
        get
        {
            return spread;
        }
        set
        {
            if (value >= 0 && value <= 1)
            {
                spread = value;
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
    public int AmmoInStorage 
    {
        get
        {
            return ammoInStorage;
        }
        set
        {
            if (value > 0 && value < maxAmmoInStorage)
            {
                ammoInStorage = value;
            }
            else if (value >= maxAmmoInStorage)
            {
                ammoInStorage = maxAmmoInStorage;
            }
        }
    }
    public int MaxAmmoInStorage 
    {
        get
        {
            return maxAmmoInStorage;
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
    public float OverviewDuration 
    {
        get
        {
            return overviewDuration;
        }
        set
        {
            if (value > 0)
            {
                overviewDuration = value;
            }
        }
    }
    #endregion

    #region Methods
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        cameraTransform = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);
    }
    protected virtual void Update()
    {
        recoveryAfterShot -= Time.deltaTime;

        if (shotEffects.activeSelf)
        {
            shotEffectsDuration -= Time.deltaTime;
            if (shotEffectsDuration <= 0)
                shotEffects.SetActive(false);
        }

        if (fireAnimationDur > 0)
        {
            fireAnimationDur -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("IsFiring", false);
        }

        UpdateInterface();
    }

    public virtual void Fire()
    {
        animator.SetBool("IsFiring", true);

        shotEffects.SetActive(true);
        recoveryAfterShot = TimeBetweenShots;
        shotEffectsDuration = maxShotEffectsDuration;

        magazine--;

        fireAnimationDur = timeBetweenShots;
    }
    public abstract void SpecialFire();
    public virtual IEnumerator Reload()
    {
        if (ammoInStorage > 0 && magazine < maxMagazine)
        {
            animator.SetBool("IsReloading", true);

            isReloading = true;
            yield return new WaitForSeconds(reloadSpeed);

            ammoInStorage += magazine;
            magazine = Mathf.Clamp(ammoInStorage, 0, maxMagazine);
            ammoInStorage = Mathf.Clamp(ammoInStorage - magazine, 0, maxAmmoInStorage);

            isReloading = false;
            animator.SetBool("IsReloading", false);
        }
    }
    public virtual IEnumerator Overview()
    {
        animator.SetBool("IsOverviewing", true);
        yield return new WaitForSeconds(overviewDuration);
        animator.SetBool("IsOverviewing", false);
    }
    public virtual void UpdateInterface()
    {
        ammoMeter.text = $"{magazine} | {ammoInStorage}";
    }
    #endregion
}

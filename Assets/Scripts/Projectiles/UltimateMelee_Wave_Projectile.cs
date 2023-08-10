using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class UltimateMelee_Wave_Projectile : MonoBehaviour
{
    private float damage;
    private float speed;
    [SerializeField]
    private float lifeTime;

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
                damage = value;
        }
    }
    public float Speed 
    {
        get
        {
            return speed;
        }
        set
        {
            if (value >= 0)
                speed = value;
        }
    }
    public float LifeTime 
    {
        get
        {
            return lifeTime;
        }
        set
        {
            if (value < 0) 
                lifeTime = value;
        }
    }
    #endregion

    private void Update()
    {
        transform.TransformDirection(new Vector3(0f, 0f, speed * Time.deltaTime));
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
    }

    public void Fire(float damage)
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).position;
        transform.rotation = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).rotation;

        this.damage = damage;
        StartCoroutine(DestructionTimer());
    }
    private IEnumerator DestructionTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}

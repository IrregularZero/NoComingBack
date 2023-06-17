using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmgProjectile : MonoBehaviour
{
    [SerializeField]
    private float explosionRadius = 5f;
    [SerializeField]
    private float explosionDamage = 0f;

    #region Properties
    public float ExplosionRadius 
    {
        get
        {
            return explosionRadius;
        }
        set
        {
            if (value > 0)
            {
                explosionRadius = value;
            }
        }
    }
    public float ExplosionDamage 
    {
        get
        {
            return explosionDamage;
        }
        set
        {
            if (value >= 0)
            {
                explosionDamage = value;
            }
        }
    }
    #endregion

    public void fire(float force)
    {
        Transform direction = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);

        GetComponent<Rigidbody>().AddForce(new Vector3(direction.forward.x, direction.forward.x, direction.forward.z + force), ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player")
        {
            Explosion(collision.collider.transform.position);
        }
    }

    private void Explosion(Vector3 ExplosionPosition)
    {
        Collider[] hitColliders = Physics.OverlapSphere(ExplosionPosition, ExplosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.name);
            if (hitCollider.gameObject.GetComponent<VitalitySystem>() != null)
            {
                hitCollider.gameObject.GetComponent<VitalitySystem>().TakeDamage(explosionDamage);
            }
        }
    }
}

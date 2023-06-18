using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmgProjectile : MonoBehaviour
{
    [SerializeField]
    private float explosionRadius = 5f;
    private float explosionDamage;

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

    public void Fire(float damage, float force)
    {
        Transform direction = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);

        explosionDamage = damage;
        GetComponent<Rigidbody>().AddForce(new Vector3(direction.forward.x, direction.forward.y, direction.forward.z) * force, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player")
        {
            Explosion(collision.contacts[0].point);
        }
    }

    private void Explosion(Vector3 ExplosionPosition)
    {
        // Line below contains creation of variables hitColliders with data from command Physics.OverlapSphere(pos, rad), which scans spheral area around object
        Collider[] hitColliders = Physics.OverlapSphere(ExplosionPosition, ExplosionRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.name);
            if (hitCollider.gameObject.GetComponent<VitalitySystem>() != null || hitCollider.gameObject.GetComponent<NPCVitality>() != null)
            {
                hitCollider.gameObject.GetComponent<VitalitySystem>().TakeDamage(explosionDamage);
            }
        }
        Destroy(gameObject);
    }
}

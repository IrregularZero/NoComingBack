using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesProjectile : MonoBehaviour
{
    private NPCVitality vitality;

    private CharacterController characterController;
    private bool destroying = false;
    [SerializeField]
    private float liveTimer = 10f;
    [SerializeField]
    private float destroyTimer = 0.5f;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;

    protected virtual void Start()
    {
        vitality = GetComponent<NPCVitality>();
        characterController = GetComponent<CharacterController>();
        StartCoroutine(LiveCycle());
    }
    protected virtual void Update()
    {
        if (!destroying)
            characterController.Move(transform.forward * speed * Time.deltaTime);

        if (vitality.Health <= 0)
            StartCoroutine(DestructionSequence());
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (destroying)
            return;
        if (other.name == name)
            return;
        if (other.tag != "Player")
            return;

        if (other.gameObject.GetComponent<VitalitySystem>() != null)
        {
            other.gameObject.GetComponent<VitalitySystem>().TakeDamage(damage);
        }

        StartCoroutine(DestructionSequence());
    }

    protected virtual IEnumerator LiveCycle()
    {
        yield return new WaitForSeconds(liveTimer);
        StartCoroutine(DestructionSequence());
    }

    public virtual IEnumerator DestructionSequence()
    {
        destroying = true;
        characterController.enabled = false;
        yield return new WaitForSeconds(destroyTimer);
        Destroy(gameObject);
    }
    public virtual void SetupProjectile(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;

    }
    public virtual void SetupProjectile(float speed, float damage, Color projectileColor)
    {
        this.speed = speed;
        this.damage = damage;
        transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = projectileColor;
    }
}

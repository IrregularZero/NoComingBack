using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesProjectile : MonoBehaviour
{
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

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        StartCoroutine(LiveCycle());
    }
    private void Update()
    {
        if (!destroying)
            characterController.Move(transform.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
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

    private IEnumerator LiveCycle()
    {
        yield return new WaitForSeconds(liveTimer);
        StartCoroutine(DestructionSequence());
    }

    public IEnumerator DestructionSequence()
    {
        destroying = true;
        characterController.enabled = false;
        yield return new WaitForSeconds(destroyTimer);
        Destroy(gameObject);
    }
    public void SetupProjectile(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
    }
}

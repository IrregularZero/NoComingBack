using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesProjectile : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField]
    private float destroyTimer = 0.5f;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        characterController.Move(transform.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<VitalitySystem>() != null)
        {
            other.gameObject.GetComponent<VitalitySystem>().TakeDamage(damage);
        }

        StartCoroutine(DestructionSequence());
    }
    private IEnumerator DestructionSequence()
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(gameObject);
    }
}

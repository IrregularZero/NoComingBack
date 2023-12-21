using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveModePerk_ActiveComponent : MonoBehaviour
{
    private float speed;
    private float damage;
    private float lifeTimer;
    
    private CharacterController characterController;
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
        if (other.tag == "Enemy" || other.tag == "EnemyProjectiles")
        {
            other.GetComponent<NPCVitality>().TakeDamage(damage);
        }
    }

    public void Setup(float damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;
    }
}

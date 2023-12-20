using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSystemChargeDamageModule : MonoBehaviour
{
    [SerializeField]
    private float hitCost;
    [SerializeField]
    private float damage;

    private UltimateSystem playerUltimateSystem;

    private void Start()
    {
        playerUltimateSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<UltimateSystem>();


        gameObject.GetComponent<MeshCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    private void Update()
    {
        if (playerUltimateSystem.Energy < hitCost)
        {
            gameObject.GetComponent<MeshCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<MeshCollider>().enabled = true;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<NPCVitality>().TakeDamage(damage, "Perk");
            playerUltimateSystem.Energy -= hitCost;
        }
    }
}

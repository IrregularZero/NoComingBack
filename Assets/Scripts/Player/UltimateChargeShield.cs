using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateChargeShield : MonoBehaviour
{
    [SerializeField]
    private float hitCost;

    private UltimateSystem playerUltimateSystem;

    private void Start()
    {
        playerUltimateSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<UltimateSystem>();


        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    private void Update()
    {
        if (playerUltimateSystem.Energy < hitCost)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyProjectiles")
        {
            Destroy(other.gameObject);
            playerUltimateSystem.Energy -= hitCost;
        }
    }


}

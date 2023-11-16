using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPart_Behaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject endEffects;

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<NPCVitality>().Health = 0;

        GameObject ef = Instantiate(endEffects, transform.position, transform.rotation) as GameObject;
        Destroy(ef, 4);
    }
}

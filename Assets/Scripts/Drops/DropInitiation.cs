using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInitiation : MonoBehaviour
{
    bool colorSet = false;

    private void LateUpdate()
    {
        if(!colorSet)
        {
            // Setting Color
            Color itemsColor = transform.GetChild(0).GetComponent<Item>().BackgroundColor;
            itemsColor = new Color(itemsColor.r, itemsColor.g, itemsColor.b, 0.3f);

            transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = itemsColor;
            transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().material.color = itemsColor;
            ParticleSystem.MainModule main = transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().main;
            main.startColor = itemsColor;

            colorSet = true;
        }
    }
}

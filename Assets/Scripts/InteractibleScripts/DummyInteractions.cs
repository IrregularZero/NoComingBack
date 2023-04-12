using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyInteractions : Interactible
{
    protected override void Interact()
    {
        if (GetComponent<MeshRenderer>().material.color == Color.red)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (GetComponent<MeshRenderer>().material.color == Color.green)
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject embededMelee;

    public void useEmbededMelee()
    {
        embededMelee.GetComponent<EmbededMelee>().Use();
    }
}

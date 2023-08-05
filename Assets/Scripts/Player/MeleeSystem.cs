using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject embededMelee;

    #region Properties
    public GameObject EmbededMelee 
    {
        get
        {
            return embededMelee;
        } 
    }
    #endregion

    public void useEmbededMelee()
    {
        embededMelee.GetComponent<EmbededMelee>().Use();
    }
}

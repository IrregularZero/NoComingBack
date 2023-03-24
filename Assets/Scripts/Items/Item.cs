using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField]
    protected string title;
    [SerializeField]
    protected string type;
    [SerializeField]
    protected string description;

    #region Properties
    public string Title 
    {
        get
        {
            return title;
        }
        set
        {
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
            {
                title = value;
            }
        }
    }
    public string Type 
    {
        get
        {
            return type;
        }
        set
        {
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
            {
                type = value;
            }
        }
    }
    public string Desription 
    {
        get
        {
            return description;
        }
        set
        {

            if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
            {
                description = value;
            }
        }
    }
    #endregion

    public abstract void Use();
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public abstract class BasePerk : MonoBehaviour
{
    [SerializeField]
    protected Sprite logo;
    [SerializeField]
    protected string title;
    [SerializeField]
    protected string description;

    protected bool active;

    #region Propterties
    public Sprite Logo 
    {
        get
        {
            return logo;
        }
        set
        {
            logo = value;
        }
    }
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
            else
            {
                Debug.Log("Title: given value is null, empty, or white space");
            }
        }
    }
    public string Description 
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
            else
            {
                Debug.Log("Descrtiption: given value is null, empty, or white space");
            }
        }
    }
    public bool Active
    {
        get
        {
            return active;
        }
    }
    #endregion

    public abstract void EnableEffect();
    public abstract void DisableEffect();
}

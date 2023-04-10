using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField]
    protected Sprite itemIcon;
    protected Color backgroundColor;

    [SerializeField]
    protected string title;
    protected string type;
    [SerializeField]
    protected string description;

    #region Properties
    public Sprite ItemIcon 
    { 
        get
        {
            return itemIcon;
        }
        set
        {
            itemIcon = value;
        }
    }
    public Color BackgroundColor 
    {
        get
        {
            return backgroundColor;
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
    public Item()
    {
        title = string.Empty;
        type = string.Empty;
        description = string.Empty;
    }
    public Item(string title, string type, string description)
    {
        this.title = title;
        this.type = type;
        this.description = description;
    }

    public abstract void Use();
}

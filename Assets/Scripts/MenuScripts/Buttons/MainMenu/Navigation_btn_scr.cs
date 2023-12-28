using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation_btn_scr : MainMenu_btn_scr
{
    [SerializeField]
    private GameObject screen;

    public override void ButtonPress()
    {
        screen.transform.GetChild(0).gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_btn_scr : MainMenu_btn_scr
{
    public override void ButtonPress()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().Exit();
    }
}

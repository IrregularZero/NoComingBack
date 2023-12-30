using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume_btn_src : MainMenu_btn_scr
{
    public override void ButtonPress()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().SwitchPauseState();
    }
}

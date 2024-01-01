using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross_btn_scr : MainMenu_btn_scr
{
    // Function: Turn off parent
    // This script should be universal for all crosses
    public override void ButtonPress()
    {
        transform.parent.gameObject.SetActive(false);
    }
}

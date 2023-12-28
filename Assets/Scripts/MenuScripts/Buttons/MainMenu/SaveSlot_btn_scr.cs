using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlot_btn_scr : MainMenu_btn_scr
{
    [SerializeField]
    private int saveSlotIndex; // 1 - 4

    public override void ButtonPress()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().StartGame(saveSlotIndex);
    }
}

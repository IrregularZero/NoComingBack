using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlot_btn_scr : MainMenu_btn_scr
{
    private bool starting = false;
    [SerializeField]
    private int saveSlotIndex; // 1 - 4

    [SerializeField]
    private string message;
    [SerializeField]
    private string positive;
    [SerializeField]
    private string negative;

    private void Update()
    {
        if (answerChanged)
        {
            answerChanged = false;
            starting = answerFromConfBlock;
        }
        if (starting)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().StartGame(saveSlotIndex);
        }
    }

    public override void ButtonPress()
    {
        GameObject.FindGameObjectWithTag("ConfirmationBlock").GetComponent<ConfirmationBlock>().SetupAndShow(message, positive, negative, gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToMainMenu_btn_src : MainMenu_btn_scr
{
    private bool exiting = false;

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
            exiting = answerFromConfBlock;
        }
        if (exiting)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().BackToMainMenu();
        }
    }

    public override void ButtonPress()
    {
        GameObject.FindGameObjectWithTag("ConfirmationBlock").GetComponent<ConfirmationBlock>().SetupAndShow(message, positive, negative, gameObject);
    }
}

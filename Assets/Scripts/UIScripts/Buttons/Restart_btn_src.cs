using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart_btn_src : MainMenu_btn_scr
{
    private bool restarting = false;

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
            restarting = answerFromConfBlock;
        }

        if (restarting)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().Restart();
        }
    }

    public override void ButtonPress()
    {
        GameObject.FindGameObjectWithTag("ConfirmationBlock").GetComponent<ConfirmationBlock>().SetupAndShow(message, positive, negative, gameObject);
    }
}

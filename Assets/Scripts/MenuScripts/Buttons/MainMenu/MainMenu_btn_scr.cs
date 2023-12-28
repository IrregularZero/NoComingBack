using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_btn_scr : MonoBehaviour
{
    protected bool answerChanged = false;
    protected bool answerFromConfBlock = false;

    public virtual void ButtonPress()
    {
        throw new System.NotImplementedException();
    }
    public virtual void recieveAnswerFromConfBlock(bool answer)
    {
        answerFromConfBlock = answer;
        answerChanged = true;
    }
}

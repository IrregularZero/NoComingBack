using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConfirmationBlock : MonoBehaviour
{
    private GameObject recipient;

    [SerializeField]
    private TextMeshProUGUI message_txt;
    [SerializeField]
    private TextMeshProUGUI positive_btn_txt;
    [SerializeField]
    private TextMeshProUGUI negative_btn_txt;

    [SerializeField]
    private float outroLength;
    private Animator animator;

    private void Start()
    {
        animator = transform.GetChild(1).GetComponent<Animator>();
    }

    public void SetupAndShow(string message_txt, string positive_btn_txt, string negative_btn_txt, GameObject recipient)
    {
        this.recipient = recipient;

        this.message_txt.text = message_txt;
        this.positive_btn_txt.text = positive_btn_txt;
        this.negative_btn_txt.text = negative_btn_txt;
        
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public void ReturnResut()
    {
        // Logic is simple, buttons disable each other, if in the end positive is enabled then player chose it
        recipient.GetComponent<MainMenu_btn_scr>().recieveAnswerFromConfBlock(positive_btn_txt.transform.parent.gameObject.activeSelf);
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        animator.SetBool("Disabled", true);

        yield return new WaitForSeconds(outroLength);

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        animator.SetBool("Disabled", false);
    }

}

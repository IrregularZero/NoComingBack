using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkingTitle : MonoBehaviour
{
    [SerializeField]
    private int amountOfMorces;
    [SerializeField]
    private string morse0;
    [SerializeField]
    private string morse1;
    [SerializeField]
    private string morse2;
    [SerializeField]
    private string morse3;
    [SerializeField]
    private string morse4;
    [SerializeField]
    private string morse5;
    [SerializeField]
    private float dotRate;
    [SerializeField]
    private float underscoreRate;

    [SerializeField]
    private Color dotColor;
    [SerializeField]
    private Color underscoreColor;
    [SerializeField]
    private Color colorInBetween;

    private TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        // Choosing random morse code
        switch (UnityEngine.Random.Range(0, amountOfMorces - 1))
        {
            case 0: StartCoroutine(MorseToBlink(morse0)); break;
            case 1: StartCoroutine(MorseToBlink(morse1)); break;
            case 2: StartCoroutine(MorseToBlink(morse2)); break;
            case 3: StartCoroutine(MorseToBlink(morse3)); break;
            case 4: StartCoroutine(MorseToBlink(morse4)); break;
            case 5: StartCoroutine(MorseToBlink(morse5)); break;
            default: StartCoroutine(MorseToBlink(morse0)); break;
        }
    }

    private IEnumerator MorseToBlink(string morseCode)
    {
        // Define top colors for smoother code
        Color topColors = text.colorGradient.topLeft;
        int i = 0;
        // Feature work only while GameObject is active
        while (gameObject.activeSelf)
        {
            float rate = -1f;
            // In order to simply copy from translators, I have to ignore spaces and slashes (/)
            bool isSpace = false;

            // Morse to blink
            switch (morseCode[i])
            {
                case '.': text.colorGradient = new VertexGradient(topColors, topColors, dotColor, dotColor); rate = dotRate; break;
                case '-': text.colorGradient = new VertexGradient(topColors, topColors, underscoreColor, underscoreColor); rate = underscoreRate; break;
                default: isSpace = true; break; // Counts as a space
            }

            // If space or slash ignore
            if (!isSpace)
            {
                // Making symbol color stay for a while;
                yield return new WaitForSeconds(rate);
                // Making effect of blinking
                text.colorGradient = new VertexGradient(topColors, topColors, colorInBetween, colorInBetween);
                // Wait to update to next color
                yield return new WaitForSeconds(Time.deltaTime);
            }

            // Cycle is somewhat infinite, so gotta keep couter in bounds
            if (++i >= morseCode.Length)
            {
                i = 0;
            }
        }
    }
}

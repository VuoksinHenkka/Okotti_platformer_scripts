using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TMP_ColorFlash : MonoBehaviour
{
    public Gradient FlashGradient;
    public float Speed = 1.0f;
    private float currentEstimate = 1;
    public TMPro.TMP_Text Text;
    public bool triggerOnEnable = false;


    private void OnEnable()
    {
        if (triggerOnEnable) Flash();
    }
    private void Update()
    {
        if (currentEstimate < 1)
        {
            Text.color = FlashGradient.Evaluate(currentEstimate);
            currentEstimate += Speed * Time.deltaTime;
        }
    }

    public void Flash()
    {
        currentEstimate = 0;
    }
}

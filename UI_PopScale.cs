using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PopScale : MonoBehaviour
{
    public float Speed = 2.0f;
    public float ScaleSize = 1.25f;
    private Vector3 originalSize = Vector3.one;
    private Vector3 doubleSize = Vector3.one*2;

    private float currentEstimate = 1;
    public bool triggerOnEnable = false;


    private void OnEnable()
    {
        if (triggerOnEnable) PopScale();
    }
    private void Awake()
    {
        originalSize = transform.localScale;
        doubleSize = transform.localScale * ScaleSize;
    }
    private void Update()
    {
        if (currentEstimate < 1)
        {
            transform.localScale = Vector3.Lerp(doubleSize, originalSize,currentEstimate);
            currentEstimate += Speed * Time.deltaTime;
        }
    }

    public void PopScale()
    {
        currentEstimate = 0;
    }
}

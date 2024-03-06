using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerText;
    public float time;
    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();

    }

    private void Update()
    {
        time += Time.deltaTime;
        timerText.text = string.Format("{0:N1}", time);
    }
}

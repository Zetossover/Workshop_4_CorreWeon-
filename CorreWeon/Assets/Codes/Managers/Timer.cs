using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    public static float elapsedTime;

    public static bool timerActivo = false;

    void Update()
    {
        if (!timerActivo)
            return;

        if (GameState.juegoPausado)
            return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public static void IniciarTimer()
    {
        timerActivo = true;
    }

    public static void DetenerTimer()
    {
        timerActivo = false;
    }

    public static void ResetTimer()
    {
        elapsedTime = 0f;
        timerActivo = false;
    }
}

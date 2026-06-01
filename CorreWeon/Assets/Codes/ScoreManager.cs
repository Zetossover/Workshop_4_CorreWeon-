using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void OnEnable()
    {
        NPC.OnDineroRobado += SumarPunto;
    }

    void OnDisable()
    {
        NPC.OnDineroRobado -= SumarPunto;
    }

    void SumarPunto()
    {
        score++;
        scoreText.text = "Puntaje: " + score.ToString();
    }
}
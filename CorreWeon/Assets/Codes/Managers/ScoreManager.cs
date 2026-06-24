using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public static int score = 0;

    void OnEnable()
    {
        NPC.OnDineroRobado += SumarPunto;
    }

    void OnDisable()
    {
        NPC.OnDineroRobado -= SumarPunto;
    }

    void Start()
    {
        scoreText.text = "Puntaje: " + score;
    }

    void SumarPunto()
    {
        score++;
        scoreText.text = "Puntaje: " + score;
    }
}
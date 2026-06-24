using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReiniciarJuego();
        }
    }

    void ReiniciarJuego()
    {
        GameState.Reset();

        Timer.ResetTimer();

        ScoreManager.score = 0;

        DifficultyManager.dificultadActual = NivelDificultad.Novato;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

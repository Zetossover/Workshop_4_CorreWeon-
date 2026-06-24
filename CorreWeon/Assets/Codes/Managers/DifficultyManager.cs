using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static NivelDificultad dificultadActual =
        NivelDificultad.Novato;

    void Update()
    {
        if (GameState.juegoPausado)
            return;

        float tiempo = Timer.elapsedTime;

        if (tiempo < 60)
            DifficultyManager.dificultadActual = NivelDificultad.Novato;
        else if (tiempo < 120)
            DifficultyManager.dificultadActual = NivelDificultad.Normal;
        else
            DifficultyManager.dificultadActual = NivelDificultad.Dificil;
    }
}
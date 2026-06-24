using UnityEngine;

public class GameState : MonoBehaviour
{
    public static bool juegoPausado = false;
    public static bool jugadorCapturado = false;
    public static void Reset()
    {
        juegoPausado = false;
        jugadorCapturado = false;
    }
}
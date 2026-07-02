using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelInicio;
    public GameObject panelGame;
    public GameObject panelTienda;
    public GameObject panelConfiguracion;
    public GameObject panelComoJugar;
    public GameObject panelDerrota;
    public GameObject panelPublicidad;

    void Start()
    {
        MostrarSolo(panelInicio);
    }

    public void MostrarSolo(GameObject panel)
    {
        panelInicio.SetActive(false);
        panelGame.SetActive(false);
        panelTienda.SetActive(false);
        panelConfiguracion.SetActive(false);
        panelComoJugar.SetActive(false);
        panelDerrota.SetActive(false);
        panelPublicidad.SetActive(false);

        panel.SetActive(true);
    }

    // ---------------- BOTONES ----------------

    public void IniciarJuego()
    {
        Debug.Log("Botón Jugar presionado");

        MostrarSolo(panelGame);

        GameState.juegoPausado = false;

        Timer.IniciarTimer();   // <-- Agregar
    }

    public void AbrirTienda()
    {
        MostrarSolo(panelTienda);
    }

    public void AbrirConfiguracion()
    {
        MostrarSolo(panelConfiguracion);
    }

    public void AbrirComoJugar()
    {
        MostrarSolo(panelComoJugar);
    }

    public void MostrarDerrota()
    {
        MostrarSolo(panelDerrota);

        GameState.juegoPausado = true;
    }

    public void MostrarPublicidad()
    {
        MostrarSolo(panelPublicidad);
    }

    public void VolverMenu()
    {
        MostrarSolo(panelInicio);

        GameState.juegoPausado = true;

        Timer.ResetTimer();     // Reinicia el tiempo
    }

    public void SalirJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
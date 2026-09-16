using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Estadísticas del Juego")]
    public int monedas = 0;
    public int vidas = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ActualizarInterfaz();
    }

    // Método para sumar puntos/monedas (Llamado por Moneda.cs)
    public void AddScore(int amount)
    {
        monedas += amount;
        ActualizarInterfaz();
    }

    // Alias alternativo
    public void SumarMoneda()
    {
        AddScore(1);
    }

    // Método para restar vidas cuando el jugador cae al vacío
    public void RestarVida()
    {
        vidas--;
        ActualizarInterfaz();

        if (vidas <= 0)
        {
            Debug.Log("¡Game Over!");
            ReiniciarJuego();
        }
    }

    public void ActualizarInterfaz()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ActualizarMonedas(monedas);
            UIManager.Instance.ActualizarVidas(vidas);
        }
    }

    public void ReiniciarJuego()
    {
        monedas = 0;
        vidas = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

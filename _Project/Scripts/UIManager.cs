using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Textos de la Interfaz (TextMeshPro)")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ActualizarMonedas(int cantidad)
    {
        if (scoreText != null)
        {
            scoreText.text = "MONEDAS: " + cantidad;
        }
    }

    public void ActualizarVidas(int cantidad)
    {
        if (livesText != null)
        {
            livesText.text = "VIDAS: " + cantidad;
        }
    }
}

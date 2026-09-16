using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Referencia a la Cámara")]
    [Tooltip("Arrastra la Main Camera aquí. Si se deja vacío, la buscará automáticamente.")]
    public Transform camara;

    [Header("Efecto de Profundidad (0 = Estático en el mundo, 1 = Pega a la cámara)")]
    [Range(0f, 1f)]
    [Tooltip("1.0 = Se mueve a la par de la cámara (cielo estático).\n0.7 = Se mueve casi con la cámara (montañas lejanas).\n0.3 = Se mueve despacio (colinas/árboles intermedias).\n0.0 = Se queda totalmente estático.")]
    public float efectoParallax = 1f;

    [Header("Repetición Infinita (Opcional)")]
    public bool repeticionInfinita = false;
    private float longitudSprite;

    private Vector3 ultimaPosicionCamara;

    private void Start()
    {
        // Si no se asignó manualmente en el Inspector, busca la Main Camera
        if (camara == null && Camera.main != null)
        {
            camara = Camera.main.transform;
        }

        if (camara != null)
        {
            ultimaPosicionCamara = camara.position;
        }

        // Obtener la longitud del sprite para la opción de repetición infinita
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            longitudSprite = sr.bounds.size.x;
        }
    }

    private void LateUpdate()
    {
        if (camara == null) return;

        // Calculamos cuánto se movió la cámara en este fotograma
        Vector3 movimientoCamara = camara.position - ultimaPosicionCamara;

        // Movemos el fondo proporcionalmente al efecto deseado
        transform.position += new Vector3(movimientoCamara.x * efectoParallax, movimientoCamara.y * efectoParallax, 0);

        // Repetición infinita del fondo si la opción está activada
        if (repeticionInfinita && longitudSprite > 0)
        {
            float temp = (camara.position.x * (1 - efectoParallax));
            if (temp > transform.position.x + longitudSprite)
            {
                transform.position += new Vector3(longitudSprite, 0, 0);
            }
            else if (temp < transform.position.x - longitudSprite)
            {
                transform.position -= new Vector3(longitudSprite, 0, 0);
            }
        }

        // Actualizamos la última posición guardada de la cámara
        ultimaPosicionCamara = camara.position;
    }
}

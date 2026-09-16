using UnityEngine; 
public class Meta : MonoBehaviour 
{ 
    private void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.CompareTag("Player") || collision.GetComponent<PlayerController>() != null)
{
    Debug.Log("¡FELICIDADES! ¡NIVEL COMPLETADO!");
    Time.timeScale = 0f;
    }
    }
    }
    
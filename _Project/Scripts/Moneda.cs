using UnityEngine;

public class Moneda : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    [SerializeField] private AudioClip pickupSFX;
    [SerializeField] private ParticleSystem pickupParticles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<PlayerController>() != null)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(coinValue);
            }

            if (pickupSFX != null && AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(pickupSFX);
            }

            if (pickupParticles != null)
            {
                Instantiate(pickupParticles, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}

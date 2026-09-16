using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 8f;

    [Header("Salto")]
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Límite de Caída")]
    public float fallLimit = -10f;

    [Header("Efectos Visuales y Sonido")]
    public ParticleSystem jumpParticles;
    public AudioSource jumpSFX;

    // Componentes e internas
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;
    private Vector3 puntoInicial;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Guarda el punto de reaparición (Respawn)
        puntoInicial = transform.position;
    }

    private void Update()
    {
        // 1. Detección de Suelo
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        // 2. Movimiento Horizontal
        float moveInput = Input.GetAxisRaw("Horizontal");

#if UNITY_6000_0_OR_NEWER
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
#else
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
#endif

        // 3. Voltear Sprite según la dirección
        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;

        // 4. Mecánica de Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            EjecutarSalto();
        }

        // 5. Caída al Vacío (Respawn)
        if (transform.position.y < fallLimit)
        {
            Reaparecer();
        }

        // 6. Actualizar Parámetros en el Animator
        ActualizarAnimaciones(moveInput);
    }

    private void EjecutarSalto()
    {
#if UNITY_6000_0_OR_NEWER
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
#else
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
#endif

        if (jumpParticles != null) jumpParticles.Play();
        if (jumpSFX != null) jumpSFX.Play();
    }

    private void Reaparecer()
    {
        // Le resta una vida al jugador en el GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestarVida();
        }

        // Teletransporta al personaje al inicio del mapa
        transform.position = puntoInicial;

        // Frena la aceleración de caída
#if UNITY_6000_0_OR_NEWER
        rb.linearVelocity = Vector2.zero;
#else
        rb.velocity = Vector2.zero;
#endif
    }

    private void ActualizarAnimaciones(float moveInput)
    {
        if (animator != null)
        {
            animator.SetBool("isRunning", Mathf.Abs(moveInput) > 0.1f);
            animator.SetBool("isGrounded", isGrounded);
#if UNITY_6000_0_OR_NEWER
            animator.SetFloat("yVelocity", rb.linearVelocity.y);
#else
            animator.SetFloat("yVelocity", rb.velocity.y);
#endif
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujo visual del GroundCheck en la vista de Escena
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}

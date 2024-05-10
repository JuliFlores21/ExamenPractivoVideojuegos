using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    public float moveSpeed = 35f;    // Velocidad de movimiento normal del jugador
    public float sprintSpeed = 55f;  // Velocidad de movimiento al correr
    public float jumpForce = 10f;    // Fuerza de salto del jugador
    public GameObject bulletPrefab;  // Prefab de la bala
    public Transform cannon;         // Referencia al cañón del jugador
    public float bulletForce = 17f;
    private Rigidbody rb;
    private bool isGrounded;
    private bool canDoubleJump;
    private Vector3 originalGravity;
    private Vector3 respawnPoint;    // Punto de reaparición dentro de la plataforma

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalGravity = Physics.gravity;
        respawnPoint = transform.position;   // Guarda la posición inicial como punto de reaparición
    }

    void Update()
    {
        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * currentSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // Saltar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                canDoubleJump = true; // Habilitar el doble salto después del primer salto
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, 0f); // Resetear la velocidad vertical
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canDoubleJump = false; // Deshabilitar el doble salto después del segundo salto
            }
        }
        
        // Disparar al hacer clic izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // Modificar la gravedad cuando el jugador no esté en el suelo
        if (!isGrounded)
        {
            Physics.gravity = originalGravity * 2f; // Duplicar la gravedad
        }
        else
        {
            Physics.gravity = originalGravity; // Restaurar la gravedad original
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el jugador está en el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el jugador salió de la plataforma
        if (other.gameObject.CompareTag("Respawn"))
        {
            transform.position = respawnPoint;   // Reaparecer en el punto central de la plataforma
        }
    }
    
    void Shoot()
    {
        // Instanciar una bala en la posición del cañón
        GameObject bullet = Instantiate(bulletPrefab, cannon.position, Quaternion.identity);

        // Obtener el Rigidbody de la bala
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // Aplicar una fuerza hacia arriba a la bala
        bulletRigidbody.AddForce(Vector3.up * bulletForce, ForceMode.Impulse);
    }

}

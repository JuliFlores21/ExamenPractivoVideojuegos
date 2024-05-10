using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float followSpeed = 10f; // Velocidad de seguimiento del enemigo
    void Update()
    {
        // Verificar si el jugador y el enemigo están en la misma altura
        float targetY = player.position.y;
        float currentY = transform.position.y;

        if (Mathf.Abs(targetY - currentY) > 0.1f)
        {
            // Mantener al enemigo a la misma altura que el jugador
            transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
        }

        // Seguir al jugador de izquierda a derecha
        float targetX = player.position.x;
        float currentX = transform.position.x;

        // Calcular la dirección hacia la que debe moverse el enemigo
        float moveDirection = Mathf.Sign(targetX - currentX);

        // Mover al enemigo en la dirección correcta
        transform.Translate(Vector3.right * moveDirection *followSpeed* Time.deltaTime);
    }
}

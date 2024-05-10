using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float bulletForce = 30f; // Fuerza con la que se dispara la bala
    public GameObject impactEffect; // Efecto de impacto al colisionar con el enemigo

    private void Start()
    {
        // Aplicar una fuerza hacia arriba a la bala al instanciarla
        GetComponent<Rigidbody>().AddForce(Vector3.up * bulletForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Comprobar si la colisión es con el enemigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Instanciar un efecto de impacto
            Instantiate(impactEffect, transform.position, transform.rotation);

            // Destruir la bala
            Destroy(gameObject);
        }
    }
}

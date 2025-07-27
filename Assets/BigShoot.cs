using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShoot : MonoBehaviour
{
    public float speed = 10f; // Velocidade da bala
    public float lifespan = 5f; // Tempo de vida da bala em segundos
    public int damage = 10; // Dano causado pela bala

    private float lifespanTimer; // Cronômetro para controlar o tempo de vida da bala
    private Vector3 direction; // Direção da bala

    void Start()
    {
        direction = transform.forward; // Define a direção inicial da bala como a direção em que ela está virada
        lifespanTimer = lifespan; // Inicializa o cronômetro do tempo de vida da bala
    }

    void Update()
    {
        // Atualiza o tempo de vida da bala
        lifespanTimer -= Time.deltaTime;
        if (lifespanTimer <= 0f)
        {
            Destroy(gameObject); // Destroi a bala quando o tempo de vida terminar
        }

        // Move a bala na direção definida a uma velocidade constante
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica se a bala colidiu com um inimigo
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<HealthSystemForDummies>().AddToCurrentHealth(-50);
        }

        // Continua a trajetória da bala mesmo após colidir com um inimigo
        // Define a direção como a direção anterior da bala refletida no plano da colisão
       // direction = Vector3.Reflect(direction, other.contacts[0].normal);
    }

}

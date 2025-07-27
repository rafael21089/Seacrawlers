using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MarineController : MonoBehaviour
{
    public float speed = 10f; // Velocidade do barco
    public float turnSpeed = 2f; // Velocidade de rotação do barco
    public float maxTiltAngle = 10f; // Ângulo máximo de inclinação do barco
    int sizeDir = 1;

    float timeRotate = 25f;

    private Rigidbody rb; // Componente Rigidbody do barco
    private float tiltAngle = 0f; // Ângulo de inclinação do barco

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obter o componente Rigidbody
    }

    void FixedUpdate()
    {
        // Calcular a direção para a qual o barco deve se mover
        Vector3 moveDirection = transform.forward * speed;

        // Aplicar a força no barco para mover e rotacionar ele
        rb.AddForce(moveDirection, ForceMode.Force);
        rb.AddTorque(transform.up * turnSpeed * Input.GetAxis("Horizontal"), ForceMode.Acceleration);

        // Rotacionar o barco em direção à nova direção
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        targetRotation.x = 0f; // Mantém a rotação em x em zero
        targetRotation.z = 0f; // Mantém a rotação em z em zero
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }


}

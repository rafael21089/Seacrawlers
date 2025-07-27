using System.Collections;
using UnityEngine;

public class MeteorShower : MonoBehaviour
{
    public GameObject meteorPrefab;
    public float minDelay = 0.5f;
    public float maxDelay = 2f;
    public int minMeteors = 5;
    public int maxMeteors = 10;
    public float duration = 10f;
    public float fallSpeed = 10f;
    public float forwardVelocity = 3f;

    private Bounds mapBounds;
    private bool isRunning = false;
    private float timer = 0f;


    public float explosionRadius = 10f;
    public float explosionDamage = 50f;
    public GameObject explosionEffect;
    private bool hasExploded = false;

    private void Start()
    {
        // Obtém as dimensões do mapa
        mapBounds = GetMapBounds();
    }

    private void Update()
    {
        if(!isRunning)
        {
            int numMeteors = Random.Range(minMeteors, maxMeteors + 1);
            StartCoroutine(MeteorShowerRoutine(numMeteors));
        }
            
        
        // Verifica se a chuva de meteoros ainda está ocorrendo
        if (isRunning)
        {
            // Move cada meteoro para baixo e para frente
            foreach (Transform child in transform)
            {
                Vector3 position = child.position;
                position += Vector3.down * fallSpeed * Time.deltaTime;
                position += Vector3.forward * (-1) * forwardVelocity * Time.deltaTime;
                child.position = position;

                // Remove o meteoro se atingir o chão
                if (position.y < mapBounds.min.y)
                {
                    Destroy(child.gameObject);
                }
            }

            // Interrompe a chuva de meteoros se o tempo acabou
            if (timer >= duration)
            {
                StopCoroutine(MeteorShowerRoutine(0));
                isRunning = false;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }




        // Verifica se o meteoro atingiu o chão ou algo na cena
        if (transform.position.y < 0f)
        {
            Debug.Log("nao if y<0");
            // Explode e causa dano a objetos próximos
            Explode();

            // Remove o meteoro
            Destroy(gameObject);
        }
    }

    // Cria um meteoro em uma posição aleatória no mapa
    private void CreateMeteor()
    {
       


        Vector3 position = new Vector3(
            Random.Range(mapBounds.min.x, mapBounds.max.x),
            mapBounds.max.y,
            Random.Range(mapBounds.min.z, mapBounds.max.z)
        ); 
        Vector3 pos = new Vector3(position.x, 10f, position.z);
        Quaternion rotation = Quaternion.Euler(-20f, 0f, 0f); // Rotation of -20 degrees on X axis
        GameObject meteor = Instantiate(meteorPrefab, pos, rotation);
        //meteor.transform.SetParent(transform);
        meteor.GetComponent<Rigidbody>().velocity = Vector3.down * fallSpeed + Vector3.forward * forwardVelocity;
    }

    // Corrotina que cria os meteoros em intervalos aleatórios
    private IEnumerator MeteorShowerRoutine(int numMeteors)
    {
        isRunning = true;
        for (int i = 0; i < numMeteors; i++)
        {
            CreateMeteor();
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
            timer += delay;
        }
        isRunning = false;
    }

    // Obtém as dimensões do mapa
    private Bounds GetMapBounds()
    {
        MeshRenderer[] meshRenderers = FindObjectsOfType<MeshRenderer>();
        Bounds bounds = new Bounds(transform.position, Vector3.zero);
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            bounds.Encapsulate(meshRenderer.bounds);
        }
        return bounds;
    }


    private void Explode()
    {
        // Cria o efeito de explosão
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Debug.Log("nao explode");

        // Causa dano a objetos próximos
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            // Ignora o próprio meteoro
            if (hitCollider.gameObject == gameObject)
            {
                continue;
            }

            // Aplica dano ao objeto atingido
            HealthSystemForDummies healthScript = hitCollider.gameObject.GetComponent<HealthSystemForDummies>();
            if (healthScript != null)
            {
                healthScript.AddToCurrentHealth(explosionDamage * (-1));
            }
        }

        hasExploded = true;
    }

    /*private bool CheckCollisions()
    {


        // Verifica se o meteoro colidiu com algum objeto na cena
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, fallSpeed * Time.deltaTime + 0.1f))
        {
            Debug.Log("nao colide2");
            if (!hasExploded)
            {
                Debug.Log("nao colide3");
                // Explode e causa dano a objetos próximos
                Explode();
                Debug.Log("nao colide4");
            }

            return true;
        }

        return false;
    }*/

    

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("nao collisionEnter");
        Explode();
        // Remove o meteoro
        Destroy(gameObject);
    }
}
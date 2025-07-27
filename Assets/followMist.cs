using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMist : MonoBehaviour
{
    public Transform target; // The target object to be centered
    public float offsetX = 1f; // Distance between the centered object and the target
    public float offsetZ = 1f; // Distance between the centered object and the target

    private GameObject player;
    public GameObject hubDistancia;
    public Material mat;

    float value = 0.11f;

    public float op = 0.7f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mat.SetFloat("_Radius2", value);

    }
    private void Update()
    {
        if (player.activeInHierarchy)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            this.GetComponent<MeshRenderer>().enabled = true;

            float distance = Vector3.Distance(transform.position, hubDistancia.transform.position);

            if (distance <= 140)
            {

                if (op <= 0.7f)
                {
                    mat.SetFloat("_Radius2Min", op);
                    op = op + 0.0005f;
                }

            }
            else
            {
                if (op >= 0.326f)
                {
                    mat.SetFloat("_Radius2Min", op);
                    op = op -0.0005f;
                }    
            }
        }

       

    }
    private void LateUpdate()
    {
        Vector3 d = target.position + (Vector3.forward );

        d.x = d.x + offsetX;
        d.z = d.z + offsetZ;

        transform.position = new Vector3(d.x , transform.position.y , d.z);
    }


    public void SetEnemyTransparency(float transparency)
    {


        Renderer enemyRenderer = transform.GetComponent<Renderer>();
        Material enemyMaterial = enemyRenderer.material;
        Color enemyColor = enemyMaterial.GetColor("_BaseColor");
        enemyColor.a = transparency;
        enemyMaterial.SetColor("_BaseColor", enemyColor);

    }
}

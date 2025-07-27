using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectCollisionEffect : MonoBehaviour
{

    public GameObject effect;

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Enemy")
        {
            GameObject expl = Instantiate(effect, transform.position, transform.rotation);
        }


    }
 }

using UnityEngine;
using System.Collections;

public class csParticleMove : MonoBehaviour
{
    public float speed = 0.1f;

	void Update () {

        if (this.gameObject.name == "MeteorGunnerSkill Variant(Clone)")
        {
            transform.Translate(Vector3.down * speed);

        }
        else
        {
            transform.Translate(Vector3.back * speed);
        }
    }
}

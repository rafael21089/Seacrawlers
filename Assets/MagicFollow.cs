using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFollow : MonoBehaviour
{
    public float speed = 3f;
    public GameObject target;
    public GameObject effect;

    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position;
            targetPosition.y = targetPosition.y + 1;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            transform.LookAt(targetPosition);

        }
        else
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            GameObject expl = Instantiate(effect, transform.position, transform.rotation);
        }
    }
}

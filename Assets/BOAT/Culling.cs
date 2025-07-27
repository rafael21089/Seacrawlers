using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Culling : MonoBehaviour
{
    public int culRange = 100;
    public bool softCulling = false;

    void OnEnable()
    {
        foreach (Transform toCul in transform)
        {
            TurnOnOff(toCul, false);
            CheckRange(toCul, 0);
        }
    }

    IEnumerator CheckRange(Transform toCul, int waitFor)
    {
        yield return new WaitForSeconds(waitFor);
        float curRange = Vector3.Distance(Camera.main.transform.position, toCul.position);
        if (curRange < culRange)
        {
            TurnOnOff(toCul, true);
        }
        else
        {
            TurnOnOff(toCul, false);
        }
        float checkIn = Mathf.Max(0.5f, 5f * curRange / culRange);
        CheckRange(toCul, (int)checkIn);
    }

    void TurnOnOff(Transform toCul, bool state)
    {
        if (!softCulling)
        {
            toCul.gameObject.SetActive(state);
        }
        else
        {
            foreach (Renderer r in toCul.GetComponentsInChildren<Renderer>())
            {
                r.enabled = state;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (Transform toCul in transform)
        {
            Gizmos.DrawWireSphere(toCul.position, culRange);
        }
    }
}

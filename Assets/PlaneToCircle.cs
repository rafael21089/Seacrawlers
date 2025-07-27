using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneToCircle : MonoBehaviour
{
    public Transform planeTransform; // the transform of the plane to cut

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Mesh circleMesh;

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        // create a circle mesh by sampling points along a unit circle
        circleMesh = new Mesh();
        const int numSegments = 64;
        Vector3[] vertices = new Vector3[numSegments + 1];
        int[] triangles = new int[numSegments * 3];
        float anglePerSegment = Mathf.PI * 2.0f / numSegments;
        Vector3 centroid = Vector3.zero;
        for (int i = 0; i <= numSegments; i++)
        {
            float angle = i * anglePerSegment;
            vertices[i] = new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle));
            centroid += vertices[i];
            if (i > 0)
            {
                int index = (i - 1) * 3;
                triangles[index + 0] = 0;
                triangles[index + 1] = i;
                triangles[index + 2] = i + 1;
            }
        }
        centroid /= numSegments + 1;
        for (int i = 0; i <= numSegments; i++)
        {
            vertices[i] -= centroid;
        }
        circleMesh.vertices = vertices;
        circleMesh.triangles = triangles;
        circleMesh.RecalculateNormals();

        // set the circle mesh as the mesh filter's mesh
        meshFilter.mesh = circleMesh;
    }

    private void Update()
    {
        // cast a ray from the camera towards the plane
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // set the position and rotation of the circle mesh to match the plane
            transform.position = hit.point;
            transform.rotation = Quaternion.LookRotation(hit.normal);

            // adjust the circle mesh radius to match the plane's size
            Vector3 localScale = transform.localScale;
            localScale.x = planeTransform.localScale.x;
            localScale.z = planeTransform.localScale.z;
            transform.localScale = localScale;

            // disable the plane renderer so it's not visible
            meshRenderer.enabled = false;
        }
    }
}

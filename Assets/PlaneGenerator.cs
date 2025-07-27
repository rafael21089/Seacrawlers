using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MeshFilter))]

public class PlaneGenerator : MonoBehaviour
{

    public int Size = 20;
    public float scale = 1.0f;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;
    private int verticiesLength = 0;

    [SerializeField] private Transform debugSphere;
    [SerializeField] private bool isUpdatingOnCPU = false;

    public NavMeshSurface nv;
    public SpawnIslands spIslands;
    public SpawnSeaMonsters spSeaMonsters;

    public GameObject Barco;

    bool onetime = false;

    public GameObject[] listOfSpawners;

    // Start is called before the first frame update
    void Awake()
    {
        mesh = new Mesh();

        mesh.name = "Sea";
       
        //GetComponent<MeshFilter>().mesh = mesh;
        //verticiesLength = (Size + 1) * (Size + 1);

        //UpdatePlaneVerticies();
        //UpdateMesh();

        //generate islands
        //mesh
        spIslands.generateIslands(listOfSpawners[0].transform.position, 500, listOfSpawners[0].name);
        spIslands.generateIslands(listOfSpawners[1].transform.position, 500, listOfSpawners[1].name);
        spIslands.generateIslands(listOfSpawners[2].transform.position, 500, listOfSpawners[2].name);
        spIslands.generateIslands(listOfSpawners[3].transform.position, 500, listOfSpawners[3].name);

    }


    private void FixedUpdate()
    {

        if (isUpdatingOnCPU)
        {
            UpdatePlaneVerticies();
            UpdateMesh();
        }

        if (debugSphere != null)
        {
            Vector3 newPos = debugSphere.position;
            newPos.y = WaterManager.current.getHeightAtPosition(newPos) + transform.position.y;
            debugSphere.position = newPos;
        }

 
    }

    public void Update()
    {
        if (onetime == false)
        {
            nv.UpdateNavMesh(nv.navMeshData);
            Barco.SetActive(true);

            spSeaMonsters.generateSeaMonsters(500);

            onetime = true;

            Destroy(this);
            Destroy(this.gameObject);
        }

               
    }

    void UpdatePlaneVerticies()
    {
        vertices = new Vector3[verticiesLength];
        uvs = new Vector2[vertices.Length];

        float halfSizeX = (scale * Size) / 2;
        float halfSizeZ = (scale * Size) / 2;

        int i = 0;
        for (int z = 0; z <= Size; z++)
        {
            for (int x = 0; x <= Size; x++)
            {
                float xPos = (x * scale) - halfSizeX;
                float zPos = (z * scale) - halfSizeZ;
                float yPos = 0;

                vertices[i] = new Vector3(xPos, yPos, zPos);

                if (isUpdatingOnCPU)
                    vertices[i] += WaterManager.current.GetWaveAddition(vertices[i] + transform.position, Time.timeSinceLevelLoad);

                uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
                i++;
            }
        }

        triangles = new int[Size * Size * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < Size; z++)
        {
            for (int x = 0; x < Size; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + Size + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + Size + 1;
                triangles[tris + 5] = vert + Size + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}

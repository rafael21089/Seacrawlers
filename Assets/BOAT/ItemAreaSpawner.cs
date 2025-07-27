using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAreaSpawner : MonoBehaviour
{
    public GameObject itemToSpread;
    public int numItemsToSpawnRocks = 2;
    public int numItemsToSpawnEnemies = 2;
    public int numItemsToSpawnTrees = 10;

    public float itemXSpread = 10;
    public float itemYSpread = 0;
    public float itemZSpread = 10;


    public GameObject islandPraMini;

    // Start is called before the first frame update

    void Start()
    {
        
        SpreadItem(0);
    }

    void SpreadItem(int id)
    {
        Vector3 randPosition = new Vector3(0, Random.Range(-itemYSpread, itemYSpread), 0) + transform.position;
        randPosition.y = 0f;
        GameObject clone = Instantiate(itemToSpread, randPosition, itemToSpread.transform.rotation);

        if (islandPraMini == null)
        {
            clone.GetComponent<PopulateIsland>().island = transform.parent.gameObject;

        }
        else
        {
            clone.GetComponent<PopulateIsland>().island = islandPraMini;
        }




    }
}

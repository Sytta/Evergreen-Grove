using UnityEngine;
using System.Collections;

public class SeedComponent : MonoBehaviour {
    //Time it takes before creating a tree
    public float timeToGrow = 6f;
    //The pulsation rate of the seed (Visuals only)
    public float pulseRate = 0.2f;
    TerrainManager terrain;
	// Use this for initialization
	void Start () {
        terrain = FindObjectOfType<TerrainManager>();
        StartCoroutine("CountDown");
	}
	
    // Called when a seed on an empty tile wants to become a tree
    void GrowTree()
    {
        TreeComponent newTree = terrain.SpawnTree(transform.position);
        if (newTree != null)
            newTree.AddSeed();

        Destroy(gameObject);
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(timeToGrow);
        /*for(int i=0;i<timeToGrow/pulseRate; i++)
        {
            yield return new WaitForSeconds(timeToGrow*pulseRate);
        }*/
        GrowTree();
    }
}

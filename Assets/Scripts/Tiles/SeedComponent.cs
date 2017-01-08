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
    }
    IEnumerator CountDown()
    {
        for (int i = 0; i < 10; i++)
        {
            while (GameManager.instance.state == GM_InGame_State.Paused)
            {
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(timeToGrow/10.0f);
        }
        /*for(int i=0;i<timeToGrow/pulseRate; i++)
        {
            yield return new WaitForSeconds(timeToGrow*pulseRate);
        }*/
        GrowTree();
        for(int i=0;i<100;i++)
        {
            transform.position += Vector3.down*0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}

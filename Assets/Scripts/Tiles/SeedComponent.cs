﻿using UnityEngine;
using System.Collections;

public class SeedComponent : MonoBehaviour {
    //Time it takes before creating a tree
    public float timeToGrow = 3;
    //The pulsation rate of the seed (Visuals only)
    public float pulseRate = 0.2f;
    TerrainManager terrain;
	// Use this for initialization
	void Start () {
        terrain = FindObjectOfType<TerrainManager>();
        StartCoroutine("CountDown");
	}
	
    void GrowTree()
    {
        terrain.PlantTree(transform.position);
        Destroy(gameObject);
    }
    IEnumerator CountDown()
    {
        for(int i=0;i<timeToGrow/pulseRate; i++)
        {
            yield return new WaitForSeconds(timeToGrow*pulseRate);
        }
        GrowTree();
    }
}

using UnityEngine;
using System.Collections;

//Tree class that handles all tree states and behaviours
/*
 * Call Spawn() when planting a tree
 * Call ReceiveDisease() to contaminate a tree
 */

public class TreeComponent : MonoBehaviour
{
    public Color diseasedLeavesColor;
    public Color sicklyLeavesColor;
    public Color diseasedBarkColor;
    public Color sicklyBarkColor;
    public float minimumSeedPlantTime;
    public float maximumSeedPlantTime;
    public float timeToTurnDiseased=2;
    private int seedCount;

    TerrainManager terrain;
    Color healthyBarkColor;
    Color healthyLeavesColor;
    SkinnedMeshRenderer mr;
    Animator anim;
    bool isDiseased = false;
    bool isSickly = false;
    bool isPlantingSeed=false;
    const int LEAVES_MAT_INDEX = 0;
    const int BARK_MAT_INDEX = 1;
    // Use this for initialization
    void Start()
    {
        mr = GetComponentInChildren<SkinnedMeshRenderer>();
        anim = GetComponent<Animator>();
        terrain = FindObjectOfType<TerrainManager>();
        healthyBarkColor = mr.materials[BARK_MAT_INDEX].color;
        healthyLeavesColor = mr.materials[LEAVES_MAT_INDEX].color;
    }
    void Spawn()
    {
        anim.SetTrigger("PlantTree");
    }
    void Die()
    {
        terrain.RemoveTree(transform.position);
    }
    public void FallDown()
    {
        Die();
    }
    public void ReceiveDisease()
    {
        isDiseased = true;
        anim.SetBool("Diseased", true);
        StartCoroutine(TurnToDiseased());
    }
    public void TurnSickly()
    {
        Color newColor = Color.Lerp(healthyBarkColor, sicklyBarkColor, (GameManager.instance.GetNatureLevel() - 0.5f) / 0.5f);
        mr.materials[BARK_MAT_INDEX].color = newColor;

        newColor = Color.Lerp(healthyLeavesColor, sicklyLeavesColor, (GameManager.instance.GetNatureLevel() - 0.5f) / 0.5f);
        mr.materials[LEAVES_MAT_INDEX].color = newColor;
        isSickly = true;
        
    }
    public void TurnHealthy()
    {
        Color newColor = healthyBarkColor;
        mr.materials[BARK_MAT_INDEX].color = newColor;

        newColor = healthyLeavesColor;
        mr.materials[LEAVES_MAT_INDEX].color = newColor;
        isSickly = false;

    }
    public void AddSeed()
    {
        seedCount++;
        if(!isPlantingSeed)
        {
            StartCoroutine("PlantSeed");
        }
    }
    public void CutDown()
    {
        Die();
    }
    void SpreadDisease()
    {
        terrain.SpreadInfection(transform.position);
        Die();
    }
    IEnumerator PlantSeed()
    {
        isPlantingSeed = true;
        for (int i = 0; i < seedCount; i++)
        {
            float plantingTime = Random.Range(minimumSeedPlantTime, maximumSeedPlantTime);
            yield return new WaitForSeconds(plantingTime);
            terrain.SpreadSeed(transform.position);
        }
        isPlantingSeed = false;
        
        
    }
    IEnumerator TurnToDiseased()
    {

        for (int i = 0; i < 20; i++)
        {
            Color newColor = Color.Lerp(mr.materials[BARK_MAT_INDEX].color, diseasedBarkColor, i/20.0f);
            mr.materials[BARK_MAT_INDEX].color = newColor;
            newColor = Color.Lerp(mr.materials[LEAVES_MAT_INDEX].color, diseasedLeavesColor, i/20.0f);
            mr.materials[LEAVES_MAT_INDEX].color = newColor;
            yield return new WaitForSeconds(timeToTurnDiseased/20);
        }
        SpreadDisease();

    }

}

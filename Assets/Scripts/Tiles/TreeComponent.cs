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
    public float TimeToTurnDiseased=2;

    Color healthyBarkColor;
    Color healthyLeavesColor;
    SkinnedMeshRenderer mr;
    Animator anim;
    bool isDiseased = false;
    bool isSickly = false;
    const int LEAVES_MAT_INDEX = 0;
    const int BARK_MAT_INDEX = 1;
    // Use this for initialization
    void Start()
    {
        mr = GetComponentInChildren<SkinnedMeshRenderer>();
        anim = GetComponent<Animator>();
        healthyBarkColor = mr.materials[BARK_MAT_INDEX].color;
        healthyLeavesColor = mr.materials[LEAVES_MAT_INDEX].color;
    }
    void Spawn()
    {
        anim.SetTrigger("PlantTree");
    }
    void Die()
    {
        Destroy(gameObject);
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
    private void SpreadDisease()
    {

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
    IEnumerator TurnToDiseased()
    {

        for (int i = 0; i < 20; i++)
        {
            Color newColor = Color.Lerp(mr.materials[BARK_MAT_INDEX].color, diseasedBarkColor, i/20.0f);
            mr.materials[BARK_MAT_INDEX].color = newColor;
            newColor = Color.Lerp(mr.materials[LEAVES_MAT_INDEX].color, diseasedLeavesColor, i/20.0f);
            mr.materials[LEAVES_MAT_INDEX].color = newColor;
            yield return new WaitForSeconds(TimeToTurnDiseased/20);
        }
        SpreadDisease();

    }

}

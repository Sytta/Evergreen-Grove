using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class TreeComponent : MonoBehaviour
{

    public Color diseasedColor;
    public Color sicklyColor;
    public float TimeToTurnDiseased=2;

    Color healthyColor;
    MeshRenderer mr;
    bool isDiseased = false;
    // Use this for initialization
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        healthyColor = mr.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        EvaluateHealth();
    }
    void EvaluateHealth()
    {
        if(GameManager.instance.GetNatureState()==GM_Nature_State.HighNatureLevel)
        {
            Color newColor = Color.Lerp(healthyColor, sicklyColor, (GameManager.instance.GetNatureLevel()-0.5f)/0.5f);
            mr.material.color = newColor;
        }
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
        StartCoroutine(TurnToDiseased());
    }
    private void SpreadDisease()
    {

    }
    IEnumerator TurnToDiseased()
    {
        for (int i = 0; i < 10; i++)
        {
            Color newColor = Color.Lerp(mr.material.color, diseasedColor, 0.1f);
            mr.material.color = newColor;
            yield return new WaitForSeconds(TimeToTurnDiseased/10);
        }

    }

}

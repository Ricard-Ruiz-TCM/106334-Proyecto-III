using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntitieUI : MonoBehaviour
{
    [SerializeField] Image healthbarSprite;
    float target;
    [SerializeField] float speed = 2;

    public List<GameObject> activeBuffs;
    public List<Vector3> positionBuffs;
    private List<GameObject> activeBuffsGO;

    private void Awake()
    {
        activeBuffs = new List<GameObject>();
        activeBuffsGO = new List<GameObject>();
        healthbarSprite.fillAmount = 1;
        target = 1;
    }

    public void SetDamage(float damage)
    {
        target -= damage;
    }
    public void AddBuff(GameObject buff)
    {
        activeBuffs.Add(buff);
        GameObject buffGO = Instantiate(buff, Vector3.zero, Quaternion.identity);
        buffGO.transform.SetParent(transform);
        buffGO.transform.localScale = new Vector3(5, 5, 5);
        buffGO.transform.localRotation = Quaternion.Euler(Vector3.zero);
        activeBuffsGO.Add(buffGO);
        for (int i = 0; i < activeBuffs.Count; i++)
        {
            activeBuffsGO[i].transform.localPosition = positionBuffs[i];
        }
    }
    public void RemoveBuff(GameObject buff)
    {
        for (int i = 0; i < activeBuffs.Count; i++)
        {
            if (activeBuffs.Contains(buff))
            {
                activeBuffs.Remove(buff);

                Destroy(activeBuffsGO[i]);
                activeBuffsGO.RemoveAt(i);
            }

            if(activeBuffs.Count != 0)
            {
                activeBuffs[i].transform.position = positionBuffs[i];
            }          
        }
    }
    public void SetHeal(float heal)
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        healthbarSprite.fillAmount = Mathf.MoveTowards(healthbarSprite.fillAmount, target, speed * Time.deltaTime);
    }
}

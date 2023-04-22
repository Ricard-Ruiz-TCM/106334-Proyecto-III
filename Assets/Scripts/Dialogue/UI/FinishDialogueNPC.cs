using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDialogueNPC : MonoBehaviour
{
    [SerializeField] private GameObject perksTree;
    [SerializeField] private GameObject PC;
    [SerializeField] private GameObject inShopDialogueBox;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject NPC;
    [SerializeField] private GameObject NPCZoomed;
    [SerializeField] private float zoomQ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startLevelUp()
    {
        perksTree.SetActive(true);
        inShopDialogueBox.SetActive(true);
        NPCZoomed.SetActive(true);
        PC.SetActive(false);
        dialogueBox.SetActive(false);
        NPC.SetActive(false);
        gameObject.transform.localScale = new Vector3(zoomQ, zoomQ, 0);
    }
}

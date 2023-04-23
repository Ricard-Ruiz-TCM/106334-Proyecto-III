using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithDialogue : MonoBehaviour
{
    [SerializeField] private GameObject materialShop;
    [SerializeField] private GameObject PC;
    [SerializeField] private GameObject playerMaterialBox;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject NPC;
    [SerializeField] private GameObject NPCZoomed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startShopping()
    {
        materialShop.SetActive(true);
        playerMaterialBox.SetActive(true);
        NPCZoomed.SetActive(true);
        PC.SetActive(false);
        dialogueBox.SetActive(false);
        NPC.SetActive(false);
    }
}

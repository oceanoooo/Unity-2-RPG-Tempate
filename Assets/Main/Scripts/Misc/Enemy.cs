using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
   public Item.ItemType type;
   public PlayerManager pm;
   public GameObject popupText;

void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

private void OnTriggerEnter2D(Collider2D collision)
    {
        bool didLose = pm.LoseItems(type); 
        
        if(didLose)
            popupText.GetComponent<TextMeshProUGUI>().text = $"You Lost 100 gold.";
        
        else 
            popupText.GetComponent<TextMeshProUGUI>().text = $"The theif didn't steal from you :).";
    }
  
}

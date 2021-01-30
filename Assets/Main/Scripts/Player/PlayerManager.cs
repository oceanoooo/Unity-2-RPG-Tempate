﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class inventorySlotProxy
{
    public int itemIndex;
    public int itemAmount;
}


public class PlayerManager : MonoBehaviour
{
    public int gold = 0;
    public int experience = 1;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI playerNameText;
    public LootTable lt;
    public List<inventorySlotProxy> inventory = new List<inventorySlotProxy>();

    // Start is called before the first frame update
    void Start()
    {
        lt = GameObject.FindGameObjectWithTag("LootTable").GetComponent<LootTable>();
        for (int i = 0; i < lt.lootItems.Count; i++)
        {
            inventory.Add(new inventorySlotProxy { itemIndex = i, itemAmount = 0 });
        }
        playerNameText.text = "Name: " + PlayerPrefs.GetString(PrefNames.playerName);
        UpdateUI();
    }

    public void UpdateUI()
    {
        goldText.text = "Gold: " + gold.ToString();
        expText.text = "Exp: " + experience.ToString();
    }

    public int SellAll(Item.ItemType iType)
    {
        int amountMade = 0;
        for (int i = 0; i < inventory.Count; i++)
        {
            if(lt.lootItems[inventory[i].itemIndex].itemType == iType)

            {
                amountMade += lt.lootItems[inventory[i].itemIndex].price * inventory[i].itemAmount;
                inventory[i] = new inventorySlotProxy { itemIndex = i, itemAmount = 0 };
            }
            
        }
        gold += amountMade;
        UpdateUI();
        return amountMade;

    }
    public bool LoseItems(Item.ItemType iType)
    {
        int LostItem = Random.Range(0,2);
        if(LostItem == 0)
        { 
            
           
            if(gold >= 100) {
                gold -= 100;
                    
                UpdateUI();
                return true;
            }
            else 
            { 
                return false;
            }
        }
        else 
        {
            int index = Random.Range(0,8);
            inventory[lt.ToSimpleInventory(index)].itemAmount -= 1;
            UpdateUI();
            return true;
        }
    





    }
    public void AddToInventory(List<int> values)
    {
        
        foreach(int index in values)
        {
            inventory[lt.ToSimpleInventory(index)].itemAmount += 1;
        }
        float multipler = (float) PlayerPrefs.GetInt(PrefNames.difficulty);
        experience += Mathf.RoundToInt(Random.Range(1/multipler, multipler) *
                            (((float)values.Count)/multipler));
        UpdateUI();
    }
}

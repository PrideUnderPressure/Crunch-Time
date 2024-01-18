using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
public class TableScript : MonoBehaviour
{
    public TableSlotScript slot1;
    public TableSlotScript slot2;
    public TableSlotScript slot3;
    public TableSlotScript slot4;
    public TableSlotScript slot5;

    public GameObject slot1card;
    public GameObject slot2card;
    public GameObject slot3card;
    public GameObject slot4card;
    public GameObject slot5card;
    
    // Update is called once per frame
    void Update()
    {
        if (slot1.cardInSlot != null)
        {
            slot1.isLocked = true;
            slot1card = slot1.cardInSlot;
        }

        if (slot2.cardInSlot != null)
        {
            slot2.isLocked = true;
            slot2card = slot2.cardInSlot;
        }

        if (slot3.cardInSlot != null)
        {
            slot3.isLocked = true;
            slot3card = slot3.cardInSlot;
        }
        
        if (slot4.cardInSlot != null)
        {
            slot4.isLocked = true;
            slot4card = slot4.cardInSlot;
        }
        
        if (slot5.cardInSlot != null)
        {
            slot5.isLocked = true;
            slot5card = slot5.cardInSlot;
        }
        
    }

}

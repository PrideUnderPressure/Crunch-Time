using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookerSlotScript : MonoBehaviour
{
    public bool isLocked;
    public SelectedCardScript selectedCardScript;
    public GameObject selectedCard;
    public float distanceFromCard;
    public bool cardInRange;
    
    public GameObject cardInSlot;
    private void Start()
    {
        selectedCardScript = GameObject.FindWithTag("CardManager").GetComponent<SelectedCardScript>();
    }

    private void Update()
    {
        if (selectedCardScript.selectedCard != null && !isLocked)
        {
            selectedCard = selectedCardScript.selectedCard;

            // Calculate the distance between this gameObject and the selectedCard
            distanceFromCard = Vector3.Distance(transform.position, selectedCard.transform.position);
            if (distanceFromCard > 0.6f)
            {
                if (cardInRange)
                {
                    selectedCard.GetComponent<CardSelect>().SetSurface(null);
                    cardInRange = false;
                }
            }
            else if (distanceFromCard <= 0.6f)
            {
                if (cardInRange != true)
                {
                    selectedCard.GetComponent<CardSelect>().SetSurface(this.gameObject);
                    cardInRange = true;
                }
            }
        }

        if (isLocked)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (!isLocked)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }


}

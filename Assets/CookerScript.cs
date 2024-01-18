using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
public class CookerScript : MonoBehaviour
{
    public CookerSlotScript slot1;
    public CookerSlotScript slot2;
    public CookerSlotScript slot3;

    public GameObject slot1card;
    public GameObject slot2card;
    public GameObject slot3card;
    
    public GameObject dishPrefab;

    public bool canCook = false;

    public CardCombinationsList cardCombinationsList;
    //Cooked Card
    public int newCardValue;
    public int newCookingTime;
    public Sprite dishImage;
    public string dishName;
    public List<CardFlavour> combinedFlavours = new List<CardFlavour>();


    private int card1Value;
    private int card2Value;
    private int card3Value;
    public TMP_Text dishPredictedValue;
    public string dishPredictedString;

    public GameObject spawnedDish;

    void Start()
    {
        cardCombinationsList = GameObject.FindWithTag("DeckManager").GetComponent<CardCombinationsList>();
    }

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

        if (slot1card && slot2card)
        {
            canCook = true;
        }
        else if (!slot1card || !slot2card)
        {
            canCook = false;
        }

        if (Input.GetKeyDown(KeyCode.C) && canCook)
        {
            CombineCards();
        }
    }

    private void FixedUpdate()
    {
        UpdateDishValueText();
    }

    public void CombineCards()
    {
        CalculateNewValue();
        CalculateNewTime();
        CalculateNewFlavours();
        FindCardCombination();
        
        //This creates a new one
        RemoveTheCards();
        spawnedDish = Instantiate(dishPrefab, slot2.transform.position, Quaternion.identity);
        spawnedDish.GetComponent<DishScript>().InitiateDish(combinedFlavours, dishName, newCardValue, dishImage);
        spawnedDish.GetComponent<CardSelect>().cookerScript = this;
    }

    public void UnlockSlots()
    {
        slot1.isLocked = false;
        slot2.isLocked = false;
        slot3.isLocked = false;
    }

    private void RemoveTheCards()
    {
        if (slot1card)
        {
            
            Destroy(slot1card);
            slot1card = null;
        }
        //2
        if (slot2card)
        {
            Destroy(slot2card);
            slot2card = null;
        }
        //3
        if (slot3card)
        {
            Destroy(slot3card);
            slot3card = null;
        }
    }
    private void CalculateNewValue()
    {
        
        newCardValue = slot1card.GetComponent<CardScript>().cardValueInt +
                       slot2card.GetComponent<CardScript>().cardValueInt;
        if (slot3card != null)
        {
            newCardValue += slot3card.GetComponent<CardScript>().cardValueInt;
        }
    }

    private void UpdateDishValueText()
    {
        //1
        if (slot1card)
        {
            card1Value = slot1card.GetComponent<CardScript>().cardValueInt;
        }
        else if (!slot1card)
        {
            card1Value = 0;
        }
        //2
        if (slot2card)
        {
            card2Value = slot2card.GetComponent<CardScript>().cardValueInt;
        }
        else if (!slot2card)
        {
            card2Value = 0;
        }
        //3
        if (slot3card)
        {
            card3Value = slot3card.GetComponent<CardScript>().cardValueInt;
        }
        else if (!slot3card)
        {
            card3Value = 0;
        }

        if ((card1Value + card2Value + card3Value) <= 0)
            dishPredictedValue.text = null;
        else
        {
            dishPredictedString = (card1Value + card2Value + card3Value).ToString();
            dishPredictedValue.text = dishPredictedString;
        }
    }

    private void CalculateNewTime()
    {
        newCookingTime = slot1card.GetComponent<CardScript>().cardTimeInt +
                       slot2card.GetComponent<CardScript>().cardTimeInt;
        if (slot3card != null)
        {
            newCookingTime += slot3card.GetComponent<CardScript>().cardTimeInt;
        }
    }
    
    private void CalculateNewFlavours()
    {
        combinedFlavours.Clear(); // Clear the list first to start with an empty list

        // Add flavours from slot1card
        combinedFlavours.AddRange(slot1card.GetComponent<CardScript>().cardFlavours);

        // Add flavours from slot2card
        combinedFlavours.AddRange(slot2card.GetComponent<CardScript>().cardFlavours);
        if (slot3card != null) 
        {
            // Add flavours from slot3card
            combinedFlavours.AddRange(slot3card.GetComponent<CardScript>().cardFlavours);
        }

        // Remove duplicates by converting to a HashSet and back to a List
        combinedFlavours = combinedFlavours.Distinct().ToList();
    }

    private void FindCardCombination()
    {
        var card1ID = slot1card.GetComponent<CardScript>().cardID;
        var card2ID = slot2card.GetComponent<CardScript>().cardID;
        
        // Check each entry in the cardCombinations list
        foreach (DishCombination combination in cardCombinationsList.cardCombinations)
        {
            // Compare both cardID1 and cardID2 for a match
            if ((combination.cardID1 == card1ID && combination.cardID2 == card2ID) ||
                (combination.cardID1 == card2ID && combination.cardID2 == card1ID))
            {
                // A matching combination is found
                dishImage = combination.dishImage;
                dishName = combination.dishName;

                // You can now use dishImage and dishName as needed
                Debug.Log("Found a matching dish: " + dishName);
                // Do something with the dishImage and dishName, e.g., display them in your UI
                break; // Exit the loop since you found a match
            }
        }
    }
}

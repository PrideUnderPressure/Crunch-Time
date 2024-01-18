using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DishScript : MonoBehaviour
{
    public int cardValue;
    public List<CardFlavour> cardFlavours;
    
    public Image dishImage;
    public TMP_Text dishValue;
    public TMP_Text dishName;
    public TMP_Text flavours;

    public void InitiateDish(List<CardFlavour> list, string dishNameNew, int cardValueNew, Sprite dishImageNew)
    {
        cardValue = cardValueNew;
        dishName.text = dishNameNew;
        cardFlavours = list;
        dishImage.sprite = dishImageNew;
        
        var howManyFlavours = cardFlavours.Count;
        for (int i = 0; i < howManyFlavours; i++)
        {
            var myString = cardFlavours[i].ToString();
            if (cardFlavours[i] != 0)
            {
                AddCardText(myString);
            }
            else 
                break;
        }
    }
    
    public void AddCardText(string textToAdd)
    {
        flavours.text = flavours.text + ("\n") + textToAdd;
    }

    private void FixedUpdate()
    {
        dishValue.text = cardValue.ToString();
    }
}

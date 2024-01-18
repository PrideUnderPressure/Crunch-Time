using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeckScript : MonoBehaviour
{
    public List<CardScriptableObject> deckCards;
    public List<CardScriptableObject> drawnCards;
    public List<CardScriptableObject> playedCards;
    public CardScriptableObject drawnCard;
    public CardStorage cardStorage;
    public List<GameObject> cardsInHand;
    
    public List<GameObject> handPoints;
    public GameObject spawnPoint;
    
    
    public GameObject cardPrefab;
    
    public float cardDrawDelay = 0f;
    
    public int handSize = 0;
    private bool drawing = false;

    public float slideDuration = 1f;
    

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !drawing)
        {
            StartCoroutine(DrawCardsWithDelay());
        }
    }

    private void FixedUpdate()
    {
        
    }

    private IEnumerator DrawCardsWithDelay()
    {
        if (handSize >= 6)
        {
            yield break; // No need to draw more cards if hand is full
        }

        drawing = true;

        var howManyToDraw = 6 - handSize;
        for (int i = 0; i < howManyToDraw; i++)
        {
            if (deckCards.Count <= 0)
            {
                List<CardScriptableObject> cardsToMoveBack = new List<CardScriptableObject>();

                foreach (var card in playedCards)
                {
                    cardsToMoveBack.Add(card);
                }

                foreach (var card in cardsToMoveBack)
                {
                    playedCards.Remove(card);
                    deckCards.Add(card);
                }
            }
            GameObject spawnedCard = Instantiate(cardPrefab, spawnPoint.transform.position, Quaternion.identity);
            int x = Random.Range(0, deckCards.Count);
            drawnCard = deckCards[x];
            deckCards.RemoveAt(x);
            spawnedCard.GetComponent<CardScript>().InitiateCard(drawnCard, cardStorage);
            StartCoroutine(SlideTheCardIn(spawnedCard, handSize));
            handSize++;
            yield return new WaitForSeconds(cardDrawDelay);
        }
        
        drawing = false;
    }

    private IEnumerator SlideTheCardIn(GameObject objectToSlide, int slot)
    {
        slideDuration = 1.0f; // Adjust the duration as needed
        float elapsedTime = 0.0f;
        Vector3 initialPosition = objectToSlide.transform.position;
        Vector3 targetPosition = handPoints[slot].transform.position;

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.SmoothStep(0.0f, 1.0f, elapsedTime / slideDuration); // Apply ease-in effect

            objectToSlide.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            yield return null;
        }

        // Ensure that the card reaches the exact target position
        objectToSlide.transform.position = targetPosition;
        cardsInHand.Add(objectToSlide);
    }

    public void CardPlayed(GameObject playedCard, CardScriptableObject scriptableCard)
    {
        cardsInHand.Remove(playedCard);
        playedCards.Add(scriptableCard);
        handSize -= 1;
        for (int x = 0; x < cardsInHand.Count; x++)
        {
            cardsInHand[x].transform.position = handPoints[x].transform.position;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using System;
using Random = UnityEngine.Random;
using System.Drawing;
using System.Linq;



public class GameManager : MonoBehaviour
{

    public GameObject[] cards;

    public GameObject deck;
    public GameObject playerCard;
    public GameObject[] playerHand;
    public GameObject[] opponentHand;

    
    public int playerCardValue;
    [SerializeField] private int playerScore;
    public GameObject opponentCard;
    public int opponentCardValue;
    [SerializeField] private int opponentScore;
    public TextMeshProUGUI playerText;
    public TextMeshProUGUI opponentText;
    public TextMeshProUGUI ManyCardsText;

    
    int totalCards;
    
    public GameObject[] pile;

    CardAttributes cardAttributes;
    CardController cardController;
    public bool isPlayerFirst = true;
    private int dif;

    // Start is called before the first frame update
    void Start()
    {

        CreateHands();
        
    }

    void Awake()
    {
        createCards();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject CardSelector(bool isPlayerFirst)
    {
        Debug.Log("card selecter called");
        // maximum card value for opponent

        if (isPlayerFirst == false)
        {
            opponentCardValue = 0;
            foreach (GameObject card in opponentHand)
            {
                if (card.GetComponent<CardAttributes>().value > opponentCardValue)
                {

                    opponentCardValue = card.GetComponent<CardAttributes>().value;
                    opponentCard = card;

                }
            }
        }
        else
        {
            List<CardAttributes> maxNumbers = new List<CardAttributes>();
            List<CardAttributes> numbers = new List<CardAttributes>();
            foreach(GameObject card in opponentHand)
            {
                CardAttributes vl = card.GetComponent<CardAttributes>();
                numbers.Add(vl);

                if(card.GetComponent<CardAttributes>().value > playerCardValue)
                {
                    CardAttributes value = card.GetComponent<CardAttributes>();
                    maxNumbers.Add(value);
                }
               
            }
            if (maxNumbers.Count > 0)
            {
                
                //opponentCardValue = maxNumbers.AsQueryable().Min();
            }
            else
            {
                //opponentCardValue = numbers.AsQueryable().Min();
            }
        }
        


        int index = Array.IndexOf(opponentHand, opponentCard);

        opponentCard = opponentHand[index];

        RemoveElement(ref opponentHand, index);
        return opponentCard;
        //opponentCard.transform.position =  new Vector3 (0, -0.100000001f, 0.5f);      


    }


    public void scoreCalculator()
    {
        if(playerCardValue > opponentCardValue)
        {
            playerScore++;
            playerText.text = "Point: " + playerScore;
        }
        else if(playerCardValue < opponentCardValue)
        {
            opponentScore++;
            opponentText.text = "Point: " + opponentScore;
        }
        else
        {
            playerScore++;
            opponentScore++;
            opponentText.text = "Point: " + opponentScore;
            playerText.text = "Point: " + playerScore; 
        }

 
    }

    private void CreateHands()
    {
        int playerZ = 5;
        float playerX = 10;

        for(int i = 0; i < 4; i++)
        {
            
            playerHand[i] = cards[0];           
            playerHand[i].transform.position = new Vector3(playerX, -7.5f, 0.5f);
            playerHand[i].transform.localScale = new Vector3(50, 50, playerZ);
            playerHand[i].transform.localRotation = new Quaternion(0,0,0,0);
            RemoveElement(ref cards, 0);
            
            playerHand[i] = Instantiate(playerHand[i]);
            playerX -= 2.5f;
            playerZ++;
            

        }

        int opponentZ = 5;
        float opponentX = 5;
        for (int i = 0; i < 4; i++)
        {
            opponentHand[i] = cards[0];           
            opponentHand[i].transform.position = new Vector3(opponentX, 7.5f, 0.5f);
            opponentHand[i].transform.localScale = new Vector3(50, 50, opponentZ);
            opponentHand[i].transform.localRotation = new Quaternion(0, 1, 0, 0);
            RemoveElement(ref cards, 0);
            opponentHand[i] = Instantiate(opponentHand[i]);
            opponentX -= 2.5f;
            opponentZ++;
            

        }
        Debug.Log("hands created");
        ManyCardsText.text = "Cards: " + cards.Length;
        
    }
    public static void RemoveElement<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            
            arr[a] = arr[a + 1];
        }
        
        Array.Resize(ref arr, arr.Length - 1);
    }


    //private void Shuffle(GameObject[] deck)
    //{
    //    for (int i = 0; i < deck.Length; i++)
    //    {
    //        container[0] = deck[i];
    //        int randomIndex = Random.Range(i,deck.Length);
    //        deck[i] = deck[randomIndex];
    //        deck[randomIndex] = container[0];
    //    }
    //    Debug.Log("cards shuffled");
    //}
    void createCards()
    {
        totalCards = deck.transform.childCount;
        cards = new GameObject[totalCards];

        for (int i = 0; i < totalCards; i++)
        {
            cards[i] = deck.transform.GetChild(i).gameObject;
        }
        cards = Shuffle(cards);

    }
    private T[] Shuffle<T>(T[] array)
    {
        System.Random rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        return array;
    }


}

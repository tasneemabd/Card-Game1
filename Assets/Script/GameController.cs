using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    public GameObject Card;
    public Transform tf_BaxCard;
    public List<GameObject> listCard = new List<GameObject>();
    public List<GameObject> listCardPlayer = new List<GameObject>();
    public List<GameObject> listCardAI = new List<GameObject>();
    public List<GameObject> listCardAI1 = new List<GameObject>();
    public List<GameObject> listCardAI2 = new List<GameObject>();
    public Text txt_GameResult;

    public Transform[] arr_Tf_AI, arr_Tf_Player, arr_Tf_AI1, arr_Tf_AI2;

    public Sprite Sp_Win, sp_Lost;


    public Image img_Resuft;
    public int scorePlayer, scoreAI, scoreAI1, scoreAI2;
   public Text txt_Round;
    public GameObject sp_WinPlayer;
    public GameObject sp_LostPlayer;
    public GameObject sp_WinAI;
    public GameObject sp_LostAI;
    public GameObject sp_WinAI1;
    public GameObject sp_LostAI1;
    public GameObject sp_WinAI2;
    public GameObject sp_LostAI2;

    public Transform tf_PlayerPlayPosition;
    public Transform tf_AIPlayPosition;
    public Transform tf_AI1PlayPosition;
    public Transform tf_AI2PlayPosition;

    private int currentTurn = 0;
    private GameObject selectedCard;
    private int currentRound = 0;

    public Image img_Player;
    public Image img_AI;
    public Image img_AI1;
    public Image img_AI2;

    public Sprite playerImage;
    public Sprite aiImage;
    public Sprite ai1Image;
    public Sprite ai2Image;
    public Button restartButton;
 

    public int winCountPlayer = 0;
    public int winCountAI = 0;
    public int winCountAI1 = 0;
    public int winCountAI2 = 0;

    public Text txt_WinCountPlayer;
    public Text txt_WinCountAI;
    public Text txt_WinCountAI1;
    public Text txt_WinCountAI2;




    private AudioSource audioSource;
   
    public AudioClip dealSound;
    
    public AudioClip playSound;

    void Start()
    {
        //playerRoundScore = 0;
        //aiRoundScore = 0;
        //ai1RoundScore = 0;
        //ai2RoundScore = 0;

        winCountPlayer = 0;
        winCountAI = 0;
        winCountAI1 = 0;
        winCountAI2 = 0;

        txt_WinCountPlayer.text =  winCountPlayer.ToString();
        txt_WinCountAI.text =  winCountAI.ToString();
        txt_WinCountAI1.text = winCountAI1.ToString();
        txt_WinCountAI2.text =  winCountAI2.ToString();

        txt_GameResult.text = "";

        img_Player.sprite = playerImage;
        img_AI.sprite = aiImage;
        img_AI1.sprite = ai1Image;
        img_AI2.sprite = ai2Image;
        audioSource = GetComponent<AudioSource>();
        restartButton.gameObject.SetActive(false);

        InstanceCard();
    }




    public void InstanceCard()
    {
        for (int i = 0; i < SpriteGame.instance.arr_Sp_Cards.Length; i++)
        {
            GameObject _Card = Instantiate(Card, tf_BaxCard.position, Quaternion.identity);
            _Card.transform.SetParent(tf_BaxCard, false);
            _Card.GetComponent<UI>().img_card.sprite = SpriteGame.instance.arr_Sp_Cards[i];

            _Card.GetComponent<Button>().onClick.AddListener(() => OnCardClick(_Card));

            listCard.Add(_Card);
        }
        StartCoroutine(SplitCard());
    }

    IEnumerator SplitCard()
    {


        scoreAI = 0;
        scorePlayer = 0;
        scoreAI1 = 0;
        scoreAI2 = 0;

        listCardPlayer.Clear();
        listCardAI.Clear();
        listCardAI1.Clear();
        listCardAI2.Clear();

        // Distribute 13 cards to each player
        for (int i = 0; i < 13; i++)
        {
            yield return new WaitForSeconds(0.25f);

            // Distribute card to Player
            int rdPlayer = Random.Range(0, listCard.Count);
            GameObject playerCard = listCard[rdPlayer];
            playerCard.transform.SetParent(arr_Tf_Player[i % 13], true);
            iTween.MoveTo(playerCard,
                iTween.Hash("position", arr_Tf_Player[i % 13].position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            iTween.RotateBy(playerCard,
                iTween.Hash("x", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            yield return new WaitForSeconds(0.25f);
            playerCard.GetComponent<UI>().Gob_FRontCard.SetActive(false);

            // Add onClick listener to the Button component of the card
            playerCard.GetComponent<Button>().onClick.AddListener(() => OnCardClick(playerCard));

            listCardPlayer.Add(playerCard);
            listCard.RemoveAt(rdPlayer);

            audioSource.clip = dealSound;
            audioSource.Play();

            // Distribute card to AI
            int rdAI = Random.Range(0, listCard.Count);
            GameObject AICard = listCard[rdAI];
            AICard.transform.SetParent(arr_Tf_AI[i % 13], true);
            iTween.MoveTo(AICard,
                iTween.Hash("position", arr_Tf_AI[i % 13].position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            iTween.RotateBy(AICard,
                iTween.Hash("x", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            yield return new WaitForSeconds(0.25f);
            AICard.GetComponent<UI>().Gob_FRontCard.SetActive(true);
            listCardAI.Add(AICard);
            listCard.RemoveAt(rdAI);


            int rdAI1 = Random.Range(0, listCard.Count);
            GameObject AI1Card = listCard[rdAI1];
            AI1Card.transform.SetParent(arr_Tf_AI1[i % 13], true);
            iTween.MoveTo(AI1Card,
                iTween.Hash("position", arr_Tf_AI1[i % 13].position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            iTween.RotateBy(AI1Card,
                iTween.Hash("x", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            yield return new WaitForSeconds(0.25f);
            AI1Card.GetComponent<UI>().Gob_FRontCard.SetActive(true);
            listCardAI1.Add(AI1Card);
            listCard.RemoveAt(rdAI1);

            int rdAI2 = Random.Range(0, listCard.Count);
            GameObject AI2Card = listCard[rdAI2];
            AI2Card.transform.SetParent(arr_Tf_AI2[i % 13], true);
            iTween.MoveTo(AI2Card,
                iTween.Hash("position", arr_Tf_AI2[i % 13].position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            iTween.RotateBy(AI2Card,
                iTween.Hash("x", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            yield return new WaitForSeconds(0.25f);
            AI2Card.GetComponent<UI>().Gob_FRontCard.SetActive(true);
            listCardAI2.Add(AI2Card);
            listCard.RemoveAt(rdAI2);
        }

        // Start the game round coroutine
        StartCoroutine(GameRound());
    }

 

    IEnumerator GameRound()
    {
        int rounds = 0;
        while (listCardPlayer.Count > 0)
        {
            yield return new WaitUntil(() => selectedCard != null);

            // Player plays a card
            GameObject playerCard = PlaySelectedCard(selectedCard, tf_PlayerPlayPosition);
            audioSource.clip = playSound;
            audioSource.Play();

            yield return new WaitForSeconds(5.0f);

            // AI 1 plays a card
            GameObject AICard = GetSmallestCard(listCardAI, tf_AIPlayPosition);
            AICard.GetComponent<UI>().Gob_FRontCard.SetActive(false);
            audioSource.clip = playSound;
            audioSource.Play();
            yield return new WaitForSeconds(5.0f);

            // AI 2 plays a card
            GameObject AI1Card = GetSmallestCard(listCardAI1, tf_AI1PlayPosition);
            AI1Card.GetComponent<UI>().Gob_FRontCard.SetActive(false);
            audioSource.clip = playSound;
            audioSource.Play();
            yield return new WaitForSeconds(5.0f);

            // AI 3 plays a card
            GameObject AI2Card = GetSmallestCard(listCardAI2, tf_AI2PlayPosition);
            AI2Card.GetComponent<UI>().Gob_FRontCard.SetActive(false);
            audioSource.clip = playSound;
            audioSource.Play();
            yield return new WaitForSeconds(5.0f);

            // Determine the winner of the round
            List<GameObject> cardsPlayed = new List<GameObject> { playerCard, AICard, AI1Card, AI2Card };

            // Check for null references
            if (cardsPlayed.Exists(card => card == null))
            {
                Debug.LogError("One of the cards played is null");
                yield break;
            }

            GameObject winningCard = DetermineWinningCard(cardsPlayed);

            // Add score to the winner and move cards to the winner
            if (winningCard == playerCard)
            {
                scorePlayer++;
                MoveCardsToWinner(cardsPlayed, arr_Tf_Player);
            }
            else if (winningCard == AICard)
            {
                scoreAI++;
                MoveCardsToWinner(cardsPlayed, arr_Tf_AI);
            }
            else if (winningCard == AI1Card)
            {
                scoreAI1++;
                MoveCardsToWinner(cardsPlayed, arr_Tf_AI1);
            }
            else if (winningCard == AI2Card)
            {
                scoreAI2++;
                MoveCardsToWinner(cardsPlayed, arr_Tf_AI2);
            }

            selectedCard = null;
            rounds++;
            txt_Round.text = "Round: " + rounds;

            if (rounds == 13)
            {
                DetermineGameWinner();
                yield break;
            }
        }

        yield return new WaitForSeconds(1.0f);
    }

    void DetermineGameWinner()
    {
        sp_WinPlayer.SetActive(false);
        sp_LostPlayer.SetActive(false);
        sp_WinAI.SetActive(false);
        sp_LostAI.SetActive(false);
        sp_WinAI1.SetActive(false);
        sp_LostAI1.SetActive(false);
        sp_WinAI2.SetActive(false);
        sp_LostAI2.SetActive(false);

        if (winCountPlayer > winCountAI && winCountPlayer > winCountAI1 && winCountPlayer > winCountAI2)
        {
            txt_GameResult.text =" You Win!";
            sp_WinPlayer.SetActive(true); 
        }
        else
        {
            txt_GameResult.text = "You   Lost";
            sp_LostPlayer.SetActive(true); 
        }

        if (winCountAI > winCountPlayer && winCountAI > winCountAI1 && winCountAI > winCountAI2)
        {
            txt_GameResult.text = "Winner: AI";
            sp_WinAI.SetActive(true); 
        }
        else
        {
            sp_LostAI.SetActive(true); 
        }

        if (winCountAI1 > winCountPlayer && winCountAI1 > winCountAI && winCountAI1 > winCountAI2)
        {
            txt_GameResult.text = "Winner: AI1";
            sp_WinAI1.SetActive(true); 
        }
        else
        {
            sp_LostAI1.SetActive(true); 
        }

        if (winCountAI2 > winCountPlayer && winCountAI2 > winCountAI && winCountAI2 > winCountAI1)
        {
            txt_GameResult.text = "Winner: AI2";
            sp_WinAI2.SetActive(true); 
        }
        else
        {
            sp_LostAI2.SetActive(true);
        }

        if ((winCountPlayer == winCountAI || winCountPlayer == winCountAI1 || winCountPlayer == winCountAI2) ||
            (winCountAI == winCountAI1 || winCountAI == winCountAI2) ||
            (winCountAI1 == winCountAI2))
        {
            txt_GameResult.text = "It's a tie!";
           
        }
        txt_GameResult.text = "RePlay";
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnCardClick(GameObject card)
    {
        if (selectedCard == null)
        {
            selectedCard = card;
            int cardValue = GetCardValue(card.GetComponent<UI>().GetCardName());
            Debug.Log("Player selected card with value: " + cardValue);
        }
    }

    GameObject PlaySelectedCard(GameObject card, Transform playPosition)
    {
        if (card == null)
        {
            Debug.LogError("Selected card is null");
            return null;
        }
        int cardValue = GetCardValue(card.GetComponent<UI>().img_card.sprite.name);
        Debug.Log("Player played card with value: " + cardValue);

        card.transform.SetParent(playPosition, true);
        iTween.MoveTo(card, iTween.Hash("position", playPosition.position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
        iTween.RotateBy(card, iTween.Hash("x", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
        listCardPlayer.Remove(card);

        return card;
    }

    //int GetCardValue(string cardName)
    //{
    //    string rankString = cardName.Substring(cardName.Length - 2);
    //    Debug.Log("dfsdsdfsd"+ rankString);
    //    int rank;

    //    if (int.TryParse(rankString, out rank))
    //    {
    //        Debug.Log(rank);

    //        return rank;
    //    }
    //    else
    //    {
    //        Debug.LogError("Invalid card rank: " + rankString);
    //        return 0;
    //    }
    //}
    int GetCardValue(string cardName)
    {
       
        Dictionary<string, int> suitValues = new Dictionary<string, int>
    {
        { "Club", 0 },
        { "Diamond", 1 },
        { "Heart", 2 },
        { "Spade", 3 }
    };

     
        string suit = cardName.Substring(0, cardName.Length - 2);
        string rankString = cardName.Substring(cardName.Length - 2);

        int rank;
        if (int.TryParse(rankString, out rank))
        {
            int suitValue;
            if (suitValues.TryGetValue(suit, out suitValue))
            {
           
                return suitValue * 13 + rank;
            }
            else
            {
                Debug.LogError("Invalid card suit: " + suit);
                return 0;
            }
        }
        else
        {
            Debug.LogError("Invalid card rank: " + rankString);
            return 0;
        }
    }

    GameObject GetSmallestCard(List<GameObject> cardList, Transform playPosition)
    {
        if (cardList == null || cardList.Count == 0)
        {
            Debug.LogError("Card list is null or empty");
            return null;
        }

        GameObject smallestCard = cardList[0];
        int smallestValue = GetCardValue(smallestCard.GetComponent<UI>().img_card.sprite.name);

        foreach (GameObject card in cardList)
        {
            int cardValue = GetCardValue(card.GetComponent<UI>().img_card.sprite.name);
            if (cardValue < smallestValue)
            {
                smallestCard = card;
                smallestValue = cardValue;
            }
        }

        cardList.Remove(smallestCard);
        smallestCard.transform.SetParent(playPosition, true);
        iTween.MoveTo(smallestCard, iTween.Hash("position", playPosition.position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
        iTween.RotateBy(smallestCard, iTween.Hash("x", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
        // smallestCard.GetComponent<UI>().Gob_FRontCard.SetActive(false);
        int playedCardValue = GetCardValue(smallestCard.GetComponent<UI>().GetCardName());
        Debug.Log("AI played card with value: " + playedCardValue);
        return smallestCard;

    }

    GameObject DetermineWinningCard(List<GameObject> cards)
    {
        if (cards == null || cards.Count == 0)
        {
            Debug.LogError("No cards to determine the winner.");
            return null;
        }

        GameObject winningCard = cards[0];
        int highestValue = GetCardValue(winningCard.GetComponent<UI>().img_card.sprite.name);

        foreach (GameObject card in cards)
        {
            int cardValue = GetCardValue(card.GetComponent<UI>().img_card.sprite.name);
            if (cardValue > highestValue)
            {
                winningCard = card;
                highestValue = cardValue;
            }
        }

        return winningCard;
    }


    //void MoveCardsToWinner(List<GameObject> cardsPlayed, Transform[] winnerTransform)
    //{
    //    for (int i = 0; i < cardsPlayed.Count; i++)
    //    {
    //        GameObject card = cardsPlayed[i];
    //        Transform targetTransform = null;

    //        // البحث عن أول موضع فارغ 
    //        for (int j = 0; j < winnerTransform.Length; j++)
    //        {
    //            if (winnerTransform[j].childCount == 0)
    //            {
    //               // targetTransform = winnerTransform[j];
    //                break;
    //            }
    //        }

    //        // إذا لم يتم العثور على أي موضع فارغ، قم بإنشاء موضع 
    //        if (targetTransform == null)
    //        {
    //            GameObject newTransformObj = new GameObject("NewCardPosition");
    //            newTransformObj.transform.SetParent(winnerTransform[0].parent, false); 
    //            targetTransform = newTransformObj.transform;


    //            targetTransform.localPosition = new Vector3(
    //                winnerTransform[0].localPosition.x + winnerTransform.Length * 1.5f, 
    //                winnerTransform[0].localPosition.y,
    //                winnerTransform[0].localPosition.z
    //            );


    //            List<Transform> tempList = new List<Transform>(winnerTransform);
    //            tempList.Add(targetTransform);
    //            winnerTransform = tempList.ToArray();
    //        }

    //        card.transform.SetParent(targetTransform, false);

    //        RectTransform rectTransform = card.GetComponent<RectTransform>();
    //        rectTransform.sizeDelta = new Vector2(100, 150);
    //        rectTransform.anchoredPosition = Vector2.zero;
    //        rectTransform.localScale = Vector3.one;

    //        iTween.MoveTo(card, iTween.Hash("position", targetTransform.position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
    //        iTween.RotateBy(card, iTween.Hash("x", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
    //        card.transform.LookAt(targetTransform.position);

    //        if (winnerTransform == arr_Tf_Player)
    //        {
    //            card.GetComponent<UI>().Gob_FRontCard.SetActive(false);

    //        }
    //        else
    //        {
    //            card.GetComponent<UI>().Gob_FRontCard.SetActive(true);
    //        }

    //        if (winnerTransform == arr_Tf_AI)
    //        {
    //            playerRoundScore++;

    //           // listCardAI.Add(card);
    //        }
    //        else if (winnerTransform == arr_Tf_AI1)
    //        {
    //           // listCardAI1.Add(card);
    //            aiRoundScore++;
    //        }
    //        else if (winnerTransform == arr_Tf_AI2)
    //        {
    //          //  listCardAI2.Add(card);
    //            ai1RoundScore++;

    //        }
    //        else if (winnerTransform == arr_Tf_Player)
    //        {
    //           // listCardPlayer.Add(card);
    //            ai2RoundScore++;

    //        }
    //    }
    //}

    void MoveCardsToWinner(List<GameObject> cardsPlayed, Transform[] winnerTransform)
    {
        if (winnerTransform == arr_Tf_Player)
        {
           // playerRoundScore += cardsPlayed.Count;
            winCountPlayer++;
            txt_WinCountPlayer.text = "" + winCountPlayer;
        }
        else if (winnerTransform == arr_Tf_AI)
        {
           // aiRoundScore += cardsPlayed.Count;
            winCountAI++;
            txt_WinCountAI.text = "" + winCountAI;
        }
        else if (winnerTransform == arr_Tf_AI1)
        {
           // ai1RoundScore += cardsPlayed.Count;
            winCountAI1++;
            txt_WinCountAI1.text = "" + winCountAI1;

        }
        else if (winnerTransform == arr_Tf_AI2)
        {
           // ai2RoundScore += cardsPlayed.Count;
            winCountAI2++;
            txt_WinCountAI2.text = "" + winCountAI2;  
        }

        foreach (var card in cardsPlayed)
        {
            Destroy(card);
        }
    }


    void AddCardToTransformArray(GameObject card, Transform[] playerTransforms)
    {
        for (int i = 0; i < playerTransforms.Length; i++)
        {
            if (playerTransforms[i].childCount == 0)
            {
                playerTransforms[i] = card.transform;
                break;
            }
        }
    }


    public void ResetGame()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //public void EqualScore()
    //{
    //    if (scorePlayer > scoreAI && scorePlayer > scoreAI1 && scorePlayer > scoreAI2)
    //    {
    //        img_Resuft.enabled = true;
    //        img_Resuft.sprite = Sp_Win;
    //    }
    //    else
    //    {
    //        img_Resuft.enabled = true;
    //        img_Resuft.sprite = sp_Lost;
    //    }
    //}
}


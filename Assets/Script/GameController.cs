
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class GameController : MonoBehaviour
//{
//    public GameObject Card;
//    public Transform tf_BaxCard;
//    public List<GameObject> listCard = new List<GameObject>();
//    public List<GameObject> listCardPlayer = new List<GameObject>();
//    public List<GameObject> listCardAI = new List<GameObject>();
//    public List<GameObject> listCardAI1 = new List<GameObject>();
//    public List<GameObject> listCardAI2 = new List<GameObject>();

//    public Transform[] arr_Tf_AI, arr_Tf_Player, arr_Tf_AI1, arr_Tf_AI2;

//    public Sprite Sp_Win, sp_Lost;
//    public Image img_Resuft;
//    public int scorePlayer, scoreAI, scoreAI1, scoreAI2;
//    public Text txt_ScorePlayer, txt_ScoreAI, txt_ScoreAI1, txt_ScoreAI2;

//    public Transform tf_PlayerPlayPosition;
//    public Transform tf_AIPlayPosition;
//    public Transform tf_AI1PlayPosition;
//    public Transform tf_AI2PlayPosition;

//    private int currentTurn = 0;
//    private GameObject selectedCard;

//    // Start is called before the first frame update
//    void Start()
//    {
//        InstanceCard();
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    public void InstanceCard()
//    {
//        for (int i = 0; i < SpriteGame.instance.arr_Sp_Cards.Length; i++)
//        {
//            GameObject _Card = Instantiate(Card, tf_BaxCard.position, Quaternion.identity);
//            _Card.transform.SetParent(tf_BaxCard, false);
//            _Card.GetComponent<UI>().img_card.sprite = SpriteGame.instance.arr_Sp_Cards[i];

//            // Add onClick listener to the Button component of the card
//            _Card.GetComponent<Button>().onClick.AddListener(() => OnCardClick(_Card));

//            listCard.Add(_Card);
//        }
//        StartCoroutine(SplitCard());
//    }

//    IEnumerator SplitCard()
//    {
//        scoreAI = 0;
//        scorePlayer = 0;
//        scoreAI1 = 0;
//        scoreAI2 = 0;

//        listCardPlayer.Clear();
//        listCardAI.Clear();
//        listCardAI1.Clear();
//        listCardAI2.Clear();

//        // Distribute 13 cards to each player
//        for (int i = 0; i < 13; i++)
//        {
//            yield return new WaitForSeconds(0.25f);

//            // Distribute card to Player
//            int rdPlayer = Random.Range(0, listCard.Count);
//            GameObject playerCard = listCard[rdPlayer];
//            playerCard.transform.SetParent(arr_Tf_Player[i % 13], true);
//            iTween.MoveTo(playerCard,
//                iTween.Hash("position", arr_Tf_Player[i % 13].position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
//            iTween.RotateBy(playerCard,
//                iTween.Hash("x", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
//            yield return new WaitForSeconds(0.25f);
//            playerCard.GetComponent<UI>().Gob_FRontCard.SetActive(false);

//            // Add onClick listener to the Button component of the card
//            playerCard.GetComponent<Button>().onClick.AddListener(() => OnCardClick(playerCard));

//            listCardPlayer.Add(playerCard);
//            listCard.RemoveAt(rdPlayer);

//            // Distribute card to AI
//            int rdAI = Random.Range(0, listCard.Count);
//            GameObject AICard = listCard[rdAI];
//            AICard.transform.SetParent(arr_Tf_AI[i % 13], true);
//            iTween.MoveTo(AICard,
//                iTween.Hash("position", arr_Tf_AI[i % 13].position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
//            iTween.RotateBy(AICard,
//                iTween.Hash("x", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
//            yield return new WaitForSeconds(0.25f);
//            AICard.GetComponent<UI>().Gob_FRontCard.SetActive(true);
//            listCardAI.Add(AICard);
//            listCard.RemoveAt(rdAI);


//            int rdAI1 = Random.Range(0, listCard.Count);
//            GameObject AI1Card = listCard[rdAI1];
//            AI1Card.transform.SetParent(arr_Tf_AI1[i % 13], true);
//            iTween.MoveTo(AI1Card,
//                iTween.Hash("position", arr_Tf_AI1[i % 13].position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
//            iTween.RotateBy(AI1Card,
//                iTween.Hash("x", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
//            yield return new WaitForSeconds(0.25f);
//            AI1Card.GetComponent<UI>().Gob_FRontCard.SetActive(true);
//            listCardAI1.Add(AI1Card);
//            listCard.RemoveAt(rdAI1);

//            int rdAI2 = Random.Range(0, listCard.Count);
//            GameObject AI2Card = listCard[rdAI2];
//            AI2Card.transform.SetParent(arr_Tf_AI2[i % 13], true);
//            iTween.MoveTo(AI2Card,
//                iTween.Hash("position", arr_Tf_AI2[i % 13].position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
//            iTween.RotateBy(AI2Card,
//                iTween.Hash("x", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
//            yield return new WaitForSeconds(0.25f);
//            AI2Card.GetComponent<UI>().Gob_FRontCard.SetActive(true);
//            listCardAI2.Add(AI2Card);
//            listCard.RemoveAt(rdAI2);
//        }

//        // Start the game round coroutine
//        StartCoroutine(GameRound());
//    }

//    IEnumerator GameRound()
//    {
//        while (listCardPlayer.Count > 0)
//        {
//            yield return new WaitUntil(() => selectedCard != null);

//            // Player plays a card
//            GameObject playerCard = PlaySelectedCard(selectedCard, tf_PlayerPlayPosition);

//            // Wait for 10 seconds before the next player plays
//            yield return new WaitForSeconds(5.0f);

//            // AI 1 plays a card
//            GameObject AICard = GetSmallestCard(listCardAI, tf_AIPlayPosition);

//            yield return new WaitForSeconds(5.0f);

//            // AI 2 plays a card
//            GameObject AI1Card = GetSmallestCard(listCardAI1, tf_AI1PlayPosition);

//            yield return new WaitForSeconds(5.0f);

//            // AI 3 plays a card
//            GameObject AI2Card = GetSmallestCard(listCardAI2, tf_AI2PlayPosition);

//            yield return new WaitForSeconds(5.0f);

//            // Determine the winner of the round
//            List<GameObject> cardsPlayed = new List<GameObject> { playerCard, AICard, AI1Card, AI2Card };

//            // Check for null references
//            if (cardsPlayed.Exists(card => card == null))
//            {
//                Debug.LogError("One of the cards played is null");
//                yield break;
//            }

//            GameObject winningCard = DetermineWinningCard(cardsPlayed);

//            if (winningCard == playerCard)
//            {
//                scorePlayer++;
//                txt_ScorePlayer.text = "Player Score: " + scorePlayer;
//            }
//            else if (winningCard == AICard)
//            {
//                scoreAI++;
//                txt_ScoreAI.text = "AI Score: " + scoreAI;
//            }
//            else if (winningCard == AI1Card)
//            {
//                scoreAI1++;
//                txt_ScoreAI1.text = "AI1 Score: " + scoreAI1;
//            }
//            else if (winningCard == AI2Card)
//            {
//                scoreAI2++;
//                txt_ScoreAI2.text = "AI2 Score: " + scoreAI2;
//            }

//            selectedCard = null; // Reset selected card
//        }

//        // End game
//        yield return new WaitForSeconds(1.0f);
//        EqualScore();
//    }


//    public void OnCardClick(GameObject card)
//    {
//        if (selectedCard == null)
//        {
//            selectedCard = card;
//        }
//    }

//    GameObject PlaySelectedCard(GameObject card, Transform playPosition)
//    {
//        if (card == null)
//        {
//            Debug.LogError("Selected card is null");
//            return null;
//        }

//        card.transform.SetParent(playPosition, true);
//        iTween.MoveTo(card, iTween.Hash("position", playPosition.position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
//        iTween.RotateBy(card, iTween.Hash("x", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
//        listCardPlayer.Remove(card);

//        return card;
//    }

//    int GetCardValue(string cardName)
//    {
//        string rankString = cardName.Substring(cardName.Length - 2); 
//        int rank;

//        if (int.TryParse(rankString, out rank))
//        {
//            return rank;
//        }
//        else
//        {
//            Debug.LogError("Invalid card rank: " + rankString);
//            return 0;
//        }
//    }

//    GameObject GetSmallestCard(List<GameObject> cardList, Transform playPosition)
//    {
//        if (cardList == null || cardList.Count == 0)
//        {
//            Debug.LogError("Card list is null or empty");
//            return null;
//        }

//        GameObject smallestCard = cardList[0];
//        int smallestValue = GetCardValue(smallestCard.GetComponent<UI>().img_card.sprite.name);

//        foreach (GameObject card in cardList)
//        {
//            int cardValue = GetCardValue(card.GetComponent<UI>().img_card.sprite.name);
//            if (cardValue < smallestValue)
//            {
//                smallestCard = card;
//                smallestValue = cardValue;
//            }
//        }

//        cardList.Remove(smallestCard);
//        smallestCard.transform.SetParent(playPosition, true);
//        iTween.MoveTo(smallestCard, iTween.Hash("position", playPosition.position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
//        iTween.RotateBy(smallestCard, iTween.Hash("x", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
//        smallestCard.GetComponent<UI>().Gob_FRontCard.SetActive(false);

//        return smallestCard;

//    }


//    GameObject DetermineWinningCard(List<GameObject> cards)
//    {
//        if (cards == null || cards.Count == 0)
//        {
//            Debug.LogError("No cards to determine the winner.");
//            return null;
//        }

//        GameObject winningCard = cards[0];
//        int lowestValue = GetCardValue(winningCard.GetComponent<UI>().img_card.sprite.name);

//        foreach (GameObject card in cards)
//        {
//            int cardValue = GetCardValue(card.GetComponent<UI>().img_card.sprite.name);
//            if (cardValue < lowestValue)
//            {
//                winningCard = card;
//                lowestValue = cardValue;
//            }
//        }

//        return winningCard;
//    }




//    public void ResetGame()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//    }

//    public void EqualScore()
//    {
//        if (scorePlayer > scoreAI && scorePlayer > scoreAI1 && scorePlayer > scoreAI2)
//        {
//            img_Resuft.enabled = true;
//            img_Resuft.sprite = Sp_Win;
//        }
//        else
//        {
//            img_Resuft.enabled = true;
//            img_Resuft.sprite = sp_Lost;
//        }
//    }
//}


//____________________________________________________________________________________________________________


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

    public Transform[] arr_Tf_AI, arr_Tf_Player, arr_Tf_AI1, arr_Tf_AI2;

    public Sprite Sp_Win, sp_Lost;
    public Image img_Resuft;
    public int scorePlayer, scoreAI, scoreAI1, scoreAI2;
    public Text txt_ScorePlayer, txt_ScoreAI, txt_ScoreAI1, txt_ScoreAI2;

    public Transform tf_PlayerPlayPosition;
    public Transform tf_AIPlayPosition;
    public Transform tf_AI1PlayPosition;
    public Transform tf_AI2PlayPosition;

    private int currentTurn = 0;
    private GameObject selectedCard;
    private int currentRound = 0;

    private AudioSource audioSource;
    // مرجع لـ AudioClip لصوت توزيع البطاقات
    public AudioClip dealSound;
    // مرجع لـ AudioClip لصوت رمي البطاقات
    public AudioClip playSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        InstanceCard();
    }



    public void InstanceCard()
    {
        for (int i = 0; i < SpriteGame.instance.arr_Sp_Cards.Length; i++)
        {
            GameObject _Card = Instantiate(Card, tf_BaxCard.position, Quaternion.identity);
            _Card.transform.SetParent(tf_BaxCard, false);
            _Card.GetComponent<UI>().img_card.sprite = SpriteGame.instance.arr_Sp_Cards[i];

            // Add onClick listener to the Button component of the card
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
                // txt_ScorePlayer.text = "Player Score: " + scorePlayer;


                MoveCardsToWinner(cardsPlayed, arr_Tf_Player);
            }
            else if (winningCard == AICard)
            {
                scoreAI++;
                // txt_ScoreAI.text = "AI Score: " + scoreAI;
                MoveCardsToWinner(cardsPlayed, arr_Tf_AI);

            }
            else if (winningCard == AI1Card)
            {
                scoreAI1++;
                // txt_ScoreAI1.text = "AI1 Score: " + scoreAI1;
                MoveCardsToWinner(cardsPlayed, arr_Tf_AI1);


            }
            else if (winningCard == AI2Card)
            {
                scoreAI2++;
                // txt_ScoreAI2.text = "AI2 Score: " + scoreAI2;
                MoveCardsToWinner(cardsPlayed, arr_Tf_AI2);


            }

            selectedCard = null;
            rounds++;
            txt_ScorePlayer.text = "Round: " + rounds;

            if (rounds == 13)
            {
                int minCards = Mathf.Min(listCardPlayer.Count, listCardAI.Count, listCardAI1.Count, listCardAI2.Count);

                if (listCardPlayer.Count == minCards)
                {
                    img_Resuft.enabled = true;
                    img_Resuft.sprite = Sp_Win;
                }
                else
                {
                    img_Resuft.enabled = true;
                    img_Resuft.sprite = sp_Lost;
                }

                yield break;
            }
        }



        yield return new WaitForSeconds(1.0f);
        //  EqualScore();
    }


    public void OnCardClick(GameObject card)
    {
        if (selectedCard == null)
        {
            selectedCard = card;
        }
    }

    GameObject PlaySelectedCard(GameObject card, Transform playPosition)
    {
        if (card == null)
        {
            Debug.LogError("Selected card is null");
            return null;
        }

        card.transform.SetParent(playPosition, true);
        iTween.MoveTo(card, iTween.Hash("position", playPosition.position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
        iTween.RotateBy(card, iTween.Hash("x", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
        listCardPlayer.Remove(card);

        return card;
    }

    int GetCardValue(string cardName)
    {
        string rankString = cardName.Substring(cardName.Length - 2);
        int rank;

        if (int.TryParse(rankString, out rank))
        {
            return rank;
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


    void MoveCardsToWinner(List<GameObject> cardsPlayed, Transform[] winnerTransform)
    {
        for (int i = 0; i < cardsPlayed.Count; i++)
        {
            GameObject card = cardsPlayed[i];
            Transform targetTransform = null;

            // البحث عن أول موضع فارغ في مصفوفة التحويلات الخاصة بالفائز
            for (int j = 0; j < winnerTransform.Length; j++)
            {
                if (winnerTransform[j].childCount == 0)
                {
                    targetTransform = winnerTransform[j];
                    break;
                }
            }

            // إذا لم يتم العثور على أي موضع فارغ، قم بإنشاء موضع جديد
            if (targetTransform == null)
            {
                GameObject newTransformObj = new GameObject("NewCardPosition");
                newTransformObj.transform.SetParent(winnerTransform[0].parent, false); // اجعلها جزءًا من نفس العنصر الأب
                targetTransform = newTransformObj.transform;

                // قم بترتيب الموضع الجديد بشكل مناسب
                targetTransform.localPosition = new Vector3(
                    winnerTransform[0].localPosition.x + winnerTransform.Length * 1.5f, // يمكنك تعديل هذه القيم لتناسب ترتيب البطاقات
                    winnerTransform[0].localPosition.y,
                    winnerTransform[0].localPosition.z
                );

                // أضف الموضع الجديد إلى مصفوفة التحويلات
                List<Transform> tempList = new List<Transform>(winnerTransform);
                tempList.Add(targetTransform);
                winnerTransform = tempList.ToArray();
            }

            card.transform.SetParent(targetTransform, false);

            RectTransform rectTransform = card.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 150);
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.localScale = Vector3.one;

            iTween.MoveTo(card, iTween.Hash("position", targetTransform.position, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            iTween.RotateBy(card, iTween.Hash("x", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            card.transform.LookAt(targetTransform.position);

            // جعل البطاقة مرئية أو مخفية بناءً على الفائز
            if (winnerTransform == arr_Tf_Player)
            {
                card.GetComponent<UI>().Gob_FRontCard.SetActive(false); // إظهار وجه البطاقة إذا كانت للاعب البشري
            }
            else
            {
                card.GetComponent<UI>().Gob_FRontCard.SetActive(true);
            }

            // إضافة البطاقة إلى القائمة المناسبة بناءً على الفائز
            if (winnerTransform == arr_Tf_AI)
            {
                listCardAI.Add(card);
            }
            else if (winnerTransform == arr_Tf_AI1)
            {
                listCardAI1.Add(card);
            }
            else if (winnerTransform == arr_Tf_AI2)
            {
                listCardAI2.Add(card);
            }
            else if (winnerTransform == arr_Tf_Player)
            {
                listCardPlayer.Add(card);
            }
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


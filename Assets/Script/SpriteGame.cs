using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpriteGame : MonoBehaviour
{
    public static SpriteGame instance;
    //  public List<Sprite> arr_Sp_Cards =new List<Sprite> ;
    public Sprite[] arr_Sp_Cards;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            DestroyImmediate(gameObject);
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        //for(int i = 0; i < arr_Sp_Cards.Length; i++)
        //{
        //    arr_Sp_Cards[i] = (Sprite)Resources.Load("resources");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 10;
    public bool takingAway = false;
    public GameObject inventoryUI1;
    public GameObject inventoryUI2;

    public static Timer instance;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        textDisplay.GetComponent<Text>().text = "Time left: 00:" + secondsLeft;
    }

    // Update is called once per frame
    void Update()
    {
        if (takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }
        if(secondsLeft == 0) 
        {
            if (inventoryUI1.activeSelf) 
            {
                inventoryUI1.SetActive(false);
                Cursor.visible = false;
            }
            if (inventoryUI2.activeSelf)
            {
                inventoryUI2.SetActive(false);
                Cursor.visible = false;
            }
            RoundManager.singleton.NextWorm();
            
        }
        
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;

        if (secondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "Time left: 00:0" + secondsLeft;
        }
        else
        {
            textDisplay.GetComponent<Text>().text = "Time left: 00:" + secondsLeft;
        }
        takingAway = false;
    }



}

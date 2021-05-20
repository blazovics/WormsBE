using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    public Text wText;

    void Update()
    {
        wText.text = SaveWinner.winner + " is the winner";    
    }
}

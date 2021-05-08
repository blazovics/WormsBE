using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public PlayerMovement[] worms;
    public  List<PlayerMovement> teamYellow = new List<PlayerMovement>();
    public List<PlayerMovement> teamBlue = new List<PlayerMovement>();
    public Transform wormCamera;

    public static RoundManager singleton;

    private int currentWorm;

    private int helper = 0;



    void Start()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }

        singleton = this;


        worms = GameObject.FindObjectsOfType<PlayerMovement>();
        wormCamera = Camera.main.transform;

        for (int i = 0; i < worms.Length; i++)
        {
            if (worms[i].teamColor == "blue")
            {
                teamBlue.Add(worms[i]); 
            }
            else
            {
                teamYellow.Add(worms[i]);
            }
        }


        for (int t = 0; t < worms.Length; t = t+2)
        {
            
            worms[t] = teamBlue[helper];
            worms[t].wormId = t;
            worms[t+1] = teamYellow[helper];
            worms[t+1].wormId = t+1;
            helper++;
        }
    }

    public void NextWorm()
    {
        StartCoroutine(NextWormCoroutine());
    }

    public IEnumerator NextWormCoroutine()
    {
        Timer.instance.takingAway = true;
        Timer.instance.secondsLeft = 11;

        var nextWorm = currentWorm + 1;
        currentWorm = -1;

        yield return new WaitForSeconds(1);

        currentWorm = nextWorm;
        if (currentWorm >= worms.Length)
        {
            currentWorm = 0;
        }

        Timer.instance.takingAway = false;
        /*wormCamera.SetParent(worms[currentWorm].transform);
        wormCamera.localPosition = Vector3.zero + Vector3.back * 10;*/
    }


    public bool IsMyTurn(int i)
    {
        return i == currentWorm;
    }
}

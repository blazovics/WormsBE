using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public PlayerMovement[] worms;
    public List<PlayerMovement> teamYellow = new List<PlayerMovement>();
    public List<PlayerMovement> teamBlue = new List<PlayerMovement>();
    public Transform wormCamera;

    public Weapon_Gun[] weapons;

    //public GameObject gameManager;

    InventoryT1 inventoryT1;
    InventoryT2 inventoryT2;

    public float lerpTimer;
    public float chipSpeed = 2f;
    public Image frontHealthBarBlue;
    public Image backHealthBarBlue;
    public TextMeshProUGUI healthTextBlue;


    public Image frontHealthBarYellow;
    public Image backHealthBarYellow;
    public TextMeshProUGUI healthTextYellow;

    private float healthTeamBlue;

    private float healthTeamYellow;

    private float maxHealth;

    public static RoundManager singleton;

    private int currentWorm;

    public int helper = 0;

    private bool isLoaded = false; 


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
                maxHealth += worms[i].health;
            }
            else
            {
                teamYellow.Add(worms[i]);
            }
        }

        for (int t = 0; t < worms.Length; t = t + 2)
        {

            worms[t] = teamBlue[helper];
            worms[t].wormId = t;
            worms[t + 1] = teamYellow[helper];
            worms[t + 1].wormId = t + 1;
            helper++;
        }

        frontHealthBarBlue.color = Color.blue;
        frontHealthBarYellow.color = Color.yellow;

        inventoryT1 = InventoryT1.instance;
        inventoryT2 = InventoryT2.instance;

        weapons = GameObject.FindObjectsOfType<Weapon_Gun>();

    }



    void Update()
    {
        if (!isLoaded)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                inventoryT1.add(weapons[i]);
                inventoryT2.add(weapons[i]);
                weapons[i].gameObject.SetActive(false);
            }
            isLoaded = true;
        }
        UpdateHealthUIBlue();
        UpdateHealthUIYellow();

    }



    public void NextWorm()
    {
        StartCoroutine(NextWormCoroutine());
    }

    public IEnumerator NextWormCoroutine()
    {

        var nextWorm = currentWorm + 1;
        currentWorm = -1;
        if (nextWorm >= worms.Length)
        {
            nextWorm = 0;
        }
        if (worms[nextWorm].health == 0)
        {
            currentWorm = nextWorm;
            NextWorm();
        }
        else 
        {
            Timer.instance.secondsLeft = 11;

            yield return new WaitForSeconds(1);
            currentWorm = nextWorm;

        }

        /*wormCamera.SetParent(worms[currentWorm].transform);
        wormCamera.localPosition = Vector3.zero + Vector3.back * 10;*/
    }


    public bool IsMyTurn(int i)
    {
        return i == currentWorm;
    }


    public void UpdateHealthUIBlue()
    {
        healthTeamBlue = 0;

        for (int i = 0; i < teamBlue.Count; i++)
        {
            healthTeamBlue += teamBlue[i].health;
        }

        float fillF = frontHealthBarBlue.fillAmount;
        float fillB = backHealthBarBlue.fillAmount;
        float hFraction = healthTeamBlue / maxHealth;
        if (fillB > hFraction)
        {

            frontHealthBarBlue.fillAmount = hFraction;
            backHealthBarBlue.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBarBlue.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBarBlue.color = Color.green;
            backHealthBarBlue.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBarBlue.fillAmount = Mathf.Lerp(fillF, backHealthBarBlue.fillAmount, percentComplete);
        }
        healthTextBlue.text = Mathf.Round(healthTeamBlue * 100 / maxHealth) + "%";
        
    }

    public void UpdateHealthUIYellow()
    {

        healthTeamYellow = 0;

        for (int i = 0; i < teamYellow.Count; i++)
        {
            healthTeamYellow += teamYellow[i].health;
        }


        float fillF = frontHealthBarYellow.fillAmount;
        float fillB = backHealthBarYellow.fillAmount;
        float hFraction = healthTeamYellow / maxHealth;
        if (fillB > hFraction)
        {

            frontHealthBarYellow.fillAmount = hFraction;
            backHealthBarYellow.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBarYellow.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBarYellow.color = Color.green;
            backHealthBarYellow.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBarYellow.fillAmount = Mathf.Lerp(fillF, backHealthBarYellow.fillAmount, percentComplete);
        }
        healthTextYellow.text = Mathf.Round(healthTeamYellow * 100 / maxHealth) + "%";

    }
}

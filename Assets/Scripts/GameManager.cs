using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private int Coins;
    [SerializeField]
    GameObject[] powerups ,places, limits,ballcount;
    [SerializeField]
    GameObject coin,GameUIs;
    [SerializeField]
    Playermove player;
    [HideInInspector] public bool escudo;
    [SerializeField] Sprite shieldImg;
    private int  score;
	[SerializeField] private Text scoretx,coinstext;
    private bool invisible;
   
    // Use this for initialization
    void Start()
    {
        Coins = PlayerPrefs.HasKey("Coins") ? PlayerPrefs.GetInt("Coins") : 0;

        invisible = false;        
        score = 0;
        limits = GameObject.FindGameObjectsWithTag("Limits");
        escudo = false;
       
        foreach (GameObject g in limits)
        {
            g.GetComponent<SpriteRenderer>().sprite = null;           
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("Coins", Coins);
        if (Coins>9999)
        {
            Coins = 0;
            PlayerPrefs.SetInt("Coins", 0);
        }
        coinstext.text = Coins.ToString();
        if (invisible)
        {
            player.GetComponent<SpriteRenderer>().enabled = false;
        }
        else player.GetComponent<SpriteRenderer>().enabled = true;
        ballcount = GameObject.FindGameObjectsWithTag("Ball");
        scoretx.text = score.ToString();
        if (escudo )
        {
            foreach (GameObject g in limits)
            {
                g.GetComponent<SpriteRenderer>().sprite = shieldImg;
            }

        }
        else
        {
            foreach (GameObject g in limits)
            {
                g.GetComponent<SpriteRenderer>().sprite = null;
            }

        }
        if (ballcount.Length==0&& player.start)
        {
            StartCoroutine(restartGame());
        }

    } 

    public int SetScore
    {
        get { return score; }
        set
        {
            if (value < 0) { score = score; }            
            else score = value;
        }
         }

    public void randPowerup()
    {
        float rand = Random.Range(0, 10f);

        if (rand <= 1f && score > 20)
        {
            if (getonLayer(9).Count < 1)
            {
                places = GameObject.FindGameObjectsWithTag("PowerUp");
                GameObject pw = Instantiate(powerups[Random.Range(0, powerups.Length)], places[Random.Range(0, places.Length)].transform.position, Quaternion.identity) as GameObject;
                Destroy(pw, 5);
            }
        }
        else if (rand<=1.8f)
        {
            places = GameObject.FindGameObjectsWithTag("PowerUp");
            GameObject c = Instantiate(coin, places[Random.Range(0, places.Length)].transform.position, Quaternion.identity) as GameObject;
            Destroy(c, 8);
        }
    }
    public IEnumerator SpeedUpPw()
    {
        Time.timeScale = 1.4f;
        BallMove.trailtx.SetColor("_TintColor", Color.blue);
        yield return new WaitForSeconds(6);
        BallMove.trailtx.SetColor("_TintColor", Color.white);
        Time.timeScale = 1;
    }
    public IEnumerator SpeedDownPw()
    {
        Time.timeScale = 0.5f;
        BallMove.trailtx.SetColor("_TintColor", Color.red);
        yield return new WaitForSeconds(6);
       BallMove.trailtx.SetColor("_TintColor", Color.white);
        Time.timeScale = 1;
    }
     IEnumerator restartGame()
    {
        yield return new WaitForSeconds(1);
        if (ballcount.Length == 0)
            Application.LoadLevel(Application.loadedLevel);
    }
    public IEnumerator turnInvisble()
     {
         invisible = true;
         Time.timeScale = 0.8f;
         yield return new WaitForSeconds(6);
         Time.timeScale = 1f;
         invisible = false;
     }
    public List<GameObject> getonLayer (int layer)
    {
        List<GameObject> tmep = new List<GameObject>();

        GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach(GameObject gb in gos)
        {
            if (gb.layer==layer)
            {
                tmep.Add(gb);
            }
        }
        return tmep;

    }
    public void updonw()
    {
        //FAZER
    }
    

    public int SetCoins
    {
        get { return Coins; }
        set
        {
            if (value > 0&& value<5)
            {
                Coins = value;
            }
        }
    }
    

}

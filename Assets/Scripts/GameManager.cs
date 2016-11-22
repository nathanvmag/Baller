using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private int Coins;
    public List<GameObject> powerups;
    [SerializeField]
    GameObject[] places, limits,ballcount;
    [SerializeField]
    GameObject coin,GameUIs,pausebt,pauseui,gamelose,gameplay;
    [SerializeField]
    Playermove player;
    [HideInInspector] public bool escudo;
    [SerializeField] Sprite shieldImg;
    private int  score;
	[SerializeField] private Text scoretx,coinstext,highscoretx;
    private bool invisible;
    int control;
    bool find;
    static bool paused;
    private int highscore ;
    bool washighscore = false;
    // Use this for initialization
    void Start()
    {

        washighscore = false;
        highscore = PlayerPrefs.HasKey("highscore") ? PlayerPrefs.GetInt("highscore") : 0;
        paused = false;        
        powerups = new List<GameObject>();
        find= true;
        Time.timeScale=1;    
        control = 1;        
        while (find)
        {
            if (Resources.Load("Prefab/PowerUps/Powerup" + control.ToString()) != null)     
            {
                powerups.Add(Resources.Load("Prefab/PowerUps/Powerup" + control.ToString()) as GameObject);
                control++;
            }
            else
            {
                find = false;                
            }
        }
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
        if (score>highscore)
        {
            washighscore = true;
            //PlayerPrefs.SetInt("highscore", score);
        }
        highscoretx.text = "Best: " +highscore.ToString();
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

        if (rand <= 3.5f && score > 15)
        {
            if (getonLayer(9).Count < 1)
            {
                places = GameObject.FindGameObjectsWithTag("PowerUp");
                GameObject pw = Instantiate(powerups[Random.Range(0, powerups.Count)], places[Random.Range(0, places.Length)].transform.position, Quaternion.identity) as GameObject;
                Destroy(pw, 7);
            }
        }
        else if (rand<=5.4f)
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
    public void homebt()
    {
        Application.LoadLevel(Application.loadedLevel);

    }
    
     IEnumerator restartGame()
    {
        if (washighscore) { PlayerPrefs.SetInt("highscore", score); }
        yield return new WaitForSeconds(1);
		if (ballcount.Length == 0){
          //  Application.LoadLevel(Application.loadedLevel);
			gamelose.SetActive(true);
			gameplay.SetActive (false);
		}
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
    public void pause()
    {
        paused = !paused;
        if (paused)
        {
            pauseui.SetActive(true);
            pausebt.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            pauseui.SetActive(false);
            pausebt.SetActive(true);
            Time.timeScale = 1;
        }
    }
    
    

    public int SetCoins
    {
        get { return Coins; }
        set
        {           
            Coins = value;           
        }

    } 

    
    
}

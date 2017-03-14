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
    GameObject coin,GameUIs,pausebt,pauseui,gamelose,gameplay,Diamondprefab,Showad;
    [SerializeField]
    Playermove player;
    [HideInInspector] public bool escudo;
    [SerializeField] Sprite shieldImg;
    private int  score;
	[SerializeField] private Text scoretx,coinstext,highscoretx,diamondsTx,price;
    private bool invisible;
    int control;
    bool find;
    static bool paused;
    private int highscore ;
    bool washighscore = false;
	Vector3 losegameposition;
	public static bool playagain;
	private int diamonds;
	int RevivePrice;
    // Use this for initialization
    void Awake()
    {
        if (Application.platform!= RuntimePlatform.Android || Application.platform!=RuntimePlatform.IPhonePlayer)
        {
            Resolution[] Resolutions= Screen.resolutions;
            foreach(Resolution r in Resolutions)
            {
                if (Mathf.Round( r.width/r.height)== 0.625f)
                {
                    Screen.SetResolution(r.width,r.height,true);
                }
            }
        }
    }
    void Start()
    {
		
		RevivePrice = 1;
		if (!PlayerPrefs.HasKey ("Diamonds")) {
			PlayerPrefs.SetInt ("Diamonds",5);
		}
		diamonds = PlayerPrefs.GetInt ("Diamonds");
		PlayerPrefs.DeleteKey ("OldSpeed");
		playagain = false;
		losegameposition = gamelose.GetComponent<RectTransform> ().position;
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
		//SetDiamonds = 20;
    }

    // Update is called once per frame
    void Update()
	{   
		
		if ((GameUIs.activeSelf) && player == null) {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Playermove> ();
		}
        if (score>highscore)
        {
            washighscore = true;
            //PlayerPrefs.SetInt("highscore", score);
        }
        highscoretx.text = ("Best " +highscore.ToString()).ToUpper();
        PlayerPrefs.SetInt("Coins", Coins);
        if (Coins>15000)
        {
            Coins = 0;
            PlayerPrefs.SetInt("Coins", 0);
         }
		diamondsTx.text = PlayerPrefs.GetInt ("Diamonds").ToString();
		if (diamonds != PlayerPrefs.GetInt ("Diamonds")) {
			PlayerPrefs.SetInt ("Diamonds", diamonds);
		}
		if (highscore > 1000) {
			highscore = 0;
			PlayerPrefs.SetInt ("highscore", highscore);
		}
        coinstext.text = Coins.ToString();
		if (invisible && player !=null)
        {
            player.GetComponent<SpriteRenderer>().enabled = false;
        }
		else if (player !=null)player.GetComponent<SpriteRenderer>().enabled = true;
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
		if ( (GameUIs.activeSelf)&& ballcount.Length==0&& player.start&&!playagain )
        {
			
            StartCoroutine(restartGame());
			playagain = true;

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
		if (rand > 7.1 && rand < 7.3) {
			places = GameObject.FindGameObjectsWithTag("PowerUp");
			GameObject d = Instantiate(Diamondprefab,places[Random.Range(0, places.Length)].transform.position, Quaternion.identity) as GameObject;
			Destroy (d, 5);
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
		PlayerPrefs.DeleteKey ("OldSpeed");

    }
    
     IEnumerator restartGame()
    {
        if (washighscore) { PlayerPrefs.SetInt("highscore", score); }
        yield return new WaitForSeconds(1);
		if (ballcount.Length == 0&& !player.starttutorial){
          //  Application.LoadLevel(Application.loadedLevel);
			StartCoroutine(loseanim());
            if (Random.Range(0,2)==1)
            {
                Showad.SetActive(true);
            }
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
            GameObject.Find("Canvas").GetComponent<Canvas>().sortingLayerName = "Ui";
        }
        else
        {
            pauseui.SetActive(false);
            pausebt.SetActive(true);
            Time.timeScale = 1;
            GameObject.Find("Canvas").GetComponent<Canvas>().sortingLayerName = "Default";
        }
    }
	IEnumerator loseanim()
	{
		gamelose.SetActive (true);
		price.text = RevivePrice.ToString ()+"x";
		GameObject.Find ("losescore").GetComponent<Text> ().text = "Score = " + score;
		while (Vector3.Distance (gamelose.GetComponent<RectTransform> ().position, new Vector3 (0, 0, 0)) > 0.05f) {
			gamelose.GetComponent<RectTransform> ().position = Vector3.MoveTowards (gamelose.GetComponent<RectTransform> ().position, new Vector3 (0, 0, 0), 20 * Time.deltaTime);
			yield return new WaitForSeconds (Time.deltaTime);
		}
		playagain = true;
		gameplay.SetActive (false);
	}
	public void PlayAgain()
	{
		if (diamonds >= RevivePrice) {
            SetDiamonds -= RevivePrice;
            RevivePrice = RevivePrice == 1 ? 2 : RevivePrice * 2;
			playagain = true;
			StartCoroutine (playAgain ());
            Showad.SetActive(false);
		}
	}
    public void playAgainAds(bool check)
    {
        playagain = true;
        if (check)
        {
            StartCoroutine(playAgain());
        }
        else { }
        Showad.SetActive(false);

    }
	IEnumerator playAgain()
	{
		
		while (Vector3.Distance (gamelose.GetComponent<RectTransform> ().position, losegameposition) > 0.05f) {
			gamelose.GetComponent<RectTransform> ().position = Vector3.MoveTowards (gamelose.GetComponent<RectTransform> ().position,losegameposition, 20 * Time.deltaTime);
			yield return new WaitForSeconds (Time.deltaTime);
		}
		gamelose.SetActive (false);
		gameplay.SetActive (true);
		player.gameObject.SetActive (true);
		
	}
    

    public int SetCoins
    {
        get { return Coins; }
        set
        {           
            Coins = value;           
        }

    } 
	public int SetDiamonds
	{
		get { return diamonds; }
		set
		{           
			diamonds = value;           
		}

	} 
	public void up()
	{
        if (touchV2.cima || touchV2.baixo || touchV2.esq || touchV2.dir) { }
        else       
		player.Up ();
	}
	public void down()
	{
        if (touchV2.cima || touchV2.baixo || touchV2.esq || touchV2.dir) { }
        else 
		player.Down ();
	}
	public void left()
	{
        if (touchV2.cima || touchV2.baixo || touchV2.esq || touchV2.dir) { }
        else 
		player.left ();
	}
	public void right()
	{
        if (touchV2.cima || touchV2.baixo || touchV2.esq || touchV2.dir) { }
        else 
		player.right ();
	}
    
    
}

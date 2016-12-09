using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Playermove : MonoBehaviour {
	Animator animator;
	[SerializeField] public GameObject[] Places ;
	int direction=0;
	private BoxCollider2D coll2d;
	public bool start;
	private GameObject bola;
	float speed =30;
	float anglespeed= 720;
    private bool crazyPlayer;
	public bool invert;
    [SerializeField]
    GameObject scoretx;
    public GameObject[] controls;
    string s = "Swipe or touch to move the player";
    string u = "Do not let the ball leave the screen";
    public bool starttutorial = false;
    [SerializeField]
    Sprite[] sprites;
	// Use this for initialization
	void Start () {
        scoretx = GameObject.FindGameObjectWithTag("sTx");
       
        starttutorial = PlayerPrefs.GetInt("TutorialTimes") > 1 ? false : true;
        Debug.Log(PlayerPrefs.GetInt("TutorialTimes"));
        crazyPlayer = false;
		invert = false;
		speed = 20;
		anglespeed = 720;
		direction = Random.Range (0, 4);
		animator = GetComponent<Animator> ();
		coll2d = GetComponent<BoxCollider2D> ();
		Places = GameObject.FindGameObjectsWithTag ("Places");
        controls = GameObject.FindGameObjectsWithTag("Controls");
        if (starttutorial)
        {
            StartCoroutine(tutorial());
        }
	}
	
	// Update is called once per frame
	void Update () {
       
		if (start) {
			try {			
				speed = (30 * 8) / 2.5f;
				anglespeed = (8 * anglespeed) / 2.5f;
			} catch {
			}
			if (Vector3.Distance (transform.position, Places [direction].transform.position) >= 0.1f || Vector3.Distance (transform.rotation.eulerAngles, Places [direction].transform.rotation.eulerAngles) >= 0.1f) {		
				transform.position = Vector3.MoveTowards (transform.position, Places [direction].transform.position, speed * Time.deltaTime);
				transform.rotation = Quaternion.RotateTowards (transform.rotation, Places [direction].transform.rotation, anglespeed * Time.deltaTime);
				coll2d.isTrigger = true;
			} else  {
				coll2d.isTrigger = false;
			}
			if (Input.GetKeyDown (KeyCode.UpArrow))
				Up ();
			 if (Input.GetKeyDown (KeyCode.LeftArrow))
				left ();
			 if (Input.GetKeyDown (KeyCode.RightArrow))
				right ();
			if (Input.GetKeyDown (KeyCode.DownArrow))
				Down ();
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    controlinput(false);
                }
            }
		
            if (touchV2.esq)
            {
                controlinput(false);
                left();
                touchV2.esq = false;
            }
            else if (touchV2.dir)
            {
                controlinput(false);
                right();
                touchV2.dir = false;
            }
            else if (touchV2.cima)
            {
                controlinput(false);
                Up();
                touchV2.cima = false;
            }
            else if (touchV2.baixo)
            {
                controlinput(false);
                Down();
                touchV2.baixo = false;
            }
            else if (touchV2.salvarposi) {
                controlinput(true);
            }
		}
	}

	public void Up()
	{
		if (crazyPlayer)
			direction = Random.Range (0, 4);
		else if (invert)
			direction = 1;
        else direction = 0;
	}
	public void Down()
	{
        if (crazyPlayer) direction = Random.Range(0, 4);
		else if (invert)
			direction = 0;
        else direction = 1;
	}
	public void left()
	{
        if (crazyPlayer) direction = Random.Range(0, 4);
		else if (invert)
			direction =3 ;
        else direction = 2;
	}
	public void right()
	{
        if (crazyPlayer) direction = Random.Range(0, 4);
		else if (invert)
			direction = 2;
        else direction = 3;

	}

	public void startgame()
	{
		start = true;
		animator.applyRootMotion = true;
		StartCoroutine (waitBall ());

	}
	IEnumerator waitBall ()
	{
        while (starttutorial)
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
		GameObject ball = Resources.Load ("Prefab/Game/Ball"+PlayerPrefs.GetInt("SelectedBall")) as GameObject;
		if (PlayerPrefs.HasKey ("OldSpeed")) {		
			
			bola = Instantiate (ball, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			bola.GetComponent<BallMove> ().direction = direction;
			yield return new WaitForSeconds (0.1f);
			bola.GetComponent<BallMove> ().speed =PlayerPrefs.GetFloat("OldSpeed")>3.2 ?  PlayerPrefs.GetFloat("OldSpeed") - 2 : 2;
		} else {
			yield return new WaitForSeconds (1);
			bola = Instantiate (ball, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			bola.GetComponent<BallMove> ().direction = direction;
		}
		GameManager.playagain = false;

	} 
	public IEnumerator ball()
	{
        if (GameObject.FindGameObjectsWithTag("Ball").Length < 3)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(waitBall());
        }
	}
   	int randomDirection(int actualdirection)
	{
		int rand = Random.Range (0, Places.Length);
		if (rand == actualdirection) {
			return randomDirection (actualdirection);
		} else
			return rand;
	}
    IEnumerator tutorial()
    {
       
        GameObject g = GameObject.FindGameObjectWithTag("Tutorial");
        g.GetComponent<Text>().text = s;
        StartCoroutine(flashing());
        yield return new WaitForSeconds(7);
        g.GetComponent<Text>().text = u;
        yield return new WaitForSeconds(1.5f);
        starttutorial = false;
        PlayerPrefs.SetInt("TutorialTimes", PlayerPrefs.GetInt("TutorialTimes") + 1);
        yield return new WaitForSeconds(2);
        g.GetComponent<Text>().text = "";
    } 
    IEnumerator flashing()
    {
        while (starttutorial)
        {
            foreach (GameObject item in controls)
            {
                item.GetComponent<Image>().sprite = sprites[1];
                item.GetComponent<Image>().color = new Color(item.GetComponent<Image>().color.r, item.GetComponent<Image>().color.b, item.GetComponent<Image>().color.g, Mathf.PingPong(Time.time/2, 0.6f));

            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        foreach (GameObject item in controls)
        {
            item.GetComponent<Image>().sprite = sprites[0];
            item.GetComponent<Image>().color = new Color(item.GetComponent<Image>().color.r, item.GetComponent<Image>().color.b, item.GetComponent<Image>().color.g,1);
        }

       
    }
    public IEnumerator crazyplayerpw ()
    {
        crazyPlayer = true;
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(4);
        Time.timeScale = 1;
        crazyPlayer = false;
    }
	public IEnumerator invertpw()
	{
		invert = true;
        scoretx.GetComponent<RectTransform>().localScale *= -1;
		yield return new WaitForSeconds (10f);
        scoretx.GetComponent<RectTransform>().localScale *= -1;
		invert = false;
	}
    void controlinput(bool value)
    {
        foreach (GameObject item in controls)
        {
            item.SetActive(value);
    
        }
    }
}


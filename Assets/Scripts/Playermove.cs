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
	// Use this for initialization
	void Start () {
        
        crazyPlayer = false;
		invert = false;
		speed = 20;
		anglespeed = 720;
		direction = Random.Range (0, 4);
		animator = GetComponent<Animator> ();
		coll2d = GetComponent<BoxCollider2D> ();
		Places = GameObject.FindGameObjectsWithTag ("Places");

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
		Debug.Log ("Aqui");
	}

	public void startgame()
	{
		start = true;
		animator.applyRootMotion = true;
		StartCoroutine (waitBall ());

	}
	IEnumerator waitBall ()
	{
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
}


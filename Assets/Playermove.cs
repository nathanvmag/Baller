using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Playermove : MonoBehaviour {
	Animator animator;
	[SerializeField] private Transform[] Places ;
	int direction=0;
	private BoxCollider2D coll2d;
	public bool start;
	private GameObject bola;
	float speed =30;
	float anglespeed= 720;
    
	// Use this for initialization
	void Start () {
       
		speed = 20;
		anglespeed = 720;
		direction = Random.Range (0, 4);
		animator = GetComponent<Animator> ();
		coll2d = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
       
		if (start) {
			try {			
				speed = (30 * bola.GetComponent<BallMove> ().speed) / 2.5f;
				anglespeed = (bola.GetComponent<BallMove> ().speed * anglespeed) / 2.5f;
			} catch {
			}
			if (Vector3.Distance (transform.position, Places [direction].position) >= 0.1f || Vector3.Distance (transform.rotation.eulerAngles, Places [direction].rotation.eulerAngles) >= 0.1f) {		
				transform.position = Vector3.MoveTowards (transform.position, Places [direction].position, speed * Time.deltaTime);
				transform.rotation = Quaternion.RotateTowards (transform.rotation, Places [direction].rotation, anglespeed * Time.deltaTime);
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
		direction = 0;
	}
	public void Down()
	{
		direction = 1;
	}
	public void left()
	{
		direction = 2;
	}
	public void right()
	{
		direction = 3;
	}

	public void startgame()
	{
		start = true;
		animator.applyRootMotion = true;
		StartCoroutine (waitBall ());

	}
	IEnumerator waitBall ()
	{
		GameObject ball = Resources.Load ("Prefab/Ball") as GameObject;
		yield return new WaitForSeconds (1);
		bola =Instantiate (ball, new Vector3 (0, 0, 0),Quaternion.identity) as GameObject;
		bola.GetComponent<BallMove> ().direction = direction;
       // bola.GetComponent<BallMove>().LookandGO(Places[direction].position, bola.GetComponent<BallMove>().speed,bola);

	}
   
   


	int randomDirection(int actualdirection)
	{
		int rand = Random.Range (0, Places.Length);
		if (rand == actualdirection) {
			return randomDirection (actualdirection);
		} else
			return rand;
	}
}


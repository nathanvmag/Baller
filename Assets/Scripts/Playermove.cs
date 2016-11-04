﻿using UnityEngine;
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
    private bool crazyPlayer;
    
	// Use this for initialization
	void Start () {
        crazyPlayer = false;
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
				speed = (30 * 8) / 2.5f;
				anglespeed = (8 * anglespeed) / 2.5f;
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
        if (crazyPlayer) direction = Random.Range(0, 4);
        else direction = 0;
	}
	public void Down()
	{
        if (crazyPlayer) direction = Random.Range(0, 4);
        else direction = 1;
	}
	public void left()
	{
        if (crazyPlayer) direction = Random.Range(0, 4);
        else direction = 2;
	}
	public void right()
	{
        if (crazyPlayer) direction = Random.Range(0, 4);
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
		GameObject ball = Resources.Load ("Prefab/Ball") as GameObject;
		yield return new WaitForSeconds (1);
		bola =Instantiate (ball, new Vector3 (0, 0,0),Quaternion.identity) as GameObject;
		bola.GetComponent<BallMove> ().direction = direction;

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
        Time.timeScale = 0.7f;
        yield return new WaitForSeconds(3);
        Time.timeScale = 1;
        crazyPlayer = false;
    }
}


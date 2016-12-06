﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
	[SerializeField] GameObject gameUI,Gameobjects,Gametitle,Gameplay,cointx,coin,hightx,Highscorebt,buttons,lobby,store,storeBt,diamond,diamondtx;
	[SerializeField] float animationspeed =10;
	float timer =0;
	// Use this for initialization
	void Start () {   
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
      

	}
	public void Startgame()
	{
		StartCoroutine (Animation ());
	}
    
	IEnumerator Animation ()
	{
		while (timer < 1) {
			Gametitle.GetComponent<RectTransform> ().position += Vector3.up * 5 * Time.deltaTime;
            coin.GetComponent<RectTransform>().position += Vector3.up * 2 * Time.deltaTime;
            cointx.GetComponent<RectTransform>().position += Vector3.up * 2 * Time.deltaTime;
			diamond.GetComponent<RectTransform>().position += Vector3.up * 2 * Time.deltaTime;
			diamondtx.GetComponent<RectTransform>().position += Vector3.up * 2 * Time.deltaTime;
			Gameplay.GetComponent<RectTransform> ().position += Vector3.up * -5 * Time.deltaTime;
            hightx.GetComponent<RectTransform>().position += Vector3.up * 5 * Time.deltaTime;
			Highscorebt.GetComponent<RectTransform>().position+=Vector3.up * -4 * Time.deltaTime;
			storeBt.GetComponent<RectTransform>().position+=Vector3.up * -4 * Time.deltaTime;
			timer += Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
        } yield return new WaitForSeconds(0.2f);
		gameUI.SetActive (true);
		Gameobjects.SetActive (true);
		gameObject.SetActive (false);
		GameObject g = Resources.Load ("Prefab/Player/Player "+PlayerPrefs.GetInt("SelectedPlayer").ToString())as GameObject;
		GameObject temp =Instantiate (g, new Vector2 (0,0),Quaternion.identity)as GameObject;
		temp.transform.parent = Gameobjects.transform;

	}
	IEnumerator highscoreanimation()
	{
		lobby.SetActive (true);
		while (Vector3.Distance(lobby.GetComponent<RectTransform> ().position,new Vector3(0,0,0))>0.05f) {
			//animate (Vector3.left, 5);
			buttons.GetComponent<RectTransform>().position = Vector3.MoveTowards(buttons.GetComponent<RectTransform>().position,new Vector3(-10,0,0),animationspeed*Time.deltaTime);
			lobby.GetComponent<RectTransform> ().position = Vector3.MoveTowards (lobby.GetComponent<RectTransform> ().position, new Vector3 (0, 0, 0), animationspeed * Time.deltaTime);

			yield return new WaitForSeconds (Time.deltaTime);		
		}
		buttons.SetActive (false);


	}
	IEnumerator backfromhighscore()
	{
		buttons.SetActive (true);

			while (Vector3.Distance(buttons.GetComponent<RectTransform>().position,new Vector3(0,0,0))>0.05f)
			{
				//animate(Vector3.right,animationspeed);
				buttons.GetComponent<RectTransform>().position = Vector3.MoveTowards(buttons.GetComponent<RectTransform>().position,new Vector3(0,0,0),animationspeed*Time.deltaTime);
				lobby.GetComponent<RectTransform>().position-=Vector3.right* -animationspeed *Time.deltaTime;
				yield return new WaitForSeconds(Time.deltaTime);
			}
			lobby.SetActive(false);

	}
	IEnumerator gotoStore()
	{
		store.SetActive (true);
		while (Vector3.Distance (store.GetComponent<RectTransform> ().position, new Vector3 (store.GetComponent<RectTransform> ().position.x, 0, 0)) > 0.3f) {
			store.GetComponent<RectTransform>().position =Vector3.MoveTowards(store.GetComponent<RectTransform>().position,new Vector3(store.GetComponent<RectTransform> ().position.x,0,0), animationspeed * Time.deltaTime);
			buttons.GetComponent<RectTransform> ().position += Vector3.up * animationspeed * Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		}
		buttons.SetActive (false);
	}
	IEnumerator backfromStore()
	{
		buttons.SetActive (true);
		while (Vector3.Distance (buttons.GetComponent<RectTransform> ().position, new Vector3 (0, 0, 0)) > 0.05f) {
			store.GetComponent<RectTransform>().position -=Vector3.up * animationspeed * Time.deltaTime;
			buttons.GetComponent<RectTransform>().position = Vector3.MoveTowards(buttons.GetComponent<RectTransform>().position,new Vector3(0,0,0),animationspeed*Time.deltaTime);
			yield return new WaitForSeconds (Time.deltaTime);
		}
		store.SetActive (false);

	}
	public void highscorebt()
	{
		StartCoroutine (highscoreanimation ());
	}
	public void backmenu()
	{
		StartCoroutine (backfromhighscore ());
	}
	public void storebt()
	{
		StartCoroutine (gotoStore ());
	}
	public void backstoretomenu()
	{
		StartCoroutine (backfromStore ());
	}
	void animate(Vector3 dire)
	{
		Gametitle.GetComponent<RectTransform> ().position += dire * animationspeed * Time.deltaTime;
		coin.GetComponent<RectTransform>().position += dire * animationspeed * Time.deltaTime;
		cointx.GetComponent<RectTransform>().position += dire * animationspeed * Time.deltaTime;
		Gameplay.GetComponent<RectTransform> ().position += dire * animationspeed * Time.deltaTime;
		hightx.GetComponent<RectTransform>().position += dire * animationspeed * Time.deltaTime;
		Highscorebt.GetComponent<RectTransform>().position+=dire * animationspeed * Time.deltaTime;
	}

}

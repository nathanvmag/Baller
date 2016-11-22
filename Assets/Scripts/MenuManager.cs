using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
	[SerializeField] GameObject gameUI,Gameobjects,Gametitle,Gameplay,cointx,coin,hightx,Highscorebt,buttons,lobby;
	float timer= 0;
	// Use this for initialization
	void Start () {
		timer= 0;
       
        
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
			Gameplay.GetComponent<RectTransform> ().position += Vector3.up * -5 * Time.deltaTime;
            hightx.GetComponent<RectTransform>().position += Vector3.up * -2 * Time.deltaTime;
			Highscorebt.GetComponent<RectTransform>().position+=Vector3.up * -4 * Time.deltaTime;
			timer += Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
        } yield return new WaitForSeconds(0.2f);
		gameUI.SetActive (true);
		Gameobjects.SetActive (true);
		gameObject.SetActive (false);
	}
	public void highscorebt()
	{
		buttons.SetActive (false);
		lobby.SetActive (true);
	}
	public void backmenu()
	{
		buttons.SetActive (true);
		lobby.SetActive (false);
	}
}

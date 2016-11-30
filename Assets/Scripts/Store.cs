using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class Store : MonoBehaviour {
	int counter =0;
	[SerializeField]GameObject controller,coinstx;
	[SerializeField]GameManager gm;
	public List<GameObject> Ballstosell;
	public List<int> Myballs;


	// Use this for initialization
	void Start () {		
		Ballstosell = new List<GameObject> ();
		Myballs = new List<int> ();
		counter = 0;
		gm.SetCoins = 400;
		if (!PlayerPrefs.HasKey ("SelectedBall")) {
			PlayerPrefs.SetInt ("SelectedBall", 0);
		}
		if (!PlayerPrefs.HasKey ("BuyBall0")) {
			PlayerPrefs.SetInt ("BuyBall0", 1);
		}


		foreach (Transform t in controller.transform) {			
			Ballstosell.Add (t.gameObject);
			Myballs.Add (0);
		}

		for (int i = 0; i < Myballs.Count; i++) {
			if (!PlayerPrefs.HasKey ("BuyBall" + i)) {
				PlayerPrefs.SetInt ("BuyBall" + i, 0);
			}
			Myballs [i] = PlayerPrefs.GetInt ("BuyBall" + i);
		}

		for (int i =0; i<Ballstosell.Count;i++)
		{
			if (PlayerPrefs.GetInt ("BuyBall" + i) == 1) {
				foreach (Transform t in Ballstosell[i].transform) {
					if (t.gameObject.name.Equals ("BuyButton")) {						
						t.gameObject.SetActive (false);
					} else if (t.gameObject.name.Equals ("Selected")) {
						t.gameObject.SetActive (false);						
					} else if (t.gameObject.name.Equals ("Select")) {
						t.gameObject.SetActive (true);						
					}					
				}
				if (i == PlayerPrefs.GetInt ("SelectedBall")) {
				
					foreach (Transform t in Ballstosell[i].transform) {
						if (t.gameObject.name.Equals ("BuyButton")) {
							t.gameObject.SetActive (false);
						} else if (t.gameObject.name.Equals ("Selected")) {
							t.gameObject.SetActive (true);						
						} else if (t.gameObject.name.Equals ("Select")) {
							t.gameObject.SetActive (false);						
						}
					}
				}


				}
			}
	
	}
	
	// Update is called once per frame
	void Update () {
		coinstx.GetComponent<Text> ().text = PlayerPrefs.GetInt ("Coins").ToString ();
		Debug.Log (PlayerPrefs.GetInt ("Coins"));
			
	}
	public void Left()
	{
		if (controller.GetComponent<RectTransform> ().localPosition.x != 0) {
			controller.GetComponent<RectTransform> ().localPosition =new Vector2(controller.GetComponent<RectTransform> ().localPosition.x+ 870,controller.GetComponent<RectTransform> ().localPosition.y);
		}
	}
	public void Right()
	{
		controller.GetComponent<RectTransform> ().localPosition = new Vector2 (controller.GetComponent<RectTransform> ().localPosition.x - 870, controller.GetComponent<RectTransform> ().localPosition.y);
	}
	public void BuyBt()
	{
		if (int.Parse (EventSystem.current.currentSelectedGameObject.transform.FindChild ("Value").gameObject.GetComponent<Text> ().text) <= PlayerPrefs.GetInt ("Coins")) {
			int temp = PlayerPrefs.GetInt ("Coins") - int.Parse (EventSystem.current.currentSelectedGameObject.transform.FindChild ("Value").gameObject.GetComponent<Text> ().text);
			PlayerPrefs.SetInt ("Coins", temp);
			gm.SetCoins = temp;
			int indice = int.Parse(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name.Substring(5,1));
			PlayerPrefs.SetInt ("BuyBall" + indice, 1);
			PlayerPrefs.SetInt ("SelectedBall", indice);
			UpdateStatus ();


		} else
			Debug.Log ("sem dinheiro");		
		}
	public void SelectBt()
	{
		int indice = int.Parse(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name.Substring(5,1));
		PlayerPrefs.SetInt ("SelectedBall", indice);
		UpdateStatus ();
	}
	public void UpdateStatus()
	{
		for (int i =0; i<Ballstosell.Count;i++)
		{
			if (PlayerPrefs.GetInt ("BuyBall" + i) == 1) {
				foreach (Transform t in Ballstosell[i].transform) {
					if (t.gameObject.name.Equals ("BuyButton")) {						
						t.gameObject.SetActive (false);
					} else if (t.gameObject.name.Equals ("Selected")) {
						t.gameObject.SetActive (false);						
					} else if (t.gameObject.name.Equals ("Select")) {
						t.gameObject.SetActive (true);						
					}					
				}
				if (i == PlayerPrefs.GetInt ("SelectedBall")) {

					foreach (Transform t in Ballstosell[i].transform) {
						if (t.gameObject.name.Equals ("BuyButton")) {
							t.gameObject.SetActive (false);
						} else if (t.gameObject.name.Equals ("Selected")) {
							t.gameObject.SetActive (true);						
						} else if (t.gameObject.name.Equals ("Select")) {
							t.gameObject.SetActive (false);						
						}
					}
				}


			}
		}
	}

}

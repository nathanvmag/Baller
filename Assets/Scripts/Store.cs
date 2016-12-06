using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class Store : MonoBehaviour {
	int counter =0;
	[SerializeField]GameObject controller,controller2,coinstx,Ballstore,Playerstore,openball,openplayer,diamondtx;
	[SerializeField]GameManager gm;
	public List<GameObject> Ballstosell;
	public List<int> Myballs;
	public List<GameObject>Playerstosell;
	public List<int>Myplayers;
	[SerializeField]Sprite diamond,coin;


	// Use this for initialization
	void Start () {	
		openball.GetComponent<Image> ().color = Color.cyan;	
		Ballstosell = new List<GameObject> ();
		Myballs = new List<int> ();
		Playerstosell = new List<GameObject> ();
		Myplayers = new List<int> ();
		counter = 0;
		//gm.SetCoins = 400;
		if (!PlayerPrefs.HasKey ("SelectedBall")) {
			PlayerPrefs.SetInt ("SelectedBall", 0);
		}
		if (!PlayerPrefs.HasKey ("BuyBall0")) {
			PlayerPrefs.SetInt ("BuyBall0", 1);
		}
		if (!PlayerPrefs.HasKey ("SelectedPlayer")) {
			PlayerPrefs.SetInt ("SelectedPlayer", 0);
		}
		if (!PlayerPrefs.HasKey ("BuyPlayer0")) {
			PlayerPrefs.SetInt ("BuyPlayer0", 1);
		}



		foreach (Transform t in controller.transform) {			
			Ballstosell.Add (t.gameObject);
			Myballs.Add (0);
		}
		foreach (Transform t in controller2.transform) {
			Playerstosell.Add (t.gameObject);
			Myplayers.Add (0);
		}

		for (int i = 0; i < Myballs.Count; i++) {
			if (!PlayerPrefs.HasKey ("BuyBall" + i)) {
				PlayerPrefs.SetInt ("BuyBall" + i, 0);
			}
			Myballs [i] = PlayerPrefs.GetInt ("BuyBall" + i);
		}
		for (int i = 0; i < Myplayers.Count; i++) {
			if (!PlayerPrefs.HasKey ("BuyPlayer" + i)) {
				PlayerPrefs.SetInt ("BuyPlayer" + i, 0);
			}
			Myplayers [i] = PlayerPrefs.GetInt ("BuyPlayer" + i);
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
		for (int i = 0; i < Playerstosell.Count; i++) {

			if (PlayerPrefs.GetInt ("BuyPlayer" + i) == 1) {
				
				foreach (Transform t in Playerstosell[i].transform) {
					if (t.gameObject.name.Equals ("BuyButton")) {						
						t.gameObject.SetActive (false);
					} else if (t.gameObject.name.Equals ("Selected")) {
						t.gameObject.SetActive (false);						
					} else if (t.gameObject.name.Equals ("Select")) {
						t.gameObject.SetActive (true);						
					}					
				}
				if (i == PlayerPrefs.GetInt ("SelectedPlayer")) {

					foreach (Transform t in Playerstosell[i].transform) {
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
		diamondtx.GetComponent<Text> ().text = PlayerPrefs.GetInt ("Diamonds").ToString ();
		if (touchV2.dir) {
			if (Ballstore.activeSelf && Ballstore.transform.parent.gameObject.activeSelf) {
				Left ();
			}
			if (Playerstore.activeSelf && Playerstore.transform.parent.gameObject.activeSelf) {
				Left2 ();
			}
			touchV2.dir = false;
		} else if (touchV2.esq) {
			if (Ballstore.activeSelf && Ballstore.transform.parent.gameObject.activeSelf) {
				Right ();
			}
			if (Playerstore.activeSelf && Playerstore.transform.parent.gameObject.activeSelf) {
				Right2 ();
			}
			touchV2.esq = false;
		
		}
	
	}
	public void Left()
	{
		if (controller.GetComponent<RectTransform> ().localPosition.x <-100) {
			controller.GetComponent<RectTransform> ().localPosition =new Vector2(controller.GetComponent<RectTransform> ().localPosition.x+ 870,controller.GetComponent<RectTransform> ().localPosition.y);
		}
	}
	public void Right()
	{
		controller.GetComponent<RectTransform> ().localPosition = new Vector2 (controller.GetComponent<RectTransform> ().localPosition.x - 870, controller.GetComponent<RectTransform> ().localPosition.y);
	}

	public void Left2()
	{
		if (controller2.GetComponent<RectTransform> ().localPosition.x <-100) {
			controller2.GetComponent<RectTransform> ().localPosition =new Vector2(controller2.GetComponent<RectTransform> ().localPosition.x+ 870,controller2.GetComponent<RectTransform> ().localPosition.y);
		}
	}
	public void Right2()
	{
		controller2.GetComponent<RectTransform> ().localPosition = new Vector2 (controller2.GetComponent<RectTransform> ().localPosition.x - 870, controller2.GetComponent<RectTransform> ().localPosition.y);
	}
	public void BuyBt()
	{
		if (EventSystem.current.currentSelectedGameObject.transform.FindChild ("Image").gameObject.GetComponent<Image> ().sprite == coin) { 
			if (int.Parse (EventSystem.current.currentSelectedGameObject.transform.FindChild ("Value").gameObject.GetComponent<Text> ().text) <= PlayerPrefs.GetInt ("Coins")) {
				int temp = PlayerPrefs.GetInt ("Coins") - int.Parse (EventSystem.current.currentSelectedGameObject.transform.FindChild ("Value").gameObject.GetComponent<Text> ().text);
				PlayerPrefs.SetInt ("Coins", temp);
				gm.SetCoins = temp;
				int indice = int.Parse (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name.Substring (5, 1));
				PlayerPrefs.SetInt ("BuyBall" + indice, 1);
				PlayerPrefs.SetInt ("SelectedBall", indice);
				UpdateStatus ();
			

			} else
				Debug.Log ("sem dinheiro");		
		}

		else if (EventSystem.current.currentSelectedGameObject.transform.FindChild ("Image").gameObject.GetComponent<Image> ().sprite == diamond) {
			if (int.Parse (EventSystem.current.currentSelectedGameObject.transform.FindChild ("Value").gameObject.GetComponent<Text> ().text) <= PlayerPrefs.GetInt ("Diamonds")) {
				int temp = PlayerPrefs.GetInt ("Diamonds") - int.Parse (EventSystem.current.currentSelectedGameObject.transform.FindChild ("Value").gameObject.GetComponent<Text> ().text);
				PlayerPrefs.SetInt ("Diamonds", temp);
				gm.SetDiamonds = temp;
				int indice = int.Parse (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name.Substring (5, 1));
				PlayerPrefs.SetInt ("BuyBall" + indice, 1);
				PlayerPrefs.SetInt ("SelectedBall", indice);
				UpdateStatus ();


			} else
				Debug.Log ("sem diamante");		
		}


	}
			
	public void BuyBtPlayer()
	{
		if (EventSystem.current.currentSelectedGameObject.transform.FindChild ("Image").gameObject.GetComponent<Image> ().sprite == coin) { 
			if (int.Parse (EventSystem.current.currentSelectedGameObject.transform.FindChild ("Value").gameObject.GetComponent<Text> ().text) <= PlayerPrefs.GetInt ("Coins")) {
				int temp = PlayerPrefs.GetInt ("Coins") - int.Parse (EventSystem.current.currentSelectedGameObject.transform.FindChild ("Value").gameObject.GetComponent<Text> ().text);
				PlayerPrefs.SetInt ("Coins", temp);
				gm.SetCoins = temp;
				int indice = int.Parse (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name.Substring (5, 1));
				PlayerPrefs.SetInt ("BuyPlayer" + indice, 1);
				PlayerPrefs.SetInt ("SelectedPlayer", indice);
				UpdateStatusPlayer ();
		
			} else
				Debug.Log ("Sem dinheiro");
		} else if (EventSystem.current.currentSelectedGameObject.transform.FindChild ("Image").gameObject.GetComponent<Image> ().sprite == diamond) {
			if (int.Parse (EventSystem.current.currentSelectedGameObject.transform.FindChild ("Value").gameObject.GetComponent<Text> ().text) <= PlayerPrefs.GetInt ("Diamonds")) {
				int temp = PlayerPrefs.GetInt ("Diamonds") - int.Parse (EventSystem.current.currentSelectedGameObject.transform.FindChild ("Value").gameObject.GetComponent<Text> ().text);
				PlayerPrefs.SetInt ("Diamonds", temp);
				gm.SetDiamonds = temp;
				int indice = int.Parse (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name.Substring (5, 1));
				PlayerPrefs.SetInt ("BuyPlayer" + indice, 1);
				PlayerPrefs.SetInt ("SelectedPlayer", indice);
				UpdateStatusPlayer ();
			}
		}		
	}
	public void SelectBt()
	{
		int indice = int.Parse(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name.Substring(5,1));
		PlayerPrefs.SetInt ("SelectedBall", indice);
		UpdateStatus ();
	}
	public void SelectBtPlayer()
	{
		int indice = int.Parse(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name.Substring(5,1));
		PlayerPrefs.SetInt ("SelectedPlayer", indice);
		UpdateStatusPlayer ();
	}
	public void OpenBallStore()
	{
		Ballstore.SetActive (true);
		Playerstore.SetActive (false);
		openball.GetComponent<Image> ().color = Color.cyan;
		openplayer.GetComponent<Image> ().color = Color.white;
	}
	public void OpenPlayerStore()
	{
		Ballstore.SetActive (false);
		Playerstore.SetActive (true);
		openball.GetComponent<Image> ().color = Color.white;
		openplayer.GetComponent<Image> ().color = Color.cyan;
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
	public void UpdateStatusPlayer()
	{
		for (int i = 0; i < Playerstosell.Count; i++) {

			if (PlayerPrefs.GetInt ("BuyPlayer" + i) == 1) {

				foreach (Transform t in Playerstosell[i].transform) {
					if (t.gameObject.name.Equals ("BuyButton")) {						
						t.gameObject.SetActive (false);
					} else if (t.gameObject.name.Equals ("Selected")) {
						t.gameObject.SetActive (false);						
					} else if (t.gameObject.name.Equals ("Select")) {
						t.gameObject.SetActive (true);						
					}					
				}
				if (i == PlayerPrefs.GetInt ("SelectedPlayer")) {

					foreach (Transform t in Playerstosell[i].transform) {
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

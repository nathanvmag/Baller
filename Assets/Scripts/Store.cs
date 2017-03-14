using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class Store : MonoBehaviour {
	int counter =0;
	[SerializeField]GameObject controller,controller2,coinstx,Ballstore,Playerstore,openball,openplayer,diamondtx,noCash,buycashWithDiamond,btcashfordiamond;
	[SerializeField]GameManager gm;
	public List<GameObject> Ballstosell;
	public List<int> Myballs;
	public List<GameObject>Playerstosell;
	public List<int>Myplayers;
	[SerializeField]Sprite diamond,coin;
	public int ballindice,playerindice;

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

        ballindice = Mathf.Abs(Mathf.RoundToInt(Mathf.Round(controller.GetComponent<RectTransform>().localPosition.x / 872)));
		playerindice =Mathf.Abs( Mathf.RoundToInt( Mathf.Round( controller2.GetComponent<RectTransform> ().localPosition.x / 872)));
		
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
		if (ballindice!=0) {
			controller.GetComponent<RectTransform> ().localPosition =new Vector2(controller.GetComponent<RectTransform> ().localPosition.x+ 872,controller.GetComponent<RectTransform> ().localPosition.y);
		}
	}
	public void Right()
	{
		if (ballindice!=controller.transform.childCount-1){
		controller.GetComponent<RectTransform> ().localPosition = new Vector2 (controller.GetComponent<RectTransform> ().localPosition.x - 872, controller.GetComponent<RectTransform> ().localPosition.y);
	}
	}
	public void Left2()
	{
		if (playerindice!=0) {
			controller2.GetComponent<RectTransform> ().localPosition =new Vector2(controller2.GetComponent<RectTransform> ().localPosition.x+ 872,controller2.GetComponent<RectTransform> ().localPosition.y);
		}
	}
	public void Right2()
	{
		if (playerindice != controller2.transform.childCount - 1) {
			controller2.GetComponent<RectTransform> ().localPosition = new Vector2 (controller2.GetComponent<RectTransform> ().localPosition.x - 872, controller2.GetComponent<RectTransform> ().localPosition.y);
		}
	}
	public void BuyBt()
	{
		if (EventSystem.current.currentSelectedGameObject.transform.FindChild ("Image").gameObject.GetComponent<Image> ().sprite == coin) {
            if (int.Parse(EventSystem.current.currentSelectedGameObject.transform.FindChild("Value").gameObject.GetComponent<Text>().text) <= PlayerPrefs.GetInt("Coins"))
            {
                int temp = PlayerPrefs.GetInt("Coins") - int.Parse(EventSystem.current.currentSelectedGameObject.transform.FindChild("Value").gameObject.GetComponent<Text>().text);
                PlayerPrefs.SetInt("Coins", temp);
                gm.SetCoins = temp;
                int indice = int.Parse(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name.Substring(5, 1));
                PlayerPrefs.SetInt("BuyBall" + indice, 1);
                PlayerPrefs.SetInt("SelectedBall", indice);
                UpdateStatus();


            }
            else
            {
                noCash.SetActive(true);
            }		
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


            }else 
            {
                noCash.SetActive(true);
            }
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
        buycashWithDiamond.SetActive(false);
		openball.GetComponent<Image> ().color = Color.cyan;
		openplayer.GetComponent<Image> ().color = Color.white;
        btcashfordiamond.GetComponent<Image>().color = Color.white;
	}
	public void OpenPlayerStore()
	{
        buycashWithDiamond.SetActive(false);
		Ballstore.SetActive (false);
		Playerstore.SetActive (true);
		openball.GetComponent<Image> ().color = Color.white;
		openplayer.GetComponent<Image> ().color = Color.cyan;
        btcashfordiamond.GetComponent<Image>().color = Color.white;
	}
    public void OpenCashWithDiamond()
    {
        buycashWithDiamond.SetActive(true);
        Ballstore.SetActive(false);
        Playerstore.SetActive(false);
        openball.GetComponent<Image>().color = Color.white;
        openplayer.GetComponent<Image>().color = Color.white;
        btcashfordiamond.GetComponent<Image>().color = Color.cyan;
    }
    public void BuyCashWithDiamonds()
    {   int price= 0;
        int WinCoin = 0;
    int indice = int.Parse(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name.Substring(5, 1));
        switch (indice)
        {
            case 0:                 
                price= 20;
                WinCoin = 50;
                break;
                 case 1:                 
                price= 100;
                WinCoin = 200;
                break;
                 case 2:                 
                price= 150;
                WinCoin = 500;
                break;
                 case 3:
                WinCoin = 1000;
                price= 300;
                break;
                 case 4:
                WinCoin = 2500;
                price= 500;
                break;
                 case 5:                 
                price= 700;
                WinCoin = 3500;
                break;
                 case 6:
                price = 1000;
                WinCoin = 5000;
                break;    
        }
        Debug.Log(price);
        if (price <= PlayerPrefs.GetInt("Diamonds"))
        {
           
            gm.SetCoins +=  WinCoin;
            gm.SetDiamonds -= price;
        }
        else noCash.SetActive(true);
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
    public void closeNoCash()
    {
        noCash.SetActive(false);
    }public void buyMore()
    {
        noCash.SetActive(false);
        GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>().buyMorediamonds();
    }

}

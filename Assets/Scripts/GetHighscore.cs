using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class GetHighscore : MonoBehaviour {
	public string[,] Dados;
	public GameObject[] Names,Highscores;
	[SerializeField] GameObject error,hslobby;
	WWWForm form;
	int control;
	// Use this for initialization
	void Start () {
        
		 form = new WWWForm ();
		control = 0;
		form.AddField ("serviceid", "392");
		StartCoroutine (site ("http://stormide.pe.hu/services.php"));
		Names = GameObject.FindGameObjectsWithTag ("Name");
		Highscores = GameObject.FindGameObjectsWithTag ("Highscore");
		foreach (GameObject g in Names) {
			g.name = "Nome" + (control).ToString();
			control++;
		}
		control = 0;
		foreach (GameObject g in Highscores) {
			g.name = "Highscore" + ( control).ToString();
			control++;
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void reconect()
	{
		StartCoroutine (site ("http://stormide.pe.hu/services.php"));
		hslobby.SetActive (true);
	}

	IEnumerator site(string url) {
		WWW www = new WWW(url,form);
		yield return www;
        Debug.Log(www.text);
		if (www.text != "") {			
			List<string> first = new List<string> ();
			first.AddRange (www.text.Split (' '));	
			
            int t = first.Count - 1;
            if (t > 10) t = 10;
            Dados = new string[t, 2];
			for (int i = 0; i < t; i++) {
				string[] split = first [i].Split ('|');
                
				Dados [i, 0] = split [0];
				Dados [i, 1] = split [1];
			}
			for (int i = 0; i < Dados.GetLength (0); i++) {
                GameObject.Find("Nome" + i).GetComponent<Text>().text = Dados[i, 0].ToUpper() ;
				GameObject.Find ("Highscore" + i).GetComponent<Text> ().text = Dados [i, 1];
			}
			error.SetActive (false);
		} else {
			
			error.SetActive (true);
			hslobby.SetActive (false);
		}

	}
}

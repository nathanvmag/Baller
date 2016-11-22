using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GetHighscore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (site ("http://stormide.pe.hu/teste.php"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator site(string url) {
		WWW www = new WWW(url);
		yield return www;
		if (www.text != "") {
			List<string> first = new List<string> ();
			first.AddRange (www.text.Split (' '));
			foreach (string s in first) {
				Debug.Log (s.Length);
			}
			Debug.Log ("correto");
		}
		else 
		Debug.Log ("Error ao conectar");
	}
}

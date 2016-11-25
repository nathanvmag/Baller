using UnityEngine;
using System.Collections;

public class SendHighscore : MonoBehaviour {
	WWWForm form;
	bool canploat;
	// Use this for initialization
	void Start () {
		form = new WWWForm ();
		canploat = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (canploat && PlayerPrefs.HasKey ("MYID")) {
			StartCoroutine (site ("http://stormide.pe.hu/services.php"));
			canploat = false;		
		}
	
	}
	IEnumerator site(string url)
	{
		form.AddField ("ID", PlayerPrefs.GetString ("MYID"));
		form.AddField ("Score", PlayerPrefs.GetInt ("highscore").ToString ());
		form.AddField ("serviceid", "721");
		WWW www = new WWW (url, form);
		yield return www;
		Debug.Log (www.text);
		if (!www.text.Equals ("sucess")) {
			yield return new WaitForSeconds (10);
			canploat = true;
		}
	}
}

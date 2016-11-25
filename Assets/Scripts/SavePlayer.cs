using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SavePlayer : MonoBehaviour {
    WWWForm form;
    string MYID;
    string playename;
    [SerializeField]
    GameObject inputname, okbutton;
    [SerializeField]
    InputField nameinput;
	// Use this for initialization
	void Start () {
		 //PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("playername"))
        {
            inputname.SetActive(true);
        }
        else inputname.SetActive(false);
        MYID = PlayerPrefs.HasKey("MYID") ? PlayerPrefs.GetString("MYID") : "";
        if (!PlayerPrefs.HasKey("MYID")&&PlayerPrefs.HasKey("playername"))
        {
            form = new WWWForm();
            form.AddField("serviceid", "33");
            form.AddField("player", PlayerPrefs.GetString("playername"));
            StartCoroutine(site("http://stormide.pe.hu/services.php"));
           
        }
        else if (PlayerPrefs.HasKey("MYID")) Debug.Log("Ja tenho id e é " + PlayerPrefs.GetString("MYID"));       
	}
	
	// Update is called once per frame
	void Update () {
        if (!string.IsNullOrEmpty(nameinput.text.Trim()))
        {
            okbutton.SetActive(true);
        }
        else okbutton.SetActive(false);
	  
	}
    IEnumerator site(string url)
    {
        WWW www = new WWW(url, form);
        yield return www;
        int e = 0;
        if (!string.IsNullOrEmpty(www.text.Trim())&&int.TryParse(www.text,out e))
        {
            MYID = www.text;
            Debug.Log("meu id " + MYID);
            PlayerPrefs.SetString("MYID", MYID);
            Debug.Log("Cadastrado com sucesso");
        }
        else
        {
            Debug.Log("tentei mas to sem net");
            StartCoroutine(tryagain());
        }
    }
    public void Okbutton()
    {
        PlayerPrefs.SetString("playername", nameinput.text);
         form = new WWWForm();
            form.AddField("serviceid", "33");
            form.AddField("player", PlayerPrefs.GetString("playername"));
            StartCoroutine(site("http://stormide.pe.hu/services.php"));
		StartCoroutine (okanim ());

    }
    IEnumerator tryagain()
    {
        yield return new WaitForSeconds(20);
        StartCoroutine(site("http://stormide.pe.hu/services.php"));
    }
  
	IEnumerator okanim()
	{
		float timer = 0;
		while (timer < 1) {
			timer += Time.deltaTime;
			inputname.GetComponent<RectTransform> ().position += Vector3.up * 15 * Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		}
		inputname.SetActive (false);
	}
}


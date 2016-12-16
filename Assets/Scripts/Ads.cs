using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements; 
public class Ads : MonoBehaviour {
   private string
        androidGameId = "1234590",
        iosGameId = "1234591";

   bool a= true ;
	// Use this for initialization
   void Start()
   {
       string gameId = androidGameId;
       if (Application.platform==RuntimePlatform.Android)
       {
           gameId = androidGameId;
       } if (Application.platform == RuntimePlatform.IPhonePlayer)
       {
           gameId = iosGameId;
       }
            
       if (Advertisement.isSupported && !Advertisement.isInitialized)
       {
           Advertisement.Initialize(gameId);
       }
   }
	
	// Update is called once per frame
	void Update () {
	        
	}
    public void ShowAd(string s)
    {
        StartCoroutine(ShowAdWhenReady(s));
    }
    IEnumerator ShowAdWhenReady(string s)
    {
        while (!Advertisement.IsReady())
            yield return null;
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Advertisement.Show();
        }
        else Advertisement.Show();
    }
    public void SeeAdForLife()
    {
        StartCoroutine(ShowRewardAds());  
    }
    IEnumerator ShowRewardAds()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;
        while (!Advertisement.IsReady())
            yield return null;
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Advertisement.Show("",options);
        }
        else Advertisement.Show("",options);

    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Camera.main.GetComponent<GameManager>().playAgainAds(true);
                break;
            case ShowResult.Skipped:
                Camera.main.GetComponent<GameManager>().playAgainAds(false);
                break;
            case ShowResult.Failed:
                Camera.main.GetComponent<GameManager>().playAgainAds(false);
                
                break;
        }
    }
}

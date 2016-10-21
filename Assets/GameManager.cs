using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] powerups ,places, limits,ballcount;
    [SerializeField]
    Playermove player;
    [HideInInspector] public bool escudo;
    [SerializeField] Sprite shieldImg;
    private int  score;
    private Text scoretx;
    private bool invisible;
    // Use this for initialization
    void Start()
    {
        invisible = false;
        scoretx = GameObject.Find("ScoreTx").GetComponent<Text>();
        score = 0;
        limits = GameObject.FindGameObjectsWithTag("Limits");
        escudo = false;
        places = GameObject.FindGameObjectsWithTag("PowerUp");
        foreach (GameObject g in limits)
        {
            g.GetComponent<SpriteRenderer>().sprite = null;           
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (invisible)
        {
            player.GetComponent<SpriteRenderer>().enabled = false;
        }
        else player.GetComponent<SpriteRenderer>().enabled = true;
        ballcount = GameObject.FindGameObjectsWithTag("Ball");
        scoretx.text = score.ToString();
        if (escudo )
        {
            foreach (GameObject g in limits)
            {
                g.GetComponent<SpriteRenderer>().sprite = shieldImg;
            }

        }
        else
        {
            foreach (GameObject g in limits)
            {
                g.GetComponent<SpriteRenderer>().sprite = null;
            }

        }
        if (ballcount.Length==0&& player.start)
        {
            StartCoroutine(restartGame());
        }

    } 

    public int SetScore
    {
        get { return score; }
        set
        {
            if (value < 0) { score = score; }            
            else score = value;
        }
         }

    public void randPowerup()
    {
        float rand = Random.Range(0, 10f);
       
        if (rand <= 0.8f&& score>30)
        {
           GameObject pw=  Instantiate(powerups[Random.Range(0, powerups.Length)], places[Random.Range(0, places.Length)].transform.position, Quaternion.identity) as GameObject;
           Destroy(pw, 5);
        }
    }
    public IEnumerator SpeedUpPw()
    {
        Time.timeScale = 1.4f;
        yield return new WaitForSeconds(6);
        Time.timeScale = 1;
    }
    public IEnumerator SpeedDownPw()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(6);
        Time.timeScale = 1;
    }
     IEnumerator restartGame()
    {
        yield return new WaitForSeconds(1);
        if (ballcount.Length == 0)
            Application.LoadLevel(Application.loadedLevel);
    }
    public IEnumerator turnInvisble()
     {
         invisible = true;
         Time.timeScale = 0.8f;
         yield return new WaitForSeconds(6);
         Time.timeScale = 1f;
         invisible = false;
     }
}

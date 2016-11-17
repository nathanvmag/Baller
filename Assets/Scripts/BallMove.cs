using UnityEngine;
using System.Collections;

public class BallMove : MonoBehaviour {
	public int direction; 
	public float speed;
	[SerializeField] Transform[] places = new Transform[4];
	bool perdeu;
	GameObject player,Balltouch;
	Rigidbody2D rb;
    int randDire;
    public float offset;
    private bool teleport;    
	[SerializeField] GameObject particle;    
    GameObject[] balls;
    GameManager gm;
    static int mult;
    CameraShakeScript camerashake;
     bool death= false ;
    public static Material trailtx;
    static bool acelerometre;
    static bool taptap = false;
    bool mouseup, touchup = true;     
   	// Use this for initialization
	void Start ()
    {

        
        mouseup = true;
        touchup = true;
        taptap = false;
        trailtx = GetComponent<TrailRenderer>().material;
        acelerometre = false;
        death = false;
        camerashake = GetComponent<CameraShakeScript>();
        mult = 1;
        gm = Camera.main.GetComponent<GameManager>();
        teleport = false;
        rb = GetComponent<Rigidbody2D> ();
		perdeu = false;
        speed = 2.5f ;
		places [0] = GameObject.Find ("PlaceUp").transform;
		places [1] = GameObject.Find ("PlaceDown").transform;
		places [2] = GameObject.Find ("PlaceLeft").transform;
		places [3] = GameObject.Find ("PlaceRight").transform;
		player = GameObject.Find ("Player");
        Balltouch = Resources.Load("Prefab/Particles/BallTouch") as GameObject;
        LookandGO(places[direction].position, speed);      
	}
	
	// Update is called once per frame
    void Update()
    {
        GetComponent<TrailRenderer>().material = trailtx;
        balls = GameObject.FindGameObjectsWithTag("Ball");
        if (perdeu)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), 2 * Time.deltaTime);
            if (balls.Length == 1)
            {
                player.SetActive(false);

            }
            if (Vector3.Distance(transform.position, new Vector3(0, 0, 0)) <= 0.05f)
            {
                GameObject g = Instantiate(particle, transform.position, Quaternion.identity) as GameObject;
                Destroy(gameObject);
                Destroy(g, 5);

            }
        }
        else if (!acelerometre&&!taptap)
        {
            if (teleport)
            {
                randDire = randomDirection(direction);
                direction = randDire;
                speed = speed >= 8 ? speed : speed += 0.04f;
                LookandGO(places[direction].position, speed);
                teleport = false;
                gm.SetScore += mult * 1;
            }
          
        }
        else if (acelerometre)
        {
            float axisx = Input.acceleration.x;
            float axixy =  Input.acceleration.y;
            float multi = 2;
            rb.velocity = new Vector2(axisx * multi * speed, axixy * multi * speed);

        }
        else if (taptap)
        {
            if (Input.touchCount>0&& touchup)
            {
                
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                    if ((hit.collider!=null)&& hit.collider.tag == gameObject.tag)
                    {
                        if (touchup && mouseup)
                        {
                            moveaftertap();
                            Debug.Log("hey");
                            touchup = false;
                        }
                    }
                }
            }
            
            else if (Input.GetMouseButton(0))
            {
                if (mouseup)
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.mousePosition)), Vector2.zero);
                    if ((hit.collider != null) && hit.collider.tag == gameObject.tag)
                    {
                        if (touchup && touchup)
                        {
                            moveaftertap();
                            mouseup = false;
                            Debug.Log("he2y");
                        }
                    }

                }                
            }
            if (Input.touchCount==0)
            {
                mouseup = true;               
            }
            if (!Input.GetMouseButton(0))
            {
                touchup = true;                
            }
        }
      
       
      
      
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (!death)
            {
                gm.randPowerup();

                switch (direction)
                {
                    case 0:
                        transform.position = new Vector3(transform.position.x, coll.gameObject.transform.position.y) + new Vector3(0, -offset, 0);
                        break;
                    case 1:
                        transform.position = new Vector3(transform.position.x, coll.gameObject.transform.position.y) + new Vector3(0, offset, 0);
                        break;
                    case 2:
                        transform.position = new Vector3(coll.gameObject.transform.position.x, transform.position.y) + new Vector3(offset, 0, 0);
                        break;
                    case 3:
                        transform.position = new Vector3(coll.gameObject.transform.position.x, transform.position.y) + new Vector3(-offset, 0, 0);
                        break;
                }
                camerashake.Shake();
                GameObject g = Instantiate(Balltouch, transform.position, coll.transform.rotation) as GameObject;

                Destroy(g, 1);
                teleport = true;
            }
            else
            {
                rb.velocity = Vector3.zero;
                perdeu = true;
            }
        }
        if (coll.gameObject.tag == "Limits")
        {
            if (!death)
            {
                if (gm.escudo)
                {
                    StartCoroutine(ignorecolission());
                    teleport = true;
                }
                else
                {
                    rb.velocity = Vector3.zero;

                    perdeu = true;
                }
            }
            else teleport = true;
        }
    }
   

        
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag =="+ball")
        {
            StartCoroutine(player.GetComponent<Playermove>().ball());
            Destroy(coll.gameObject);          
        }
        if (coll.gameObject.tag=="SpeedUp")
        {            
            Destroy(coll.gameObject);
            StartCoroutine(gm.SpeedUpPw());
        }
        if (coll.gameObject.tag == "SpeedDown")
        {
            Destroy(coll.gameObject);
            StartCoroutine(gm.SpeedDownPw());
        }
        if (coll.gameObject.tag == "Escudo")
        {
            Destroy(coll.gameObject);
            gm.escudo = true;
        }
        if (coll.gameObject.tag == "Score2x")
        {
            Destroy(coll.gameObject);
            StartCoroutine(score2x());
        }
        if (coll.gameObject.tag == "Invisible")
        {
            Destroy(coll.gameObject);
            StartCoroutine(gm.turnInvisble());
        }
        if (coll.gameObject.tag == "crazyPlayer")
        {
            Destroy(coll.gameObject);
            StartCoroutine(player.GetComponent<Playermove>().crazyplayerpw());
        } if (coll.gameObject.tag == "flashing")
        {
            Destroy(coll.gameObject);
            StartCoroutine(flash());
        }
        if (coll.gameObject.tag == "death")
        {
            Destroy(coll.gameObject);
            StartCoroutine(deathPW());
        }
        if (coll.gameObject.tag == "coin")
        {
            Destroy(coll.gameObject);
            gm.SetCoins++;
        }
        if (coll.gameObject.tag == "Updown")
        {
            Destroy(coll.gameObject);
			StartCoroutine (player.GetComponent<Playermove> ().invertpw ());
		}
        if (coll.gameObject.tag == "Acelerometre")
        {
            Destroy(coll.gameObject);
            StartCoroutine(acelerometrepw());
        }
        if (coll.gameObject.tag == "TapTap")
        {
            Destroy(coll.gameObject);
            StartCoroutine(taptappw());
        }
        if (coll.gameObject.tag == "Troll")
        {
            Destroy(coll.gameObject);
            StartCoroutine(TrollPw());
        }
        if (coll.gameObject.tag == "Teleport")
        {
            Destroy(coll.gameObject);
            GameObject[] g = GameObject.FindGameObjectsWithTag("PowerUp");
            transform.position = g[Random.Range(0, g.Length)].transform.position;
            teleport = true;
        }
        if (coll.gameObject.tag == "Minuscle")
        {
            Destroy(coll.gameObject);
            StartCoroutine(minusclepw());
        }
        
        
        
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (!death)
            {
                switch (direction)
                {
                    case 0:
                        transform.position = new Vector3(transform.position.x, coll.gameObject.transform.position.y) + new Vector3(0, -offset, 0);
                        break;
                    case 1:
                        transform.position = new Vector3(transform.position.x, coll.gameObject.transform.position.y) + new Vector3(0, offset, 0);
                        break;
                    case 2:
                        transform.position = new Vector3(coll.gameObject.transform.position.x, transform.position.y) + new Vector3(offset, 0, 0);
                        break;
                    case 3:
                        transform.position = new Vector3(coll.gameObject.transform.position.x, transform.position.y) + new Vector3(-offset, 0, 0);
                        break;
                }
                camerashake.Shake();
                GameObject g = Instantiate(Balltouch, transform.position, coll.transform.rotation) as GameObject;


                Destroy(g, 1);
                teleport = true;
            }
            else 
            {rb.velocity= Vector3.zero;
                perdeu = true;
         }
        }
        if (coll.gameObject.tag == "Limits")
        {
            if (!death){
            rb.velocity = Vector3.zero;
            perdeu = true;
            }

            else teleport = true ;

        }
        }
    void moveaftertap()
    {
        randDire = randomDirection(direction);
        direction = randDire;
        speed = speed >= 8 ? speed : speed += 0.04f;
        LookandGO(places[direction].position, speed);        
       // gm.SetScore += mult * 1;
    }
        
     
    
	public void LookandGO(Vector3 positiontolook,float speed)
	{
		Vector3 diff = positiontolook -transform.position;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0,0, rot_z - 90);
        rb.velocity =transform.up * speed;
      
	}
    public Quaternion Look(Vector3 positiontolook,Transform eu )
    {
        Vector3 diff = positiontolook - eu.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
       return Quaternion.Euler(0, 0, rot_z + 90);       
    }

    int randomDirection(int actualdirection)
    {
        int rand = Random.Range(0, places.Length);
        if (rand == actualdirection)
        {
            return randomDirection(actualdirection);
        }
        else
            return rand;
    }
    IEnumerator ignorecolission ()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
        gm.escudo = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<CircleCollider2D>().isTrigger = false;
    }
    IEnumerator score2x()
    {
        mult = 2;
        yield return new WaitForSeconds(10);
        mult = 1;
    }
    IEnumerator flash()
    {
        int count =0;
        while (count<25)
        {
            yield return new WaitForSeconds(0.4f);
            count++;
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            GetComponent<TrailRenderer>().enabled = !GetComponent<TrailRenderer>().enabled;
        }
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<TrailRenderer>().enabled = true;
    }
    IEnumerator deathPW()
    {
        death = true;
        GetComponent<SpriteRenderer>().color = Color.black;
        trailtx.SetColor("_TintColor",Color.grey);
        yield return new WaitForSeconds(6);
        GetComponent<SpriteRenderer>().color = Color.white;
        trailtx.SetColor("_TintColor", Color.white);       
        death = false;
    }
   IEnumerator acelerometrepw()
    {
        acelerometre = true;
        trailtx.SetColor("_TintColor", Color.green);
        yield return new WaitForSeconds(8);
        trailtx.SetColor("_TintColor", Color.white);
        acelerometre = false;
        teleport = true;
    }
    IEnumerator taptappw()
   {
       taptap = true;
       player.SetActive(false);
       Time.timeScale = 0.7f;
       yield return new WaitForSeconds(8);
       Time.timeScale = 1;
       player.SetActive(true);
       taptap = false;
   }
    IEnumerator minusclepw()
    {
        for (int i = 0; i < 3; i++)
        {
            transform.localScale *= 1.4f;
            yield return new WaitForSeconds(3);
            transform.localScale /= 1.4f;
            transform.localScale /= 1.4f;
            yield return new WaitForSeconds(3);
            transform.localScale *= 1.4f;
        }
    }
        IEnumerator TrollPw()
    {
        bool loop = true;
        int c = 0;
        trailtx.SetColor("_TintColor", new Color( 61/255f, 245/255f, 210/255f));
       while (loop)
       {
           if (c>6)
           {
               loop = false;
               trailtx.SetColor("_TintColor", Color.white);
           }
           c++;
           teleport = true ;
             yield return new WaitForSeconds(Mathf.Abs(speed));
       }
    }
}

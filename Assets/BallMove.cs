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
   	// Use this for initialization
	void Start ()
    {
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
        Balltouch = Resources.Load("Prefab/BallTouch") as GameObject;
        LookandGO(places[direction].position, speed);      
	}
	
	// Update is called once per frame
	void Update () {
        
        balls = GameObject.FindGameObjectsWithTag("Ball");
		       if (teleport)
               {
                   randDire = randomDirection(direction);
                   direction = randDire;
                   speed = speed >= 8 ? speed : speed += 0.07f;
                   LookandGO(places[direction].position, speed);
                   teleport = false;
               }
        if (perdeu)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), 2 * Time.deltaTime);
            if (balls.Length == 1)
            {
                player.SetActive(false);               

            }
            if (Vector3.Distance(transform.position,new Vector3(0,0,0))<=0.05f)
            {
              GameObject g =  Instantiate(particle,transform.position,Quaternion.identity) as GameObject;
                Destroy(gameObject);
                Destroy(g, 5);
               
            }
        }
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            gm.randPowerup();
            gm.SetScore += mult*1;
           switch (direction)
           {
                case 0:
                    transform.position = new Vector3(transform.position.x,coll.gameObject.transform.position.y)+ new Vector3(0, -offset, 0);
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
        if (coll.gameObject.tag == "Limits")
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
        }
        
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            gm.SetScore += mult * 1;
            switch (direction)
            {
                case 0:
                    transform.position = new Vector3(transform.position.x,coll.gameObject.transform.position.y)+ new Vector3(0, -offset, 0);
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
        if (coll.gameObject.tag == "Limits")
        {
            rb.velocity = Vector3.zero;
            perdeu = true;
        }
        
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
   
}

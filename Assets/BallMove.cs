using UnityEngine;
using System.Collections;

public class BallMove : MonoBehaviour {
	public int direction; 
	public float speed;
	[SerializeField] Transform[] places = new Transform[4];
	bool perdeu;
	GameObject player;
	Rigidbody2D rb;
    int randDire;
    public float offset;
    private bool teleport;
    public GameObject particle;
    private float  shakeDuration, shakeAmount, decreaseFactor;
	// Use this for initialization
	void Start () {
        teleport = false;
        rb = GetComponent<Rigidbody2D> ();
		perdeu = false;
        speed = 8 ;
		places [0] = GameObject.Find ("PlaceUp").transform;
		places [1] = GameObject.Find ("PlaceDown").transform	;
		places [2] = GameObject.Find ("PlaceLeft").transform;
		places [3] = GameObject.Find ("PlaceRight").transform;
		player = GameObject.Find ("Player");		
        LookandGO(places[direction].position, speed);
        shakeDuration = 0;
		shakeAmount = 0.1f;
		decreaseFactor = 1;
	}
	
	// Update is called once per frame
	void Update () {        
		       if (teleport)
               {
                   randDire = randomDirection(direction);
                   direction = randDire;
                   speed = speed >= 8 ? speed : speed += 0.1f;
                   LookandGO(places[direction].position, speed);
                   teleport = false;
               }
        if (perdeu)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), 2 * Time.deltaTime);
            player.SetActive(false);
            shakeDuration = 1;
            ShakeBall();
            if (Vector3.Distance(transform.position,new Vector3(0,0,0))<=0.05f)
            {
                Instantiate(particle,transform.position,Quaternion.identity);
                Destroy(gameObject);

            }
        }
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
          
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
           teleport = true;
        }
        if (coll.gameObject.tag == "Limits")
        {
            rb.velocity = Vector3.zero;
            perdeu = true;
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
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
            teleport = true;
         }
        if (coll.gameObject.tag =="Limits")
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
        rb.velocity = transform.up * speed;
       // Debug.Log("veio aqui");
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
    void ShakeBall ()
    {
            Vector3 bposition = transform.position;
		
			if (shakeDuration > 0)
			{
				transform.position = bposition + Random.insideUnitSphere * shakeAmount;
				shakeDuration -= Time.deltaTime * decreaseFactor;
			}
			else
			{
				shakeDuration = 0f;
				transform.position = bposition;
			}
    }
}

using UnityEngine;
using System.Collections;

public class touchV2 : MonoBehaviour {
    public static bool salvarposi,esperar;
    public static bool esq, dir, cima, baixo, aperto;
    float posix, posiy;
    float timer; 
	float left,right,up,down;
	// Use this for initialization
	void Start () {
        salvarposi = true;
        esperar = true;
        timer = 0; 
	}
	void FixedUpdate()
	{
		
	}

	
	// Update is called once per frame
	void Update ()
	{
		if (Input.touchCount > 0) {
			if (salvarposi) {
				posix = Input.GetTouch (0).position.x;
				posiy = Input.GetTouch (0).position.y;
				//	Debug.Log("x= " + posix + " y= " + posiy);
				salvarposi = false;

			}

			if (Input.GetTouch (0).phase == TouchPhase.Moved && !salvarposi) {
				
					left = Input.GetTouch (0).position.x - posix;
					right = Input.GetTouch (0).position.x - posix;
					up = Input.GetTouch (0).position.y - posiy;
					down = Input.GetTouch (0).position.y - posiy;
				//Debug.Log ("left= " + left + " up = " + up);
					if (right > 5 && right > up && right > down * -1 && esperar) {
						Debug.Log("arrasto righteta");   
						dir = true; 

						esperar = false;
					} else if (left * -1 > 5 && left * -1 > up && left * -1 > down * -1 && esperar) {
						//Debug.Log("arrasto left");    
						esq = true;
						esperar = false;
					} else if (up > 5 && up > right && up > left * -1 && esperar) {
						//Debug.Log("arrasto up");       
						cima = true; 
						esperar = false;
					} else if (down * -1 > 5 && down * -1 > right && down * -1 > left * -1 && esperar) {
						//Debug.Log("arrasto down"); 
						baixo = true; 
						esperar = false;
					}

			}
			if (Input.GetTouch (0).phase == TouchPhase.Stationary) {
				timer += Time.deltaTime;
				if (timer > 0.1f) {
					aperto = true;
					esperar = false; 
				}
			} else {
				timer = 0;
				aperto = false;
			}
			if (Input.GetTouch (0).phase == TouchPhase.Ended) {
				salvarposi = true;
				esperar = true;
			}
		}

	}
}

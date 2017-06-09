using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 15.0f;
	public float paddingOnShip = 1f;
	
	float xMin;
	float xMax;
	
	void Start(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xMin = leftMost.x + paddingOnShip;
		xMax = rightMost.x - paddingOnShip;
	}

	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow)){
		
//			transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);

			transform.position += Vector3.left * speed * Time.deltaTime;
			
		}else if(Input.GetKey(KeyCode.RightArrow)){
		
			transform.position += Vector3.right * speed * Time.deltaTime;
			
		}
		
		//restrict the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		
		
		//else if(Input.GetKey(KeyCode.UpArrow)){
//			transform.position += new Vector3(0, speed * Time.deltaTime, 0);
//		}else if(Input.GetKey(KeyCode.DownArrow)){
//			transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
//		}				
	}
}

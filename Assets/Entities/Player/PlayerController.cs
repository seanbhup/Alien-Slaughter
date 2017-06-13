using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 15.0f;
	public float paddingOnShip = 1f;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public float health = 250f;
	
	public AudioClip fireSound;
	
	float xMin;
	float xMax;
	
	void Start(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xMin = leftMost.x + paddingOnShip;
		xMax = rightMost.x - paddingOnShip;
	}
	
	void Fire(){
	//makes it so projectile doesnt spawn on player, killing him.
	//Vector3 offset = new Vector3(0,1,0);
		GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0,projectileSpeed,0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
			InvokeRepeating("Fire",0.000001f, firingRate);
		}
		if(Input.GetKeyUp(KeyCode.A)){
			CancelInvoke("Fire");
		}
	
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
	
	void OnTriggerEnter2D(Collider2D collider){
		
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			Debug.Log("Player was hit");
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0){
				Destroy(gameObject);
			}
		}
	}
}

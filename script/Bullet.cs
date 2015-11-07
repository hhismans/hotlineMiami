using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public 	float		speed = 30;
	private Vector3		direction;
	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		Vector3 pos;
		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		
		var angle = Vector3.Angle(pos-transform.position,Vector3.down);
		
		if(pos.x > transform.position.x)
			angle*=-1;
		
		transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,270 - angle),1);


		rb = GetComponent<Rigidbody2D>();
		//rb.velocity = transform.TransformDirection (Vector3.forward * speed);
	}

	void FixedUpdate () {
		rb.velocity = transform.right * speed;
	}
}

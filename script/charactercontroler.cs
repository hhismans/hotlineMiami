using UnityEngine;
using System.Collections;

public class charactercontroler : MonoBehaviour {

	// Use this for initialization
	private Rigidbody2D rb;
	public	float		speed;
	public	bool		weaponIsSet = false;


	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	
	void LookAtMouse(GameObject Target, GameObject Entity)
	{
		/*float AngleRad = Mathf.Atan2(Input.mousePositionon.y - Entity.transform.position.y, Target.transform.position.x - Entity.transform.position.x);
		// Get Angle in Degrees
		float AngleDeg = (180 / Mathf.PI) * AngleRad;
		// Rotate Object
		this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);*/
	}

	void getBoolaxis()
	{
	/*	if (Input.GetAxis("Horizontal") && !getAxisHinUse)
			getAxisHinUse = true;
		if (Input.GetAxis ("Horizontal") == 0)
			getAxisHinUse = false;
		if (Input.GetAxis("Vertical") && !getAxisVinUse)
			getAxisVinUse = true;
		if (Input.GetAxis ("Vertical") == 0)
			getAxisVinUse = false;*/
	}

	public void setWeapon (Weapon weapon)
	{
		weapon.transform.parent = gameObject.transform;
		weapon.gameObject.layer = 3;
		weapon.gameObject.transform.localPosition = new Vector3(0.25f,-0.2f); 
		weapon.gameObject.transform.localEulerAngles = new Vector3(0,0); 
		weapon.gameObject.transform.localScale = new Vector3 (1,1,1);
	}

	void FixedUpdate () {
		//move
		float moveH = Input.GetAxis ("Horizontal");
		float moveV = Input.GetAxis("Vertical");

		rb.MovePosition(transform.position + new Vector3 (moveH * speed, moveV * speed));

		// ROTATION
		Vector3 pos;
		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		
		var angle = Vector3.Angle(pos-transform.position,Vector3.down);
		
		if(pos.x > transform.position.x)
			angle*=-1;
		
		transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,180 - angle),1);



		//GetComponent<Rigidbody2D>().MoveRotation(Quaternion.Euler(new Vector3(0, m_Angle - 270, 0)));
	}
}
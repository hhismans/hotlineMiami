using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Sprite onFloorSprite;
	public Sprite EquipedSprite;
	public int	  munition;
	public Bullet bullet;
	public float coolDown = 1;

	private bool isSet = false;
	private charactercontroler Player = null;
	private bool usable = true;
	private Rigidbody2D rb;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Player" && !isSet && !coll.gameObject.GetComponent<charactercontroler>().weaponIsSet)
		{
			Player = coll.gameObject.GetComponent<charactercontroler>();
			if (!coll.gameObject.GetComponent<charactercontroler>().weaponIsSet)
			{
				isSet = true;
				GetComponent<Renderer>().sortingOrder = 3;
				GetComponent<SpriteRenderer>().sprite = EquipedSprite;
				coll.gameObject.GetComponent<charactercontroler>().weaponIsSet = true;
				transform.parent = coll.gameObject.GetComponent<charactercontroler>().transform;
				gameObject.layer = 3;
				gameObject.transform.localPosition = new Vector3(0.25f,-0.2f); 
				gameObject.transform.localEulerAngles = new Vector3(0,0); 
				gameObject.transform.localScale = new Vector3 (1,1,1);
			}
		}

	}

	void dropWeapon()
	{
		GetComponent<Renderer>().sortingOrder = 0;
		GetComponent<SpriteRenderer>().sprite = onFloorSprite;
		transform.parent = null;
		Player.weaponIsSet = false;
		Player = null;
	}

	IEnumerator throwingMovement()
	{
		Vector3 MousePos  = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		MousePos.z = 0;
		float speed = 0.7f;

		Debug.Log(MousePos);
		while (speed > 0)
		{
			transform.position = Vector3.MoveTowards(transform.position, MousePos, speed);
			speed -= 0.05f;
			yield return new WaitForEndOfFrame();
		}
		isSet = false;
		
	}

	IEnumerator CoolDownWeapon()
	{
		usable = false;
		yield return new WaitForSeconds (coolDown);
		usable = true;
	}
	void shot()
	{
		Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
	}
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isSet)
		{
			if (Input.GetMouseButtonDown(0) && usable)
			{
				shot ();
				StartCoroutine(CoolDownWeapon());
				Debug.Log ("shot");	
			}
			else if (Input.GetMouseButtonDown(1))
			{
				dropWeapon();
				StartCoroutine(throwingMovement());
			}
		}
	}
}

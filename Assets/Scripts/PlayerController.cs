using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;
	public bool canJump;
	public Text countText;
	private int count;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
	}

	void Update()
	{
		{
			if (transform.position.y <= -30.0f)
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		rb.AddForce(movement * speed);

		GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);

		{
			if (Input.GetKeyDown(KeyCode.Space) && canJump)
			{
				Vector3 jump = new Vector3(0.0f, 350.0f, 0.0f);

				GetComponent<Rigidbody>().AddForce(jump);
			}

		}

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Pick Up")) 
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}

		if (other.gameObject.CompareTag("Ground")) {

			canJump = true;            
		}


	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Ground"))
		{

			canJump = false;
		}
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
	}
}
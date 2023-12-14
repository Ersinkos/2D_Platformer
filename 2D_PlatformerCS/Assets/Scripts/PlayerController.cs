using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private float force = 10f;
	private float speed = 5f;
	private Rigidbody2D rb;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
	}
	private void FixedUpdate()
	{
		MovePlayer();
	}
	public void MovePlayer()
	{
		float x = Input.GetAxis("Horizontal");
		transform.Translate(new Vector2(x, 0f) * Time.fixedDeltaTime * speed);
	}
	public void Jump()
	{
		rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
	}
}

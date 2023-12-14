using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private float speed = 10f;
	private void Update()
	{
		MovePlayer();
	}
	public void MovePlayer()
	{
		float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
		transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
	}
}

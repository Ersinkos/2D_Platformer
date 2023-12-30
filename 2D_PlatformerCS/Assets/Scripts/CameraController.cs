using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Vector3 offset = new Vector3(0f, 3f, -10f);
	private float smoothFactor = 10f;
	private Vector3 velocity = Vector3.zero;
	[SerializeField] Transform target;

	private void LateUpdate()
	{
		Follow();
	}
	public void Follow()
	{
		Vector3 targetPosition = new Vector3(target.position.x + offset.x,transform.position.y, target.position.z + offset.z);
		Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor);

		transform.position = smoothPosition;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Vector2 moveInput;
	private Rigidbody2D rb;
	[Space]
	[Header("Movement")]
	[SerializeField] private float moveSpeed;
	[SerializeField] private float acceleration;
	[SerializeField] private float decceleration;
	[SerializeField] private float velPower;
	[SerializeField] private float frictionAmount;

	[Space]
	[Header("Jumping")]
	[SerializeField] private float jumpForce;
	[SerializeField] private float jumpCoyoteTime;
	[SerializeField] private float jumpBufferTime;
	[SerializeField] private float fallGravityMultiplier;
	private bool isJumping;
	private bool isFalling;
	private float lastGroundedTime;
	private float lastJumpTime;


	[Space]
	[Header("Ground Check")]
	[SerializeField] Transform groundCheckPoint;
	[SerializeField] Vector2 groundCheckSize;
	[SerializeField] LayerMask groundLayer;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		isJumping = false;
		isFalling = false;
		lastGroundedTime = 0;
		lastJumpTime = 0;
	}

	private void Update()
	{
		lastGroundedTime -= Time.deltaTime;
		lastJumpTime -= Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.Space))
		{
			OnJumpInput();
		}
		if (isJumping)
		{
			if (IsGrounded())
			{
				lastGroundedTime = jumpCoyoteTime;
			}
		}
		if (isJumping && rb.velocity.y < 0)
		{
			isJumping = false;
			isFalling = true;
		}
		if (lastGroundedTime > 0 && !isJumping)
		{
			isFalling = false;
		}
		if (CanJump() && lastJumpTime > 0)
		{
			isJumping = true;
			isFalling = false;
		}
		if (IsGrounded())
		{
			isJumping = false;
			lastGroundedTime = jumpCoyoteTime;
		}
	}
	private void FixedUpdate()
	{
		moveInput.x = Input.GetAxisRaw("Horizontal");
		Run(1f);
		if (lastJumpTime > 0 && lastGroundedTime > 0)
		{
			Jump();
		}
	}
	private void Run(float lerpAmount)
	{
		float targetSpeed = moveInput.x * moveSpeed;
		targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, lerpAmount);
		float speedDif = targetSpeed - rb.velocity.x;
		float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
		float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
		rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
	}
	private void Jump()
	{
		lastJumpTime = 0;
		lastGroundedTime = 0;
		rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
	}
	private bool IsGrounded()
	{
		return Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
	}
	private bool CanJump()
	{
		return lastGroundedTime > 0 && !isJumping;
	}
	private void OnJumpInput()
	{
		lastJumpTime = jumpBufferTime;
	}
}

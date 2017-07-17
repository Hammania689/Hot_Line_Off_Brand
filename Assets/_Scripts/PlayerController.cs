using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(requiredComponent: typeof(Camera))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject reticle;
    [SerializeField] private Camera mainCamera;


    private Rigidbody2D rb;
    private Vector3 cursorPos;

	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update ()
	{
        MouseToReticle();
	    Movement();
	}

    // Use to check wheter or not Player is pressing move keys
    bool isMoving()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            return false;
        return true;
    }

    // Use to change postition of the Player
    private void Movement()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
            rb.velocity += new Vector2(Input.GetAxisRaw("Horizontal"), rb.velocity.y) * speed;
        if (Input.GetAxisRaw("Vertical") != 0)
            rb.velocity += new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical")) * speed;
        else if (isMoving() == false && rb.velocity.magnitude > 0f)
        {
            rb.velocity = rb.velocity * Mathf.MoveTowards(rb.velocity.sqrMagnitude,0,rb.velocity.sqrMagnitude / Time.deltaTime);
        }

        float maxSpeed = 6f;

        if (rb.velocity.sqrMagnitude > maxSpeed)
        {
            Vector2 newSpeed = rb.velocity.normalized;
            rb.velocity = newSpeed * speed;
        }
    }

    // Use to update the position of the reticle
    private void MouseToReticle()
    {
        cursorPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        reticle.transform.position = new Vector2(cursorPos.x, cursorPos.y);
    }
}

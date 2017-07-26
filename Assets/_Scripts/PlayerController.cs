using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Vector3 cursorPos;
    public static GameObject player;

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private float speed = 6.5f;
    [SerializeField]
    private GameObject reticle;
    [SerializeField]
    private GameObject projectile;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = this.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MouseToReticle();
        ShootProjectile();
        Movement();
    }

    // Use to check wheter or not Player is pressing move keys
    bool isMoving()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            return false;
        return true;
    }

    // Changes postition of the Player
    private void Movement()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
            rb.velocity += new Vector3(Input.GetAxisRaw("Horizontal"), rb.velocity.y) * speed;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            rb.velocity += new Vector3(rb.velocity.x, 0, speed);
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            rb.velocity += new Vector3(rb.velocity.x, 0, -speed);
        else if (isMoving() == false && rb.velocity.magnitude > 0f)
            rb.velocity = rb.velocity * Mathf.MoveTowards(rb.velocity.sqrMagnitude, 0, rb.velocity.sqrMagnitude / Time.deltaTime);

        float maxSpeed = 15f;

        if (rb.velocity.sqrMagnitude > maxSpeed)
        {
            Vector3 newSpeed = rb.velocity.normalized;
            rb.velocity = newSpeed * speed;
        }
    }

    // Updates the position of the reticle
    private void MouseToReticle()
    {
        cursorPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        reticle.transform.position = new Vector3(cursorPos.x, 0, cursorPos.z);
    }

    // Takes the current position and adds an offset from the normalized magnitude of the Reticle
    private Vector3 launchPos()
    {
        return (transform.position + (cursorPos.normalized * 3));
    }

    // On Left Click instantiates and shoots a projectile with normalized vector values of the Reticle
    private void ShootProjectile()
    {
        Rigidbody2D prefab;
        if (Input.GetMouseButtonDown(0))
        {
            prefab = Instantiate(projectile.GetComponent<Rigidbody2D>(),
                launchPos(),projectile.transform.rotation, this.transform) as Rigidbody2D;

            Vector2 direction = new Vector2(cursorPos.x,cursorPos.z).normalized;
            prefab.velocity += direction * 8;
            Destroy(prefab.gameObject, 5);
        }
    }
}

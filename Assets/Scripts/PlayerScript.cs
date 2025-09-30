using UnityEditor.UI;
using UnityEngine;

/*
    This script provides jumping and movement in Unity 3D - Gatsby
*/

public class Player : MonoBehaviour
{
    public Transform head;

    // Camera Rotation
    public float mouseSensitivity = 2f;
    private float verticalRotation = 0f;
    private Transform cameraTransform;

    // Ground Movement
    private Rigidbody rb;
    public float MoveSpeed = 5f;
    private float moveHorizontal;
    private float moveForward;

    // Jumping
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f; // Multiplies gravity when falling down
    public float ascendMultiplier = 2f; // Multiplies gravity for ascending to peak of jump
    private bool isGrounded = true;
    public LayerMask groundLayer;
    private float groundCheckTimer = 0f;
    private float groundCheckDelay = 0.3f;
    private float playerHeight;
    private float raycastDistance;

    public int hp = 0;
    public int lvl = 1;

    public HpBarScript healthBar;

    public int lvl1Cap;
    public int lvl2Cap;

    public Light directionalLight;

    void Start()
    {
        set_hp(0);

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cameraTransform = Camera.main.transform;

        // Set the raycast to be slightly beneath the player's feet
        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight / 2) + 0.2f;

        // Hides the mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        update_lights();



        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveForward = Input.GetAxisRaw("Vertical");

        RotateCamera();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // Checking when we're on the ground and keeping track of our ground check delay
        if (!isGrounded && groundCheckTimer <= 0f)
        {
            Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
            isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, groundLayer);
        }
        else
        {
            groundCheckTimer -= Time.deltaTime;
        }

    }

    void FixedUpdate()
    {
        MovePlayer();
        ApplyJumpPhysics();
    }

    void update_lights()
    {
        if (transform.position.y > 240)
        {

        }
        directionalLight.intensity = Mathf.Clamp(((transform.position.y - 100) / 130) * 2.1f, 0f, 2.1f);
        if (directionalLight.intensity <= 0f)
        {
            // Disable ambient light
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientLight = Color.black;

            // Remove skybox influence
            RenderSettings.skybox = null;

            // Turn off reflections
            RenderSettings.reflectionIntensity = 0f;
        }
    }

    void MovePlayer()
    {

        Vector3 movement = (transform.right * moveHorizontal + transform.forward * moveForward).normalized;
        Vector3 targetVelocity = movement * MoveSpeed;

        // Apply movement to the Rigidbody
        Vector3 velocity = rb.linearVelocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.linearVelocity = velocity;

        // If we aren't moving and are on the ground, stop velocity so we don't slide
        if (isGrounded && moveHorizontal == 0 && moveForward == 0)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    void RotateCamera()
    {
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        head.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        //cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

    }

    void Jump()
    {
        isGrounded = false;
        groundCheckTimer = groundCheckDelay;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z); // Initial burst for the jump
    }

    void ApplyJumpPhysics()
    {
        if (rb.linearVelocity.y < 0)
        {
            // Falling: Apply fall multiplier to make descent faster
            rb.linearVelocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        } // Rising
        else if (rb.linearVelocity.y > 0)
        {
            // Rising: Change multiplier to make player reach peak of jump faster
            rb.linearVelocity += Vector3.up * Physics.gravity.y * ascendMultiplier * Time.fixedDeltaTime;
        }
    }

    public void take_damage(int damagePoints)
    {
        set_hp(0-damagePoints);
    }
    public void drop_item(int itemPoints)
    {
        set_hp(0-itemPoints);
    }

    public void take_item(int itemPoints)
    {
        set_hp(itemPoints);
    }
    public void set_hp(int itemPoints)
    {
        int oldLvl = lvl;
        hp += itemPoints;
        hp = Mathf.Clamp(hp, 0, 596);
        healthBar.update_bar(hp);

        if (hp >= lvl2Cap)
        {
            lvl = 3;
        }else if (hp >= lvl1Cap)
        {
            lvl = 2;
        }else if(hp <= lvl2Cap)
        {
            lvl = 1;
        }
        if (oldLvl > lvl)
        {
            if(lvl == 1)
            {
                transform.position = new Vector3(258, 267, 88);
            }else if (lvl == 2)
            {
                transform.position = new Vector3(275, 140, 250);
            }
        }

    }
}

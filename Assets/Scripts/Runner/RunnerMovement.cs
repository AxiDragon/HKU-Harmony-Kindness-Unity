using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerMovement : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rb;
    [Tooltip("Force multiplier applied to the player cube.")]
    public float extraForce;
    [Tooltip("Force multiplier applied to the player cube for jumps.")]
    public float jumpForce;
    float groundDistance = 0.1f;
    bool isGrounded = true;
    Animator playerAnim;

    Transform groundCheck;
    LayerMask groundMask;

    public void Start()
    {
        groundCheck = transform.Find("GroundCheck");
        groundMask = LayerMask.GetMask("Ground");
        playerAnim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            playerAnim.SetBool("isGrounded", true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAnim.SetTrigger("jump");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        else
            playerAnim.SetBool("isGrounded", false);
            

        float horizontal = Input.GetAxis("Horizontal");

        rb.AddForce(Vector3.right * horizontal * extraForce * Time.deltaTime);
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, extraForce / -5f, extraForce / 5f), rb.velocity.y, rb.velocity.z);

        if (isGrounded)
            rb.transform.Translate(Vector3.right * horizontal * Time.deltaTime * 3f);

        Vector3 rotation = Vector3.forward * rb.velocity.x;
        transform.rotation = Quaternion.Euler(rotation);

        if (transform.position.y < -1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

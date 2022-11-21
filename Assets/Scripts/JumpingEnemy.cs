using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private int jumpPower;

    [SerializeField]
    private bool isGrounded;

    [SerializeField]
    private float groundCheckRadius;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        jumpPower = 100;
        StartCoroutine(Jumping());
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);
    }

    public IEnumerator Jumping()
    {
        while (true)
        {
            GroundCheck();

            if (isGrounded)
            {
                rb2d.AddForce(new Vector2(0, jumpPower));
            }
            yield return new WaitForSeconds(3);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().loseLife();
            Destroy(gameObject);
        }
    }
}

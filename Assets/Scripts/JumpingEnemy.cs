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
    private int jumpCooldown;

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
        jumpPower = Random.Range(50,100);
        jumpCooldown = Random.Range(3,5);

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
            yield return new WaitForSeconds(jumpCooldown);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.infection = GameManager.instance.infection + 10;

            Destroy(gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    public float speed = 1;
    public float jumpForce = 400f;
    public LayerMask groundLayer;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spRenderer;
    private bool isGround = true;

    // Start is called before the first frame update
    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.spRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal"); // left:-1, idle:0, right:1
        anim.SetFloat("Speed", Math.Abs(x) * speed);
        rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);

        if (x < 0)
        {
            spRenderer.flipX = true;
        }
        else if (x > 0)
        {
            spRenderer.flipX = false;
        }

        if (Input.GetButtonDown("Jump") & isGround) // ジャンプ
        {
            Jump();
        }

        Vector3 left_SP = transform.position - Vector3.right * 0.3f;
        Vector3 right_SP = transform.position + Vector3.right * 0.3f;
        Vector3 EP = transform.position - Vector3.up * 0.1f;
        Debug.DrawLine(left_SP, EP, Color.red);
        Debug.DrawLine(right_SP, EP, Color.red);
    }

    private void FixedUpdate()
    {
        isGround = IsGround();
        Debug.Log(isGround);

        // Vector2 groundPos =
        //     new Vector2(
        //         transform.position.x,
        //         transform.position.y
        //     );

        // Vector2 groundArea = new Vector2(0.5f, 0.5f);

        // Debug.DrawLine(groundPos + groundArea, groundPos - groundArea, Color.white);

        // isGround =
        //     Physics2D.OverlapArea(
        //         groundPos + groundArea,
        //         groundPos - groundArea,
        //         groundLayer
        //     );

        // Debug.Log(isGround);

    }
    void Jump()
    {
        anim.SetBool("isJump", true);
        rb2d.AddForce(Vector2.up * jumpForce);
    }
    bool IsGround()
    {
        Vector3 left_SP = transform.position - Vector3.right * 0.2f;
        Vector3 right_SP = transform.position + Vector3.right * 0.2f;
        Vector3 EP = transform.position - Vector3.up * 0.6f;
        return Physics2D.Linecast(left_SP, EP, groundLayer) || Physics2D.Linecast(right_SP, EP, groundLayer);
    }
}
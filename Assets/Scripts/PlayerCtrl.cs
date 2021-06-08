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

        if (Input.GetButtonDown("Jump") & isGround) // スペースキーを押した時 かつ 地面に接地していた時 にジャンプ
        {
            Jump();
        }

        if (isGround) // 地面にいるときはジャンプモーションoff
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", false);
        }

        float velX = rb2d.velocity.x;
        float velY = rb2d.velocity.y;
        if (velY > 0.5f) // velocityが上向きに0.5fを超えていたらジャンプ
        {
            anim.SetBool("isJump", true);
        }
        else
        {
            anim.SetBool("isJump", false);
        }
        if (velY < -0.3f) // velocityが下向きに0.1fを超えていたら落下
        {
            anim.SetBool("isFall", true);
        }
        else
        {
            anim.SetBool("isFall", false);
        }


        Vector3 left_SP = transform.position - Vector3.right * 0.3f; // デバッグ用にsceneビューに当たり判定線を表示 -> 表示されない？
        Vector3 right_SP = transform.position + Vector3.right * 0.3f;
        Vector3 EP = transform.position - Vector3.up * 0.6f;
        Debug.DrawLine(left_SP, EP, Color.red);
        Debug.DrawLine(right_SP, EP, Color.red);
    }

    private void FixedUpdate()
    {
        isGround = IsGround();
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
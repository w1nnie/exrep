using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    public float speed = 1;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spRenderer;

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
        float x = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Math.Abs(x) * speed);
        rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);
        Debug.Log(x * speed);

        if (x < 0)
        {
            spRenderer.flipX = true;
        }
        else if (x > 0)
        {
            spRenderer.flipX = false;
        }
    }
}
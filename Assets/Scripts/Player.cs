using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //public variables
    public GameObject bullet;

    //Components
    private Rigidbody2D rb2D;
    private AudioSource jumpAudio;
    private AudioSource CollectAudio;
    private AudioSource hurtAudio;

    //serialized fields
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    //normal variables
    private float moveHorizontal;
    private float moveVertical;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private int cherries;
    //start method
    void Start(){
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        jumpAudio = gameObject.GetComponent<AudioSource>();
        CollectAudio = gameObject.GetComponent<AudioSource>();
        hurtAudio = gameObject.GetComponent<AudioSource>();
        moveSpeed = 3f;
        jumpForce = 10f;
        cherries = 0;
    }

    //Update method
    void Update(){
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        MovePlayer();
        ShootPlayer();
    }

    private void ShootPlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }

    //Move Player
    private void MovePlayer() {
        if (moveHorizontal > 0f)
        {
            rb2D.velocity = new Vector2(moveHorizontal * moveSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = false;
            anim.SetBool("isRunning", true);
        }
        else if (moveHorizontal < 0f)
        {
            rb2D.velocity = new Vector2(moveHorizontal * moveSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = true;
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown("w"))
        {
            Jump();
        }
    }

    //Jump
    private void Jump() {
        rb2D.velocity = new Vector2(rb2D.velocity.x, moveVertical * jumpForce);
        anim.SetTrigger("isJumping");
        jumpAudio.Play();
    }

    //if on ground
    private void OnTriggerEnter2D(Collider2D collision){ 
        if (collision.gameObject.tag == "Cherry")
        {
            CollectAudio.Play();
            Destroy(collision.gameObject);
            cherries++;
        }

        if(collision.gameObject.tag == "Trap"){
            Destroy(collision.gameObject);
            hurtAudio.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
}

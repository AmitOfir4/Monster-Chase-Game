using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;
    
    [SerializeField] 
    private float jumpForce = 11f;

    private float movementX;
    private float movementY;
    [SerializeField]
    private Rigidbody2D myBody;
    private Animator anim;
    private SpriteRenderer sr;
    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";
    private bool isGrounded = true;
    private bool hasShield = false;
    private string ENEMY_TAG = "Enemy";
    private string COIN_TAG = "Coin";

    [SerializeField]
    private Timer timer;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (timer == null)
        {
            timer = FindObjectOfType<Timer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer()
    {
        if (gameObject.name == "Rino(Clone)")
        {
            AnimateRino();
        }
        else
        {
            if (movementX > 0)
            {
                anim.SetBool(WALK_ANIMATION, true);
                sr.flipX = false;
            }
            else if (movementX < 0)
            {
                anim.SetBool(WALK_ANIMATION, true);
                sr.flipX = true;
            }
            else
            {
                anim.SetBool(WALK_ANIMATION, false);
            }
        }
    }

    void AnimateRino()
    {
        if (movementX > 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }
        else if (movementX < 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
           isGrounded = false;
           myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag(ENEMY_TAG) && !hasShield)
        {
            Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag(COIN_TAG))
        {
            Destroy(collision.gameObject);
            GivePlayerShield();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ENEMY_TAG) && !hasShield)
        {
            Destroy(gameObject);
        }
    }

    private void GivePlayerShield()
    {
        hasShield = true;
        Debug.Log("Shield activated for 10 seconds");
        StartCoroutine(DisableShieldAfterTime(10f));
    }
    
    private IEnumerator DisableShieldAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        hasShield = false;
        Debug.Log("Shield disabled after 10 seconds");
    }
}

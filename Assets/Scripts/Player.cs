using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveForce = 10f;
    
    [SerializeField] private float jumpForce = 11f;

    [SerializeField] private ShieldUI shieldUI;

    private float _movementX;
    private float _movementY;
    [SerializeField]
    private Rigidbody2D myBody;
    private Animator _anim;
    private SpriteRenderer _sr;
    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";
    private bool _isGrounded = true;
    private bool _hasShield = false;
    private Coroutine _shieldCoroutine;
    private float _shieldDuration = 10f;
    private string ENEMY_TAG = "Enemy";
    private string COIN_TAG = "Coin";

    [SerializeField]
    private Timer timer;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (timer == null)
        {
            timer = FindObjectOfType<Timer>();
        }
        if (shieldUI ==  null)
        {
            shieldUI = FindObjectOfType<ShieldUI>();
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
        _movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(_movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer()
    {
        if (gameObject.name == "Rino(Clone)")
        {
            AnimateRino();
        }
        else
        {
            if (_movementX > 0)
            {
                _anim.SetBool(WALK_ANIMATION, true);
                _sr.flipX = false;
            }
            else if (_movementX < 0)
            {
                _anim.SetBool(WALK_ANIMATION, true);
                _sr.flipX = true;
            }
            else
            {
                _anim.SetBool(WALK_ANIMATION, false);
            }
        }
    }

    void AnimateRino()
    {
        if (_movementX > 0)
        {
            _anim.SetBool(WALK_ANIMATION, true);
            _sr.flipX = true;
        }
        else if (_movementX < 0)
        {
            _anim.SetBool(WALK_ANIMATION, true);
            _sr.flipX = false;
        }
        else
        {
            _anim.SetBool(WALK_ANIMATION, false);
        }
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
           _isGrounded = false;
           myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            _isGrounded = true;
        }

        if (collision.gameObject.CompareTag(ENEMY_TAG) && !_hasShield)
        {
            Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag(COIN_TAG))
        {
            Destroy(collision.transform.root.gameObject);
            GivePlayerShield();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ENEMY_TAG) && !_hasShield)
        {
            Destroy(gameObject);
        }
    }

    private void GivePlayerShield()
    {
        _hasShield = true;
        Debug.Log($"Shield activated for {_shieldDuration} seconds");

        if (_shieldCoroutine != null)
        {
            StopCoroutine(_shieldCoroutine);
        }

        _shieldCoroutine = StartCoroutine(DisableShieldAfterTime(_shieldDuration));

        if (shieldUI != null)
        {
            shieldUI.ActivateShield(_shieldDuration);
        }
    }
    
    private IEnumerator DisableShieldAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        _hasShield = false;
        Debug.Log($"Shield disabled after {_shieldDuration} seconds");
    }
}

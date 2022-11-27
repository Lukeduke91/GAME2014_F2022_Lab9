using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Movement")]
    public float HorizontalForce;
    public float HorizontalSpeed;
    public float VerticalForce;
    public float airFactor;
    public Transform GroundPoint;
    public float GroundRadius;
    public LayerMask GetLayerMask;
    public bool isGrounded;

    [Header("Animations")]
    public Animator animator;
    public PlayerAnimationState playerAnimationState;

    [Header("Health System")]
    public HealthBarController Health;
    public LifeCounterController life;
    public DeathPlaneController deathPlane;

    [Header("Controls")]
    public Joystick leftStick;
    [Range(0.1f, 1.0f)]
    public float verticalThreshold;

    private Rigidbody2D rigidbody2D;
    private SoundManager soundManager;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Health = FindObjectOfType<PlayerHealth>().GetComponent<HealthBarController>();
        life = FindObjectOfType<LifeCounterController>();
        deathPlane = FindObjectOfType<DeathPlaneController>();
        soundManager = FindObjectOfType<SoundManager>();
        leftStick = (Application.isMobilePlatform) ? GameObject.Find("LeftStick").GetComponent<Joystick>() : null;
    }

    void Update()
    {
        if(Health.value <= 0)
        {
            // TODO: update life counter
            life.LoseLife();
            if(life.value > 0)
            {
                Health.resetHealth();
                deathPlane.ReSpawn(gameObject);
                soundManager.PlaySoundFX(SoundFX.DEATH, Channel.PLAYER_DEATH_FX);
                // TODO: Play Death Sound
            }

        }

        if(life.value <= 0)
        {
            // TODO: Load the end scene
            SceneManager.LoadScene("End");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hit = Physics2D.OverlapCircle(GroundPoint.position, GroundRadius, GetLayerMask);
        isGrounded = hit;

        Move();
        Jump();
        AirCheck();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") + ((Application.isMobilePlatform) ? leftStick.Horizontal : 0.0f);
        if (x != 0.0f)
        {
            flip(x);
            x = ((x > 0.0) ? 1.0f : -1.0f);

            rigidbody2D.AddForce(Vector2.right * x * HorizontalForce  * ((isGrounded) ? 1 : airFactor));

            rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, HorizontalSpeed);

            ChangeAnimation(PlayerAnimationState.RUN);
        }
        if((isGrounded) && (x == 0.0f))
        {
            ChangeAnimation(PlayerAnimationState.IDLE);
        }
    }

    private void Jump()
    {
        var y = Input.GetAxis("Jump") + ((Application.isMobilePlatform) ? leftStick.Vertical : 0.0f);

        if((isGrounded) && (y > verticalThreshold))
        {
            rigidbody2D.AddForce(Vector2.up * VerticalForce, ForceMode2D.Impulse);
            soundManager.PlaySoundFX(SoundFX.JUMP, Channel.PLAYER_SOUND_FX);
        }
    }

    private void AirCheck()
    {
        if (!isGrounded)
        {
            ChangeAnimation(PlayerAnimationState.JUMP);
        }
    }

    public void flip(float x)
    {
        if(x != 0.0f)
        {
            transform.localScale = new Vector3((x > 0.0f) ? 1.0f : -1.0f, 1.0f, 1.0f);
        }
    }

    private void ChangeAnimation(PlayerAnimationState animationState)
    {
        playerAnimationState = animationState;
        animator.SetInteger("AnimationState", (int)playerAnimationState);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundPoint.position, GroundRadius);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Health.takeDamage(20);
            //TODO: update the life value
            if(life.value > 0)
            {
                soundManager.PlaySoundFX(SoundFX.HURT, Channel.PLAYER_HURT_FX);
                // TODO: Play the "Hurt" sound
            }

        }
    }
}

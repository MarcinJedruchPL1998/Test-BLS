using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public InputMenu inputMenu;

    [SerializeField] float speed = 2f;
    [SerializeField] float screenBoundsOffset = 0.5f;

    [SerializeField] GameplayManager gameplayManager;

    public int playerLives = 3;
    public int playerPoints;

    Vector2 moveDirection;
    Vector2 screenBounds;

    Animator anim;

    bool beenCollision;
    bool gameOver;

    void Awake()
    {
        inputMenu = new InputMenu();
        inputMenu.PlayerInput.Movement.started += ctx => PlayerMove(ctx.ReadValue<Vector2>());
        inputMenu.PlayerInput.Movement.canceled += ctx => PlayerMove(ctx.ReadValue<Vector2>());
    }

    private void Start()
    {
        anim = GetComponent<Animator>();

        //Calculate screen boundaries positions
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }


    private void OnEnable()
    {
        inputMenu.Enable();
    }

    private void OnDisable()
    {
        inputMenu.Disable();
    }

    public void PlayerMove(Vector2 direction)
    {
        moveDirection = direction;
    }

    void Update()
    {
        Vector2 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y + screenBoundsOffset, screenBounds.y - screenBoundsOffset);
        transform.position = pos;

        transform.Translate(moveDirection * speed * Time.deltaTime);

        if(!gameOver)
        {
            if (moveDirection.y > 0) //if player plane fly up (W or Arrow UP)
            {
                anim.Play("playerPlane_fly_up");
            }
            else if (moveDirection.y < 0) //if player plane fly down (S or Arrow DOWN)
            {
                anim.Play("playerPlane_fly_down");
            }
            else //if player plane fly forward (no key down)
            {
                anim.Play("playerPlane_fly_forward");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyPlane" && !beenCollision)
        {
            anim.Rebind();
            anim.Play("playerPlane_collision");

            RemoveLive();
            AddPoint(10);
           
            beenCollision = true; //Lock collisions with enemy planes for a while
        }
    }

    public void RemoveLive()
    {
        playerLives--;
        gameplayManager.LoadScoresAndLives();

        if(playerLives < 1)
        {
            gameOver = true;
            anim.Play("playerPlane_destroyed");
        }
       
    }

    public void PlaneDestroyed()
    {
        gameplayManager.GameOver();
    }

    public void AddPoint(int points)
    {
        playerPoints += points;
        gameplayManager.LoadScoresAndLives();
    }

    public void BeenCollision() //Animation Event, set "beenCollision" to false to allow next collisions
    {
        beenCollision = false;
    }

    public void ResetPlayer()
    {
        transform.position = new Vector2(transform.position.x, 0f);
        playerLives = 3;
        playerPoints = 0;
        gameplayManager.LoadScoresAndLives();

        gameOver = false;
        
    }

}

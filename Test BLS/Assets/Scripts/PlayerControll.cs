using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public InputMenu inputMenu;

    [SerializeField] float speed = 2f;
    [SerializeField] float screenBoundsOffset = 0.5f;

    [SerializeField] GameplayManager gameplayManager;

    Vector2 moveDirection;
    Vector2 screenBounds;

    Animator anim;

    public bool beenCollision;

    void Awake()
    {
        inputMenu = new InputMenu();
        inputMenu.PlayerInput.Movement.started += ctx => PlayerMove(ctx.ReadValue<Vector2>());
        inputMenu.PlayerInput.Movement.canceled += ctx => PlayerMove(ctx.ReadValue<Vector2>());
    }

    private void Start()
    {
        anim = GetComponent<Animator>();

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

        if(moveDirection.y > 0)
        {
            anim.Play("playerPlane_fly_up");
        }
        else if(moveDirection.y < 0)
        {
            anim.Play("playerPlane_fly_down");
        }
        else
        {
            anim.Play("playerPlane_fly_forward");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyPlane" && !beenCollision)
        {
            anim.Rebind();
            anim.Update(0);
            anim.Play("playerPlane_collision");
            gameplayManager.RemoveLive();

            beenCollision = true;
        }
    }

    public void BeenCollision()
    {
        beenCollision = false;
    }
}

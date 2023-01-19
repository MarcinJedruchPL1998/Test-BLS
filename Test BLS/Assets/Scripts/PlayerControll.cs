using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public InputMenu inputMenu;

    [SerializeField] float speed = 2f;
    [SerializeField] float screenBoundsOffset = 0.5f;

    Vector2 moveDirection;
    Vector2 screenBounds;

    void Awake()
    {
        inputMenu = new InputMenu();
        inputMenu.PlayerInput.Movement.started += ctx => PlayerMove(ctx.ReadValue<Vector2>());
        inputMenu.PlayerInput.Movement.canceled += ctx => PlayerMove(ctx.ReadValue<Vector2>());
    }

    private void Start()
    {
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
            GetComponent<Animator>().Play("playerPlane_fly_up");
        }
        else if(moveDirection.y < 0)
        {
            GetComponent<Animator>().Play("playerPlane_fly_down");
        }
        else
        {
            GetComponent<Animator>().Play("playerPlane_fly_forward");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    [SerializeField] float speed;

    public GameplayManager gameplayManager;

    Animator anim;
    BoxCollider2D bc2d;

    void Start()
    {
        int randomAnim = Random.Range(1, 3);
        anim = GetComponent<Animator>();
        anim.Play("enemyPlane" + randomAnim + "_fly");

        bc2d = GetComponent<BoxCollider2D>();

    }

    
    void Update()
    {
        transform.Translate(-Vector3.right * speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bullet" || collision.gameObject.tag == "PlayerPlane")
        {
            bc2d.enabled = false;
            anim.Play("enemyPlane_explosion");

            gameplayManager.AddPoint();
        }

        if(collision.gameObject.tag == "destroyBorder")
        {
            DestroyPlane();
        }
    }

    public void DestroyPlane()
    {
        Destroy(gameObject);
    }
}

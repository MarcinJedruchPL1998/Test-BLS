using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    public PlayerControll playerControll;

    [SerializeField] float speed;

    Animator anim;
    BoxCollider2D bc2d;

    void Start()
    {
        //Randomize color of enemy plane

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

            playerControll.AddPoint(10); //Give 10 points to player for shooting down the enemy plane
        }

        if(collision.gameObject.tag == "destroyBorder")
        {
            DestroyPlane(); //Destroy enemy plane if it's beyond the left border of the screen
        }
    }

    public void DestroyPlane()
    {
        Destroy(gameObject);
    }
}

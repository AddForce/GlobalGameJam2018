using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{

    public float speed = 1.0f;
    public LayerMask impassibleLayer;
    public float checkerRadius = 0.05f;

    private bool facingRight = true;

    private bool moveTop = true;
    private bool moveBack = true;
    private bool moveBot = true;
    private bool moveFront = true;

    Transform top;
    Transform back;
    Transform bottom;
    Transform front;

    private void Awake()
    {
        top = transform.Find("Top");
        back = transform.Find("Back");
        bottom = transform.Find("Bottom");
        front = transform.Find("Front");
    }

    private void Update()
    {
        float hMove = Input.GetAxis("Horizontal");
        float vMove = Input.GetAxis("Vertical");

        //moveTop = !Physics2D.OverlapCircle(top.position, checkerRadius, impassibleLayer);
        //moveBot = !Physics2D.OverlapCircle(bottom.position, checkerRadius, impassibleLayer);
        //moveFront = !Physics2D.OverlapCircle(front.position, checkerRadius, impassibleLayer);
        //moveBack = !Physics2D.OverlapCircle(back.position, checkerRadius, impassibleLayer);


        if (hMove > 0) {
            if (moveFront) {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }

            if (!facingRight)
            {
                Flip();
            }
        }
        else if (hMove < 0)
        {
            if (moveBack)
            {
                transform.Translate(-Vector3.right * speed * Time.deltaTime);
            }
            if (facingRight)
            {
                Flip();
            }
        }

        if (vMove > 0 && moveTop) {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (vMove < 0 && moveBot)
        {
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        Transform swap = front;
        front = back;
        back = swap;
        facingRight = !facingRight;
    }
}

using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {


    private ManaMan mana;

    public float speed = 1.0f;
    public LayerMask impassibleLayer;
    public float checkerRadius = 0.05f;

    private bool facingRight = true;

    public float usableMana;

    #region TODO
    private bool moveTop = true;
    private bool moveBack = true;
    private bool moveBot = true;
    private bool moveFront = true;

    Transform top;
    Transform back;
    Transform bottom;
    Transform front;

    #endregion TODO

    private void Awake() {
        mana = GetComponent<ManaMan>();
    }

    private void Update() {
        useSpell();
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

            if (!facingRight) {
                Flip();
            }
        } else if (hMove < 0) {
            if (moveBack) {
                transform.Translate(-Vector3.right * speed * Time.deltaTime);
            }
            if (facingRight) {
                Flip();
            }
        }

        if (vMove > 0 && moveTop) {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        } else if (vMove < 0 && moveBot) {
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void useSpell() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            print("using spell");
            mana.castSpell(10);
        }
    }

    private void Flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        Transform swap = front;
        facingRight = !facingRight;
    }
}

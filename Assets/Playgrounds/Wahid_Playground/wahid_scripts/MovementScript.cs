using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class MovementScript : MonoBehaviour {
    public float smoothTime = 4;
    public float speed = 1.0f;
    public LayerMask impassibleLayer;
    public float checkerRadius = 0.05f;
    private ManaMan mana;

    private Rigidbody rb;
    private bool facingRight = true;
    private SpriteRenderer spriteRend;
    private float startTime;

    Vector3 newPosition;

    public Vector3 velocity = Vector3.zero;
    int layerMask;

    private bool canMove = true;

    void Awake() {
        layerMask = ~(1 << LayerMask.NameToLayer("layerX"));
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        newPosition = transform.position;
        mana = GetComponent<ManaMan>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        startTime = Time.time;
    }

    void Update() {
        if (canMove) {
            clickAndMove();
            keyboardMove();
        }
    }

    private void keyboardMove() {
        float hMove = Input.GetAxis("Horizontal");
        float vMove = Input.GetAxis("Vertical");

        if (hMove > 0) {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            Flip(false);
        } else if (hMove < 0) {
            transform.Translate(-Vector3.right * speed * Time.deltaTime);
            Flip(true);
        }

        if (vMove > 0) {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        } else if (vMove < 0) {
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        }
    }

    private bool isMoving = false;
    float lerpTime = 1f;
    float currentLerpTime;
    Vector3 startPos;
    float moveDistance = 10f;

    private void clickAndMove() {
        if (Input.GetMouseButton(0)) {
            currentLerpTime = 0;
            startPos = transform.position;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                newPosition = hit.point;
                newPosition.y = transform.position.y;
            }
            isMoving = true;
        }

        if (isMoving) {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
            transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, newPosition) < 0.1f) {
                isMoving = false;
            }
        }
    }

    public void stopMove() {
        canMove = false;
    }

    public void startMove() {
        canMove = true;
    }

    public bool getMove() {
        return canMove;
    }
    private void Flip(bool facingRight) {
        spriteRend.flipX = facingRight;
    }
}

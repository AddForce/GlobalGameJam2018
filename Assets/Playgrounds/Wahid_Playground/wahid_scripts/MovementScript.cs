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

    private Transform spawnPoint;

    void Awake() {
        foreach (Transform child in this.transform) if (child.CompareTag("SpawnPoint")) { spawnPoint = child; }
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
        spawnLookAt();
        if (canMove) {
            clickAndMove();
            //keyboardMove(); NOT TODAY!
        }
    }

    public float pushbackForce = 3;
    private bool hasTouchedWall = false;
    private void OnCollisionEnter(Collision collision) {
        if (!hasTouchedWall) {
            hasTouchedWall = true;
            if (collision.transform.CompareTag("Wall")) {
                print("touching wal");
                rb.velocity = Vector3.zero;
                stopMove();
                Invoke("startMove", 1);
            }
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.transform.CompareTag("Wall")) {
            hasTouchedWall = false;
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


    private void spawnLookAt() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray)) {
            //   Instantiate(this, transform.position, transform.rotation);
            spawnPoint.rotation = Quaternion.LookRotation(newPosition + Vector3.up);
        }
    }
        //    if (Physics.Raycast(ray, out hit)) {
        //  //  if (hit.collider.CompareTag("Ground")) {
        //        spawnPoint.rotation = Quaternion.LookRotation(newPosition + Vector3.up);
        //    //}
        //}

    private void clickAndMove() {
        if (Input.GetMouseButton(0)) {

            currentLerpTime = 0;
            startPos = transform.position;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.CompareTag("Ground")) {
                    newPosition = hit.point;
                    newPosition.y = transform.position.y;

                }
            }
            if (canMove) {
                isMoving = true;
            }
        }
        Vector3 rayOrigin = transform.position;
        RaycastHit normalhit;

        if (Physics.Raycast(rayOrigin, newPosition, out normalhit, 10)) {
            Debug.DrawRay(rayOrigin, newPosition * 10, Color.red);
        }

        if (isMoving) {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
            transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * speed);
            print("moving");
            if (Vector3.Distance(transform.position, newPosition) < 0.1f) {
                isMoving = false;
            }
        } else {
            newPosition = transform.position;
        }

        if (Input.GetMouseButtonUp(0)) {
            isMoving = false;
        }
    }

    public void stopMove() {
        isMoving = false;
        canMove = false;
    }

    public void startMove() {
        isMoving = true;
        canMove = true;
    }

    public bool getMove() {
        return canMove;
    }
    private void Flip(bool facingRight) {
        spriteRend.flipX = facingRight;
    }
}

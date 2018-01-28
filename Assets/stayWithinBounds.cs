using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stayWithinBounds : MonoBehaviour {
    private float rightBound;
    private float leftBound;
    private float topBound;
    private float bottomBound;
    private Vector3 pos;
    private Transform target;
    private Renderer planeBound;

    // Use this for initialization
    void Start() {
        float vertExtent = GetComponent<Camera>().orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;
        planeBound = GameObject.FindGameObjectWithTag("Ground").GetComponent<Renderer>();
        target = GameObject.FindWithTag("Player").transform;
        leftBound = (float)(horzExtent - planeBound.bounds.size.x / 2.0f);
        rightBound = (float)(planeBound.bounds.size.x / 2.0f - horzExtent);
        bottomBound = (float)(vertExtent - planeBound.bounds.size.y / 2.0f);
        topBound = (float)(planeBound.bounds.size.y / 2.0f - vertExtent);
    }

    // Update is called once per frame
    void Update() {
        var pos = new Vector3(target.position.x, target.position.y, transform.position.z);
        //pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
        //pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);
        transform.position = pos;
    }
    void OnLevelWasLoaded() {
        Start();
    }
}
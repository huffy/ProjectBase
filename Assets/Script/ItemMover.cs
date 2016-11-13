using UnityEngine;
using System.Collections;

public class ItemMover : MonoBehaviour {

    private LayerMask layerMask;
    private bool canMove;
    private bool canBuild = true;
    private Ray2D ray;
    private RaycastHit2D hit;
    private Material material;

    public float offset;

    // Use this for initialization
    void Start () {
        layerMask = 1 << 8;
        material = GetComponent<SpriteRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
       

    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer==10)
            canBuild = false;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == 10)
            canBuild = true;
    }

    void OnMouseDown() {
        if (!canMove)
        {
            canMove = true;
        }
        else {
            if (canBuild) {
                canMove = false;
            }
        } 
    }


    void FixedUpdate() {
        if (canMove) {
            FollowMouseMove();
            CheckColor();
        }
    }

    void CheckColor() {
        if (canBuild) material.color = Color.white;
        else material.color = Color.red;
    }

    void FollowMouseMove() {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            transform.position = new Vector2(hit.point.x, offset);
        }
    }
}


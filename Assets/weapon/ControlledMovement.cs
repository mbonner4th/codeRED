using UnityEngine;
using System.Collections;

public class ControlledMovement : MonoBehaviour {

    private int playerNum = 0;
    private string up;
    private string horizontal;
    private string down;
    private int rotation;
    Quaternion upAngle;
    Quaternion leftAngle;
    Quaternion downAngle;
    Quaternion rightAngle;
    private bool takingInput = true;
    private Transform theParent;
    public float maxX = 25;
    public float minX = -25;
    public float maxY = 30;
    public float minY = -10;

    void Awake() {
        upAngle.w = 0.7f; upAngle.x = 0; upAngle.y = 0; upAngle.z = 0.7f;
        leftAngle.w = 0; leftAngle.x = 0; leftAngle.y = 1; leftAngle.z = 0;
        downAngle.w = 0.7f; downAngle.x = 0; downAngle.y = 0; downAngle.z = -0.7f;
        rightAngle.w = 0; rightAngle.x = 0; rightAngle.y = 0; rightAngle.z = 0;
        if (transform.rotation.y == 1.0f)
        {
            goHorizontal(-1.0f);
        }
        else
        {
            goHorizontal(1.0f);
        }
    }

	// Update is called once per frame
	void Update () {
        if (playerNum == 1) {
            SetP1Input();
        } else if (playerNum == 2) {
            SetP2Input();
        }
        if (takingInput) {
            if (Input.GetButtonDown(up)) {
                goUp();

            } else if (Input.GetButtonDown(down)) {
                goDown();

            } else if (Input.GetAxis(horizontal) != 0) {
                goHorizontal(Input.GetAxis(horizontal));
            }
        }
        if (theParent == null)
        {
            takingInput = false;
        }

        if (transform.position.x > maxX || transform.position.x < minX)
        {
            Destroy(this.gameObject);
        }

        if (transform.position.y > maxY || transform.position.y < minY)
        {
            Destroy(this.gameObject);
        }
    }

    private void goUp() {
        Vector3 up;
        up.x = 0;
        up.z = 0;
        up.y = 15;
        transform.GetComponent<Rigidbody2D>().velocity = up;
        transform.rotation = upAngle;
    }

    private void goDown() {
        Vector3 down;
        down.x = 0;
        down.z = 0;
        down.y = -15;
        transform.GetComponent<Rigidbody2D>().velocity = down;
        transform.rotation = downAngle;
    }

    private void goHorizontal(float axis) {
        if (axis < 0) {
            Vector3 left;
            left.x = -15;
            left.z = 0;
            left.y = 0;
            transform.GetComponent<Rigidbody2D>().velocity = left;
            transform.rotation = leftAngle;

        } else if (axis > 0) {
            Vector3 right;
            right.x = 15;
            right.z = 0;
            right.y = 0;
            transform.GetComponent<Rigidbody2D>().velocity = right;
            transform.rotation = rightAngle;

        }
    }

    private void SetP1Input() {
        up = "Jump1";
        horizontal = "Horizontal1";
        down = "Down1";
    }

    private void SetP2Input() {
        up = "Jump2";
        horizontal = "Horizontal2";
        down = "Down2";
    }

    public void setPlayerNum(int playerNum) {
        this.playerNum = playerNum;
    }

    public void setParent(Transform parent)
    {
        theParent = parent;
    }
}

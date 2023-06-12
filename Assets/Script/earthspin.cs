using UnityEngine;
using System.Collections;

public class earthspin : MonoBehaviour {
    public float speed = 10f;//spin speed

    void Update() {
        transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World);//use rotation method to rotate earth.
    }
}
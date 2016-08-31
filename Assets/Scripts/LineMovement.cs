using UnityEngine;
using System.Collections;
using System;

public class LineMovement : MonoBehaviour, ITimeChanging {

    [SerializeField]
    Vector3 direction = Vector3.up;
    [SerializeField]    
    float speed = 0.2f;

    public void AddTime(float dt)
    {
        transform.position += dt * speed * direction.normalized;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

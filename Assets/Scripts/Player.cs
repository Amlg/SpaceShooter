using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private float _speed = 3.5f;
    //test commit change
    void Start()
    {
        //take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //new Vector3(1, 0, 0); < same thing as Vector3.right
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
        
    }
}
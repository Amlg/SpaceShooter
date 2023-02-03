using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _rotationSpeed = 20f;
    private Player _player;
    [SerializeField]
    private GameObject _explosionPrefab;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate object on z axis
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        
    }
    // check for laser collision (trigger)

    //instantiate explosion at the position of asteroid

    //destroy explosion after 3 sec

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

        }
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.5f);
        }
    }
}

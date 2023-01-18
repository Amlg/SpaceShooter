using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleshotPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedBoostActive = false;
    [SerializeField]
    private bool _isShieldBoostActive = false;
    //variable reference to the shield visualiser
    [SerializeField]
    private GameObject _shieldVisualizer;
    private SpawnManager _spawnManager;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
    }

    void Update()
    {
        CalculateMovement();
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        
        transform.Translate(direction * _speed * Time.deltaTime);
        
        //moving to the edge of the screen logic
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }

    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
       
        if (_isTripleShotActive)
        {
            Instantiate(_tripleshotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }
    
    public void Damage()
    {
        if(_isShieldBoostActive)
        {
            _isShieldBoostActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        
        _lives--;

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
        
    }
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    public void ShieldBoostActive()
    {
        _isShieldBoostActive = true;
        StartCoroutine(ShieldBoostPowerDownRoutine());
        _shieldVisualizer.SetActive(true);
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }
    IEnumerator ShieldBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _isShieldBoostActive = false;
        _shieldVisualizer.SetActive(false);
    }

    public void AddScore(int points)
    {
        _score = _score + points;
        _uiManager.UpdateScore(_score);
    }
}

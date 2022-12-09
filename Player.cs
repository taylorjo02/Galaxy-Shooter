using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private float _speedMultiplier = 2;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.25f;

    private float _canFire = -1;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _isTripleShotEnabled = false;

    [SerializeField]
    private bool _isSpeedBoostActive = false;

    [SerializeField]
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _shieldVisualizer;

    private int _playerScore;

    private UIManager _uiManager;

    [SerializeField]
    private GameObject _leftEngine, _rightEngine;

    [SerializeField]
    private AudioClip _laserSound;

    [SerializeField]
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // determines the starting position of the player when the game is launched (x,y,z values)
        transform.position = new Vector3(0, -1.8f, 0);

        // Allows the Player Gameobject to find the Spawn Manager game object in order to call the On Player Death method to stop spawning enemies.
        _spawnManager = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnManager>();

        //Finds the UI Manager Gameobject
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        //Finds the AudioSource
        _audioSource = GetComponent<AudioSource>();


        // Performs a null check to make sure the player can communicate with the spawn manager, and UI Manager and prints an error message if unsuccessful.
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource on the player is NULL.");
        }
        else
        {
            _audioSource.clip = _laserSound;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Calls this method to control player movement per frame
        CalculateMovement();

        // Checks if real game time is greater than the cooldown variable, and if so, the laser can fire
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal");
        float verticalSpeed = Input.GetAxis("Vertical");

        //Controls movement speed of player gameobject on both axes, with the _speed variable * real time & 1 frame per second
        transform.Translate(Vector3.right * horizontalSpeed * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalSpeed * _speed * Time.deltaTime);


        // Sets boundaries on both axes for the player object, and allows player to wrap from one side of screen to the other
        if (transform.position.y >= 5.5f)
        {
            transform.position = new Vector3(transform.position.x, 5.5f, 0);
        }
        else if (transform.position.y <= -3.65f)
        {
            transform.position = new Vector3(transform.position.x, -3.65f, 0);
        }

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    //Method for determining fire rate of laser, and creates the laser with positioning on the player gameobject
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        // Creates the laser at the offset desired for best visual effect
        if (_isTripleShotEnabled == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }

        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        //Plays the laser sound effect
        _audioSource.Play();
    }

    //Controls damage to the player, and checks if the player is dead. Is called from the Enemy gameobject
    public void Damage()
    {

        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        //Subtracts lives as player takes damage
        _lives--;

        if (_lives == 2)
        {
            _rightEngine.gameObject.SetActive(true);
        }

        else if (_lives == 1)
        {
            _leftEngine.gameObject.SetActive(true);
        }

        //Calls the UI Manager method to update the lives on the display image
        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotEnabled = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        while (_isTripleShotEnabled == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotEnabled = false;
        }
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    private IEnumerator SpeedBoostPowerDownRoutine()
    {
        while (_isSpeedBoostActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isSpeedBoostActive = false;
            _speed /= _speedMultiplier;
        }
    }

    public void ShieldsActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddToPlayerScore(int points)
    {
        _playerScore += points;
        _uiManager.UpdateScoreUI(_playerScore);
    }
}

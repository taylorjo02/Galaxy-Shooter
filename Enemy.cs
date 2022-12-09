using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.5f;

    private Player _player;

    private Animator _anim;

    [SerializeField]
    private GameObject _explosion;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y < -4.0f)
        {
            float randomX = Random.Range(-9.0f, 9.0f);
            transform.position = new Vector3(randomX, 7.0f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Stores Player information in a variable for null check on the Player component
            Player player = other.transform.GetComponent<Player>();

            //Checks if gameobject collided with has a Player component, then calls the Damage method. Provides error handling. 
            if (player != null)
            {
                player.Damage();
            }

            //Trigger destruction animation
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _enemySpeed = 0;
            Destroy(this.gameObject);
        }

        //Checks for collision with laser, and destroys laser object and then itself
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddToPlayerScore(Random.Range(10, 21));
            }

            //Trigger destruction animation
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _enemySpeed = 0;
            Destroy(this.gameObject);
        }
    }
}

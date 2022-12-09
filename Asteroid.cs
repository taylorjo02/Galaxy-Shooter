using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 20.0f;

    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private SpawnManager _spawnManager;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            //Create explosion on asteroid
            Instantiate(_explosion, transform.position, Quaternion.identity);

            Destroy(other.gameObject);

            //Call method to start spawning enemies and powerups
            _spawnManager.StartSpawning();

            Destroy(this.gameObject, 0.25f);

        }
    }
}

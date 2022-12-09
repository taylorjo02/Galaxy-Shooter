using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    //Integer system to identify different powerups by number to reuse this script for all of them
    [SerializeField]
    private int _powerUpID; //0 = Triple Shot, 1 = Speed Boost, 2 = Shields

    [SerializeField]
    private AudioClip _powerupAudio;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.0f)
        {
            Destroy(this.gameObject);
        }

    }

    // Method to handle collision of the powerup with the player through a null check and then destroy the object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_powerupAudio, transform.position);

            if (player != null)
            {
                switch(_powerUpID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8.0f;
    
    // Update is called once per frame
    void Update()
    {
        //Sets direction of the laser and the laser speed when fired
        float verticalSpeed = Input.GetAxis("Vertical");
        transform.Translate(_laserSpeed * Time.deltaTime * Vector3.up);

        //Checks if laser is off screen, and destroys object if condition is met
        if (transform.position.y > 8f)
        {
            // Check if gameobject has a parent and delete it once used
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }
}

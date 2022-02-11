using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    private GameObject focalPoint;
    private Rigidbody playerRB;

    [Header("Power up values")]
    public GameObject powerUpIndicator;
    public bool hasPowerUp = false;
    private float powerUpStrength = 15f;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        focalPoint = GameObject.FindGameObjectWithTag("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (hasPowerUp)
        {
            HandlePowerUpIndicator();
        }

        if (transform.position.y < -10)
        {
            GameManager.instance.isGameOver = true;
            Destroy(gameObject);
        }
    }

    // Moves forward or backwards depending on the focal point rotation
    void Movement()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            StartCoroutine(PowerUpCountdown());
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 repulsion = (collision.gameObject.transform.position - transform.position);

            // Makes an impulse to the enemy in the direction of the collision taking in mind the strength of the power up
            enemyRB.AddForce(repulsion * powerUpStrength, ForceMode.Impulse);
        } 
    }


    // If the player has the power up it shows the power up indicator and rotates it
    private void HandlePowerUpIndicator()
    {
        powerUpIndicator.SetActive(true);
        powerUpIndicator.transform.position = transform.position;
    }

    IEnumerator PowerUpCountdown()
    {
        yield return new WaitForSeconds(5);
        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
    }
}

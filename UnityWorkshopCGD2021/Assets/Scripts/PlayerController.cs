using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    CharacterController player;

    // Variables for movement
    Vector3 movement;
    [SerializeField] float speed;
    [SerializeField] Transform groundCheck; //Potentially make it an array
    [SerializeField] GameObject gun;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        MovePlayer();
    }

    void CheckGrounded()
    {
        // Checks if grounded
        isGrounded = Physics.OverlapSphere(groundCheck.position, 0.1f, LayerMask.GetMask("Ground")).Length > 0;
    }

    void MovePlayer()
    {
        // Sets the movement vector with input
        movement = ((transform.right * Input.GetAxisRaw("Horizontal")) + (transform.forward * Input.GetAxisRaw("Vertical"))).normalized;

        // Move the player and apply gravity when not grounded
        player.Move(movement * speed * Time.deltaTime);
        if (!isGrounded)
        {
            player.Move(Vector3.up * -9.81f * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement_Manager : MonoBehaviour
{
    public VariableJoystick joystick;
    public CharacterController controller;
    public float movementSpeed;
    public float rotationSpeeed;

    public Canvas inputCanvas;
    public bool isJoystick;

    public Animator playerAnimator;

    public float rotationSpeed = 10f; // Adjust the rotation speed as needed

    public bool enabledJoystickInput = true;
    public bool enabledMovement = true;

    private void Start()
    {
        enableJoystickInput();
    }

    public void enableJoystickInput()
    {
        isJoystick = true;
        inputCanvas.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (isJoystick && enabledJoystickInput)
        {
            var movemovementDirection = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);
            controller.SimpleMove(movemovementDirection * movementSpeed);

            if (movemovementDirection.sqrMagnitude <= 0)
            {
                playerAnimator.SetBool("run", false);
                return;
            }

            playerAnimator.SetBool("run", true);
            var targetDirection = Vector3.RotateTowards(controller.transform.forward, movemovementDirection, rotationSpeeed * Time.deltaTime,0.0f);

            // Set the x component of targetDirection to 0 to prevent rotation on the x-axis
            targetDirection.y = 0.0f;

            controller.transform.rotation = Quaternion.LookRotation(targetDirection);

            // Ensure the character's position stays at y = 0
            controller.transform.position = new Vector3(controller.transform.position.x, 0.0f, controller.transform.position.z);

        }
    }

    public void RotatePlayerTowards(Vector3 direction)
    {

        if (direction != Vector3.zero)
        {
            // Calculate the target rotation only on the y-axis
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            // Use Mathf.SmoothDamp for smoother rotation
            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Ensure the player is facing the correct direction
            controller.transform.forward = direction.normalized;

            // Reset the x-axis rotation to zero
            Vector3 eulerAngles = controller.transform.rotation.eulerAngles;
            eulerAngles.x = 0;
            controller.transform.rotation = Quaternion.Euler(eulerAngles);
        }
    }
}



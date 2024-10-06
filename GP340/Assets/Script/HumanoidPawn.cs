using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidPawn : Pawn
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Get the animator attached to this object
        animator = GetComponent<Animator>();
    }

    public override void Move(Vector3 direction)
    {
        // Apply the speed to the direction vector by muliplying by our max speed
        
        direction = transform.InverseTransformDirection(direction);
        direction *= maxMoveSpeed;

        // Send the values from the direction in to the animator
        animator.SetFloat("Forward", direction.z);
        animator.SetFloat("Right", direction.x);
    }

    public override void Rotate(float speed)
    {
        // Use the Rotate function to rotate based on speed
        transform.Rotate(0, speed * maxRotationSpeed * Time.deltaTime, 0);
    }

    public override void RotateToLookAt(Vector3 targetPoint)
    {
        // Find the vector from our position to the target point
        Vector3 lookVector = targetPoint - transform.position;

        // Find the rotation that will look down that vector with world up being the up direction
        Quaternion lookRotation = Quaternion.LookRotation(lookVector, Vector3.up);

        // Rotate slightly towards that target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, maxRotationSpeed * Time.deltaTime);
    }
}
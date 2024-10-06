using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    public bool isMouseRotation;

    protected override void Update()
    {
        base.Update();
    }

    protected override void MakeDecisions()
    {
        // Get the Input axes
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Limit the vector to a max magnitude of 1
        moveVector = Vector3.ClampMagnitude(moveVector, 1);

        // Tell the pawn to move
        pawn.Move(moveVector);

        // if we are mouse rotating
        if (isMouseRotation)
        {
            // Create a ray from the mouse position to the world from the camera
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Create a 2D plane at our feet facing +Y
            Plane footPlane = new Plane(Vector3.up, pawn.transform.position);

            // Find the distance of the ray
            float distanceToIntersect;
          
            if (footPlane.Raycast(mouseRay, out distanceToIntersect))
            {
                // Find the intersection point
                Vector3 intersectionPoint = mouseRay.GetPoint(distanceToIntersect);

                // Look at the intersection point
                pawn.RotateToLookAt(intersectionPoint);
            }
            else
            {
                Debug.Log("Camera is not looking at the ground - no intersection between plane and ray");
            }
        }
        else
        {
            // Rotate based on rotation axis
            pawn.Rotate(Input.GetAxis("CameraRotation"));
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public Transform[] lanes; // Array of lane GameObjects
    public float laneChangeSpeed = 5f; // Speed of lane change

    public Transform player; // Reference to the player object

    private int currentLaneIndex = 1; // Initial lane index of the player

    private void Update()
    {
        // Detect swipe input
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Moved)
            {
                float swipeDelta = touch.deltaPosition.x;

                // Determine swipe direction
                int targetLaneIndex = currentLaneIndex;
                if (swipeDelta < 0) // Swipe left
                    targetLaneIndex--;
                else if (swipeDelta > 0) // Swipe right
                    targetLaneIndex++;

                // Clamp the target lane index within the valid range
                targetLaneIndex = Mathf.Clamp(targetLaneIndex, 0, lanes.Length - 1);

                // Move the player to the target lane
                MoveToLane(targetLaneIndex);
            }
        }
    }

    private void MoveToLane(int laneIndex)
    {
        // Update the current lane index
        currentLaneIndex = laneIndex;

        // Move the player to the target lane by setting the X-position to the desired point within the lane
        Vector3 targetPosition = player.position;
        targetPosition.x = lanes[laneIndex].position.x;
        // Example of smooth movement:
        //player.position = Vector3.Lerp(player.position, targetPosition, Time.deltaTime * laneChangeSpeed);
        // Example of direct positioning:
        player.position = targetPosition;
    }
}



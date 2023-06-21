using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public Transform[] lanes; // Array of lane GameObjects
    public float laneChangeSpeed = 5f; // Speed of lane change
    public int threshold = 10; // Threshold of swipe strength

    public float swipeBufferTime = 0.5f; // Cooldown time between lane changes
    private bool canSwipe = true; // Flag to track if a swipe is currently allowed

    public Transform player; // Reference to the player object

    private int currentLaneIndex = 1; // Initial lane index of the player

    private void Update()
    {
        // Detect swipe input
        if (canSwipe && Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Moved)
            {
                float swipeDelta = touch.deltaPosition.x;
                if (Mathf.Abs(swipeDelta) >= threshold)
                {
                    // Process the swipe
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

                    // Apply buffer - Disable swiping temporarily
                    StartCoroutine(EnableSwipeBuffer());
                }
                
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

    private IEnumerator EnableSwipeBuffer()
    {
        canSwipe = false; // Disable swiping

        yield return new WaitForSeconds(swipeBufferTime);

        canSwipe = true; // Enable swiping again
    }
}



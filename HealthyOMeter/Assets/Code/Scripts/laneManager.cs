using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public Transform[] Lanes;
    public float LaneChangeSpeed = 5f;
    public int Threshold = 10;

    public float SwipeBufferTime = 0.5f;
    private bool CanSwipe = true;

    public Transform Player;

    private int CurrentLaneIndex = 1;
    private Vector3 TargetPosition;

    //Declaration of Character Movement SFX
    [SerializeField] private AudioSource LeftMovementEffect;
    [SerializeField] private AudioSource RightMovementEffect;

    private void Start()
    {
        if (Lanes.Length == 0)
        {
            UnityEngine.Debug.LogError("Lanes array is empty. Please assign lanes in the Inspector.");
            this.enabled = false;
            return;
        }
        TargetPosition = Player.position;
        StartCoroutine(DetectSwipe());
        StartCoroutine(SmoothMoveToLane());
    }

    private IEnumerator DetectSwipe()
    {
        while (true)
        {
            if (CanSwipe && Input.touches.Length > 0)
            {
                Touch touch = Input.touches[0];
                if (touch.phase == TouchPhase.Moved)
                {
                    float swipeDelta = touch.deltaPosition.x;
                    if (Mathf.Abs(swipeDelta) >= Threshold)
                    {
                        int TargetLaneIndex = CurrentLaneIndex;
                        if (swipeDelta < 0)
                        {
                            TargetLaneIndex--;
                            LeftMovementEffect.Play();
                        } 
                        else if (swipeDelta > 0)
                        {
                            TargetLaneIndex++;
                            RightMovementEffect.Play();
                        }

                        TargetLaneIndex = Mathf.Clamp(TargetLaneIndex, 0, Lanes.Length - 1);

                        MoveToLane(TargetLaneIndex);
                        StartCoroutine(EnableSwipeBuffer());
                        yield return new WaitUntil(() => CanSwipe);
                    }
                }
            }
            yield return null;
        }
    }

    private void MoveToLane(int laneIndex)
    {
        CurrentLaneIndex = laneIndex;
        TargetPosition = Player.position;
        TargetPosition.x = Lanes[laneIndex].position.x;
    }

    private IEnumerator SmoothMoveToLane()
    {
        while (true)
        {
            Player.position = Vector3.MoveTowards(Player.position, TargetPosition, LaneChangeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator EnableSwipeBuffer()
    {
        CanSwipe = false;
        yield return new WaitForSeconds(SwipeBufferTime);
        CanSwipe = true;
    }
}

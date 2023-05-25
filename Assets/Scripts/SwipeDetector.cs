using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    public static event OnSwipeInput SwipeEvent;
    public delegate void OnSwipeInput(Vector3 direction);

    private Vector2 swipeStart;
    [SerializeField]
    private Vector2 swipeDelta;

    public float delta = 80f;

    private bool isSwiping;
    private bool isMobile;

    // Start is called before the first frame update
    void Start()
    {
        isMobile = Application.isMobilePlatform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                swipeStart = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
                ResetSwipe();
        }
        else
        {
            if (Input.touchCount > 0)
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    isSwiping = true;
                    swipeStart = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended ||
                         Input.GetTouch(0).phase == TouchPhase.Canceled)
                    ResetSwipe();
        }

        CheckSwipe();
    }

    private void CheckSwipe()
    {
        //swipeDelta = Vector2.zero;

        if (isSwiping)
            if (!isMobile && Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - swipeStart;
            else if (Input.touchCount > 0)
                swipeDelta = Input.GetTouch(0).position - swipeStart;

        if (swipeDelta.magnitude > delta)
            if (SwipeEvent != null)
            {
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                    SwipeEvent(swipeDelta.x > 0 ? Vector3.right : Vector3.left);
                //else
                    //SwipeEvent(swipeDelta.y > 0 ? Vector3.up : Vector3.down);
                Debug.Log("Shift");
            }

        //ResetSwipe();
    }

    private void ResetSwipe()
    {
        isSwiping = false;

        swipeDelta = Vector2.zero;
        swipeStart = Vector2.zero;
    }
}

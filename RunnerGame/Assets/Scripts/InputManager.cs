using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public enum Direction { Left, Up, Right, Down, None }

    Direction direction;
    Vector2 startPos, endPos;
    bool draggingStarted;

    private float _speed = 5.0f;
    private float _acceleration = 0.2f;
    private float _jumpForce = 10f;

    //////
    public float swipeThreshold = 100f;
    public GameObject playerObj;
    public Vector3 dir;
    public bool jump;
    //////

    private void Awake()
    {
        draggingStarted = false;
        direction = Direction.None;
        jump = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggingStarted = true;
        startPos = eventData.pressPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggingStarted)
        {
            endPos = eventData.position;

            Vector2 difference = endPos - startPos; // difference vector between start and end positions.

            if (difference.magnitude > swipeThreshold)
            {
                if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y)) // Do horizontal swipe
                {
                    direction = difference.x > 0 ? Direction.Right : Direction.Left; // If greater than zero, then swipe to right.
                }
                else //Do vertical swipe
                {
                    direction = difference.y > 0 ? Direction.Up : Direction.Down; // If greater than zero, then swipe to up.
                }
            }
            else
            {
                direction = Direction.None;
            }
        }
    }
    private void Update()
    {
        if (!GameManager.instance.gameOver)
        {
            _speed += _acceleration * Time.deltaTime;
            playerObj.transform.Translate(dir * _speed * Time.deltaTime);
            if(jump)
                playerObj.GetComponent<Rigidbody>().AddForce(Vector3.up * _jumpForce, ForceMode.Force);
        }
            
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingStarted && direction != Direction.None)
        {
            if (!GameManager.instance.gameOver)
                
                if(direction == Direction.Left)
                {                    
                    dir = new Vector3(-1, 0 , 0);
                }
                else if(direction == Direction.Right)
                {                   
                    dir = new Vector3(0, 0, 1);
                }
                else if(direction == Direction.Up)
                {
                    
                    if (Input.GetMouseButtonDown(0))
                    {
                        jump = true;
                    }
                    else
                    {
                        jump = false;
                    }
                }
   
            Debug.Log("Swipe direction: " + direction);
        }


        //reset the variables
        startPos = Vector2.zero;
        endPos = Vector2.zero;
        draggingStarted = false;
    }

}
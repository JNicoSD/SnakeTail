using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public PlayerManager playerManager;
    private PlayerObject playerObject;
    private Direction direction;
    private Transform prevTransform;
    //public PlayerTail playerTail;

    private Vector3 moveVertical, moveHorizontal;
    void Start()
    {
        playerObject = new PlayerObject(this.gameObject);

        //moveVertical = new Vector3(0,playerManager.moveSpeed * Time.deltaTime,0);
        //moveHorizontal = new Vector3(playerManager.moveSpeed * Time.deltaTime,0,0);

        direction = Direction.Right;
    }

    // Update is called once per frame
    void Update()
    {
        gb.transform.position = playerParts[0].transform.position;
        gb2.transform.position = playerParts[1].transform.position;
        PlayerMovement();
    }

    void PlayerMovement()
    {
        //gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(gameObject.transform.localEulerAngles), Quaternion.Euler(new Vector3(0,0,180)), 0.1f);
        if(Input.GetKey(KeyCode.W) && direction != Direction.Down)
        {
            prevTransform = playerParts[0].transform;
            UpdatePosition();
            gameObject.MoveForward(playerManager.moveSpeed, 90);
            direction = Direction.Up;
        }
        if(Input.GetKey(KeyCode.A) && direction != Direction.Right)
        {
            prevTransform = playerParts[0].transform;
            UpdatePosition();
            gameObject.MoveForward(playerManager.moveSpeed, -180);
            direction = Direction.Left;
        }
        if(Input.GetKey(KeyCode.S) && direction != Direction.Up)
        {
            prevTransform = playerParts[0].transform;
            UpdatePosition();
            gameObject.MoveForward(playerManager.moveSpeed, -90);
            direction = Direction.Down;
        }
        if(Input.GetKey(KeyCode.D) && direction != Direction.Left)
        {
            prevTransform = playerParts[0].transform;
            UpdatePosition();
            gameObject.MoveForward(playerManager.moveSpeed, 0);
            direction = Direction.Right;
        }
    }

    public List<GameObject> playerParts;
    
    public GameObject gb, gb2;
    public bool  isFollow, isTrail, isFollowBehind;
    public void UpdatePosition()
    {
        /*for(int i = playerParts.Count - 1; i > 0; i--)
        {
            //playerParts[i].transform.Translate(new Vector3(playerManager.moveSpeed * Time.deltaTime,0,0));
            //playerParts[i].transform.LookAt2D(playerParts[i-1].transform);
            playerParts[i].transform.FollowObject(playerParts[i-1].transform, 1f, playerManager.moveSpeed);
        }*/
        
        for(int i = 1; i < playerParts.Count; i++)
        {
            //playerParts[i].transform.LookAt2D(playerParts[i-1].transform.position);
            if(isFollow) playerParts[i].transform.FollowObject(playerParts[i-1], 0.5f, 1f, playerManager.moveSpeed);
            else if(isTrail) playerParts[i].transform.TrailBehind(playerParts[i-1], 1f, playerManager.moveSpeed);
            else if(isFollowBehind) playerParts[i].transform.FollowBehind(playerParts[i-1], 0f, playerManager.moveSpeed);
            //prevTransform = playerParts[i].transform;
        }
    }
}

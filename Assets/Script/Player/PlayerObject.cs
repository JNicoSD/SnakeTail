using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject
{
    private GameObject _player;
    public PlayerObject(GameObject player)
    {
        this._player = player;
    }

    /*public void Move(float rotationAngle, float movement)
    {
        this._player.transform.localEulerAngles = new Vector3(0,0, rotationAngle);
        //this._player.transform.Translate(new Vector3(movement * Time.deltaTime,0,0));
        this._player.transform.localPosition += this._player.transform.right * movement * Time.deltaTime;
    }*/
}

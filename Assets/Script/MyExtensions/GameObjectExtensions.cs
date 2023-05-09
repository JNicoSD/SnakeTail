using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{

    public static void MoveForward(this GameObject obj, float moveSpeed, float rotationAngle)
    {
        obj.transform.rotation = Quaternion.Lerp(Quaternion.Euler(obj.transform.localEulerAngles), Quaternion.Euler(new Vector3(0,0,rotationAngle)), 0.01f);
        //this._player.transform.Translate(new Vector3(movement * Time.deltaTime,0,0));
        obj.transform.localPosition += obj.transform.right * moveSpeed * Time.deltaTime;
    }
}

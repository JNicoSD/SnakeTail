using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    #region LookAt2D
    static float lookAngle;
    public static void LookAt2D(this Transform current, Vector3 target)
    {
        lookAngle = Mathf.Abs(Mathf.Atan((target.y - current.position.y) / (target.x - current.position.x)) * Mathf.Rad2Deg);
        
        if(current.position.x > target.x && current.position.y > target.y)
        {
            lookAngle = 180 + lookAngle;
        }
        else if(current.position.x > target.x)
        {
            lookAngle = 180 - lookAngle;
        }
        else if(current.position.y > target.y)
        {
            lookAngle = 360 - lookAngle;
        }

        current.rotation = Quaternion.Lerp(Quaternion.Euler(current.localEulerAngles), Quaternion.Euler(new Vector3(0, 0, lookAngle)), 0.05f);

        //current.localEulerAngles = new Vector3(0, 0, lookAngle);
    }
    #endregion

    #region FollowObject 
    public static void FollowObject(this Transform current, GameObject target, float targetPositionOffset, float gapBetweenObjects, float moveSpeed)
    {
        //*************************************************************************************************************// 
        // (target.transform.right * targetPositionOffset) is used to set the distance between the current and target  //
        // gapBetweenObjects = 0 will make the current object look at the center of target                             //
        // gapBetweenObjects = *scale of the object/2* will make the current object look at the back of the target     //
        //*************************************************************************************************************//
        current.LookAt2D(target.transform.position - (target.transform.right * targetPositionOffset));
        current.position += (current.right * moveSpeed * Time.deltaTime);
        
        bool isEqual = false;
        int iterations = 0;
        //Make sure that the Following objects stays withing the gap range set
        for(float i = 0.01f; isEqual == false; iterations++)
        {
            // Forwards the current object if the distance between it and the target is large
            if(Vector3.Distance(current.position, target.transform.position) > gapBetweenObjects + 0.01f)
            {
                current.position += (i * current.right);
            }
            // Move back the current object if the distance between it and the target is too little
            else if((Vector3.Distance(current.position, target.transform.position) < gapBetweenObjects - 0.01f))
            {
                current.position -= (i * current.right);
            }
            else // When the distance is equals to gapBetweenObjects+-0.01 
            {
                isEqual = true;
            }

            if(iterations > 100) break; // To prevent infinite loop since it happens sometimes.
        }
    }
    #endregion

    #region TrailBehind
    private static Vector3 target_top, target_down, target_back, current_front;
    public static void TrailBehind(this Transform current, GameObject target, float gapBetweenObjects, float moveSpeed)
    {
        target_back = target.transform.position - (target.transform.right * (target.transform.localScale.x/2));

        current.LookAt2D(target.transform.position);
        current.position = Vector3.Lerp(current.position, target_back, gapBetweenObjects/100f);
    }
    #endregion

    #region FollowBehind
    public static void FollowBehind(this Transform current, GameObject target, float gapBetweenObjects, float moveSpeed)
    {
        target_back = target.transform.position - (target.transform.right * (target.transform.localScale.x/2));
        current_front = current.transform.position + (current.transform.right * (current.transform.localScale.x/2));
       
        current.LookAt2D(target.transform.position);
        current.position += (current.right * moveSpeed * Time.deltaTime);

        // Make sure that the Following objects stays within the gap range set
        // Snaps the current to the back of target
        if(Vector3.Distance(current_front, target_back) > (0.01f + gapBetweenObjects) || Vector3.Distance(current_front, target_back) < (0.01f + gapBetweenObjects)) 
        {
            current.position = (target_back - (current.transform.right * (current.transform.localScale.x/2))) - (current.transform.right * gapBetweenObjects);
        }
    }
    #endregion
}

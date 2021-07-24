using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSoldier : MonoBehaviour
{
    public float moveSpeed = 2;
    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow)) movement += Vector3.forward;
        if (Input.GetKey(KeyCode.DownArrow)) movement += Vector3.back;
        if (Input.GetKey(KeyCode.LeftArrow)) movement += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow)) movement += Vector3.right;

        movement.Normalize();

        movement *= moveSpeed * Time.deltaTime;

        //Vector3 newPos = transform.position + movement;
        //Vector3 minPos = grid.GetPosFromCoords(Vector2Int.zero);
        //Vector3 maxPos = grid.GetPosFromCoords(grid.size);
        //transform.position = newPos.Clamp(minPos, maxPos);

        transform.position += movement;
    }
}

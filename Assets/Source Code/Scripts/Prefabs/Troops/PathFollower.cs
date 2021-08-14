using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public float moveSpeed = 1;
    // Update is called once per frame

    Path nextPath;
    Vector3 target;

    public void Init(Path spawnpoint)
    {
        nextPath = spawnpoint;
        target = spawnpoint.gameObject.transform.position;
    }

    void Start()
    {
        StartCoroutine(MoveAlongPath());
    }

    IEnumerator MoveAlongPath()
    {
        while (true)
        {
            Vector3 start = transform.position;
            float y = start.y;
            start += Vector3.down * (y - target.y);
            Vector3 moveDirection = target - start;
            float distance = moveDirection.magnitude;

            float movement = moveSpeed * Time.deltaTime;

            while(movement >= distance) // provavelmente o laço acontecerá de 0 a 1 vezes, a não ser que o lag seja muuito grande
            {
                bool finished = nextPath.distance == 0;
                nextPath = nextPath.smallerNeighbour();

                if(nextPath == null | finished)
                {
                    transform.position = target + Vector3.up * (y - target.y);
                    yield break;
                }

                movement -= distance;
                start = target;
                target = nextPath.gameObject.transform.position;
                moveDirection = target - start;
                distance = moveDirection.magnitude;
            }

            transform.position = start + moveDirection.normalized * movement + Vector3.up * (y - target.y);
            yield return null;
        }
    }
}

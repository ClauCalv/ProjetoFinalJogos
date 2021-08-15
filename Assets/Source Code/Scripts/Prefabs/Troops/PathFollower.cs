using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public float moveSpeed = 1;
    // Update is called once per frame

    Path nextPath;
    Vector3 target;

    public float distanceTraveled = 0;
    public bool stopMoving = false;

    Animator animator;

    public void Init(Path spawnpoint)
    {
        nextPath = spawnpoint;
        target = spawnpoint.gameObject.transform.position;
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(MoveAlongPath());
    }

    IEnumerator MoveAlongPath()
    {
        while (true)
        {
            if (stopMoving)
                yield break;

            Vector3 start = transform.position;
            float y = start.y;
            start += Vector3.down * (y - target.y);
            Vector3 moveDirection = target - start;
            float distance = moveDirection.magnitude;

            float movement = moveSpeed * Time.deltaTime;

            while(movement >= distance) // provavelmente o la�o acontecer� de 0 a 1 vezes, a n�o ser que o lag seja muuito grande
            {
                bool finished = nextPath.distance == 0;
                nextPath = nextPath.smallerNeighbour();

                if(nextPath == null | finished)
                {
                    transform.position = target + Vector3.up * (y - target.y);

                    animator.SetBool("Walk", false);

                    yield break;
                }

                movement -= distance;
                distanceTraveled += distance;
                start = target;

                target = nextPath.gameObject.transform.position;
                moveDirection = target - start;
                distance = moveDirection.magnitude;

                
            }

            distanceTraveled += distance;
            transform.position = start + moveDirection.normalized * movement + Vector3.up * (y - target.y);

            transform.rotation = Quaternion.LookRotation(moveDirection);

            animator.SetBool("Walk", true);

            yield return null;
        }
    }
}

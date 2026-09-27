using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]

    public GameObject applePrefab;
    public GameObject branchPrefab;

    public float speed = 1f;
    public float leftAndRightEdge = 10f;
    public float changeDirChance = 0.1f;

    public float appleDropDelay = 1f;
    public float branchDropDelay = 10f;

    void Start()
    {
        // Start dropping apples
        Invoke("DropApple", 2f);

        // Start branches between apple drops
        Invoke("DropBranch", 5.5f);
    }

    void DropApple()
    {
        GameObject apple =
            Instantiate<GameObject>(applePrefab);

        apple.transform.position = transform.position;

        Invoke("DropApple", appleDropDelay);
    }

    void DropBranch()
    {
        GameObject branch =
            Instantiate<GameObject>(branchPrefab);

        Vector3 pos = transform.position;

        // Slightly offset the branch left or right
        pos.x += Random.Range(-3f, 3f);

        branch.transform.position = pos;

        Invoke("DropBranch", branchDropDelay);
    }

    void Update()
    {
        // Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Changing Direction
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed);
        }
    }

    void FixedUpdate()
    {
        // Random direction changes
        if (Random.value < changeDirChance)
        {
            speed *= -1;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public ScoreCounter scoreCounter;

    void Start()
    {
        // Find the ScoreCounter object in the scene
        GameObject scoreGO = GameObject.Find("ScoreCounter");

        // Get the ScoreCounter script from it
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }

    void Update()
    {
        // Get mouse position on the screen
        Vector3 mousePos2D = Input.mousePosition;

        // Push the mouse position into the game world
        mousePos2D.z = -Camera.main.transform.position.z;

        // Convert screen position to world position
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Move basket left/right with mouse
        Vector3 pos = transform.position;
        pos.x = mousePos3D.x;
        transform.position = pos;
    }

   void OnCollisionEnter(Collision coll)
{
    Debug.Log("Basket hit: " + coll.gameObject.name + 
              " Tag: " + coll.gameObject.tag);

    GameObject collidedWith = coll.gameObject;

    if (collidedWith.CompareTag("Apple"))
    {
        Destroy(collidedWith);

        scoreCounter.score += 100;
        HighScore.TRY_SET_HIGH_SCORE(scoreCounter.score);
    }
    else if (collidedWith.CompareTag("Branch"))
    {
        Debug.Log("BRANCH CAUGHT");

        Destroy(collidedWith);

        ApplePicker apScript =
            Camera.main.GetComponent<ApplePicker>();

        apScript.GameOver();
    }
}
}
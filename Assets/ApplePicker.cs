using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]

    public GameObject basketPrefab;
    public int numBaskets = 4;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;

    public List<GameObject> basketList;

    private Text roundText;
    private GameObject restartButton;

    public bool gameOver = false;

    void Start()
    {
        basketList = new List<GameObject>();

        // Create the baskets
        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO =
                Instantiate<GameObject>(basketPrefab);

            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);

            tBasketGO.transform.position = pos;

            basketList.Add(tBasketGO);
        }

        // Find RoundText
        GameObject roundGO = GameObject.Find("RoundText");

        if (roundGO != null)
        {
            roundText = roundGO.GetComponent<Text>();
        }

        // Find and hide Restart button
        restartButton = GameObject.Find("RestartButton");

        if (restartButton != null)
        {
            restartButton.SetActive(false);
        }
    }

    public void AppleMissed()
    {
        if (gameOver)
        {
            return;
        }

        // Destroy all falling apples
        GameObject[] appleArray =
            GameObject.FindGameObjectsWithTag("Apple");

        foreach (GameObject apple in appleArray)
        {
            Destroy(apple);
        }

        // Remove one basket
        int basketIndex = basketList.Count - 1;

        GameObject basketGO = basketList[basketIndex];

        basketList.RemoveAt(basketIndex);

        Destroy(basketGO);

        // Game Over when no baskets remain
        if (basketList.Count == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        // Prevent GameOver from running multiple times
        if (gameOver)
        {
            return;
        }

        gameOver = true;

        // Change Round text to Game Over
        if (roundText != null)
        {
            roundText.text = "Game Over";
        }

        // Show Restart button
        if (restartButton != null)
        {
            restartButton.SetActive(true);
        }

        // Stop AppleTree from spawning more objects
        AppleTree tree = FindFirstObjectByType<AppleTree>();

        if (tree != null)
        {
            tree.CancelInvoke();
            tree.enabled = false;
        }

        // Destroy any apples still falling
        GameObject[] appleArray =
            GameObject.FindGameObjectsWithTag("Apple");

        foreach (GameObject apple in appleArray)
        {
            Destroy(apple);
        }

        // Destroy any branches still falling
        GameObject[] branchArray =
            GameObject.FindGameObjectsWithTag("Branch");

        foreach (GameObject branch in branchArray)
        {
            Destroy(branch);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("_Scene_0");
    }
}
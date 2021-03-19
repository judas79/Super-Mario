using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// T15 Needed to manipulate the UI
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    // T15 set up to track our collisions with Coin objects
    // Called when something hits the Coin
    // Called when something hits the Coin
    void OnCollisionEnter2D(Collision2D col)
    {

        // T15 Play coin sound
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.GetCoin);

        // Increase the Score,  Text component
        // this will cause an error until you copy paste the increaseTextUIScore(), down below
        increaseTextUIScore();

        // destroy the coin
        Destroy(gameObject);

    }

    // copied and pasted from PrizeBlock.cs, to add to the score, above
    // Increases the score the the text UI name passed
    void increaseTextUIScore()
    {

        // Find the Score UI component
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();

        // Get the string stored in it and convert to an int
        int score = int.Parse(textUIComp.text);

        // Increment the score
        score += 10;

        // Convert the score to a string and update the UI
        textUIComp.text = score.ToString();
    }
}

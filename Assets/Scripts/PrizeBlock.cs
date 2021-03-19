using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// T15 Needed to manipulate the UI
using UnityEngine.UI;

public class PrizeBlock : MonoBehaviour
{
    // T15 define, for animation of the PrizeBlock
    public AnimationCurve anim;

    // T15 definen how many coins per PrizeBlock
    public int coinsInBlock = 5;

    // T15 track for a collision
    // Check if the Manny collision hit the bottom of the Prizeblock
    // get what collided with ur prizeblock, nd react to it
    void OnCollisionEnter2D(Collision2D col)
    {
        // make it so manny can jump on top of the prizeblock, 
        // but prizeblock react to it if he hits the bottom of the box
        // check if Manny(col.contacts[0].point.y) collided with
        // the bottom of the prizebox( transform.position.y is bottom of prizebox)
        if (col.contacts[0].point.y < transform.position.y)
        {
            // Start co-routine named RunAnimation, to pause things
            StartCoroutine(RunAnimation());
        }
        // check If block contains any more coins
        // if greater than zero spit out more coins
        if (coinsInBlock > 0)
        {

            // call sound manager to Play coin sound, 'getCoin'
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.GetCoin);

            // Increase the Score Text component by calling the function
            increaseTextUIScore();

            // decrement 1 value from the coins in the block
            coinsInBlock--;

        }
    }

    // T15The co-routine needs to return an Ienumerator
    IEnumerator RunAnimation()
    {
        // we will use that, up and down curve, for the animation
        // get the starting vector position, for out prizebox, 
        //so we can move it into its other position
        Vector2 startPos = transform.position;

        // cycle through all the keys in our animation curve
        // so we can know how many y values we do to move up, 
        // and then back down again, we will get the length of the animation
        // in the duration of time the animation takes to complete its cycle
        // then increment x after the time duration has completed
        for (float x = 0; x < anim.keys[anim.length - 1].time; x += Time.deltaTime)
        {

            // Change the block position to what is defined
            // on the AnimationCurve. anim.Evaluate changes our animation
            transform.position = new Vector2(startPos.x,
                startPos.y + anim.Evaluate(x));

            // Continue looping at next update, which is dependant on the frame rate
            yield return null;
        }
    }

    // Increases the score the the text UI name passed
    void increaseTextUIScore()
    {

        // Find the Score UI component, and be able to change the score text
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();

        // Get the string stored in text and convert to an int
        int score = int.Parse(textUIComp.text);

        // Increment the score
        score += 10;

        // Convert the score to a string and update the UI
        textUIComp.text = score.ToString();
    }

}

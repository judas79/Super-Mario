using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    // T16 Snail speed, public so we can easily find and change it in Unity
    public float speed = 2f;

    // T16 direction for our snail to start in
    Vector2 direction = Vector2.right;

    // T16 move our snail by updating our rigidBody for the snail
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = direction *speed;
    }

    // T16 to make the Snail move and face in the opposite direction,
    // when it hits SnailStart or SnailEnd, using the trigger,
    // where we put a checkmark by, in the settings, for them
    private void OnTriggerEnter2D(Collider2D col)
    {
        // flip direction of movement
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);

        // change, to that x direction, leave y alone
        direction = new Vector2(-1 * direction.x, direction.y);
    }

    // T16 if manny lands on top of the snail the snail dies, if not, 
    // and snail runs into manny, manny dies
    void OnCollisionEnter2D(Collision2D col)
    {
        // if name Manny(in hierarchy) collides with snail
        if (col.gameObject.name == "Manny")
        {
            // and if Manny collides with the top > of the snail
            if(col.contacts[0].point.y > transform.position.y)
            {
                // Snail has been killed, by the bottom of the Mannys feet
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.SnailDies);

                // get Animator component and set it to the Dead trigger 
                // we set up in the Animator editor, of the snail Dead.
                GetComponent<Animator>().SetTrigger("Dead");

                // remove the collider by setting it to false
                // so our snail can fall off the screen
                GetComponent<Collider2D>().enabled = false;

                // direction for snail x is the same as it was
                // direction of y is -1 (down), until off screen
                direction = new Vector2(direction.x, -1);

                // give snail some time 3 seconds to fall off the screen, 
                // before destroying it
                Destroy(gameObject, 3);
            }
            else
            {
                // Manny has been killed, by the front or the rear of the snail
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.MannyDies);
                Destroy(col.gameObject, .2f);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBlock : MonoBehaviour
{
    // T15 define Used to change the sprite
    // from the BrickBlock wall to the explodedBlock using SpriteRenderer instance
    private SpriteRenderer sr;

    // T15 define in a public (changeable way) The sprite to change into 'explodedBlock'
    public Sprite explodedBlock;

    // T15 define in a public (changeable way), a period of time between switching sprites
    public float secBeforeSpriteChange = .2f;

    // T15 sometimes we have problems with the SpriteRenderer
    // we want to make sure the spriterender is setup before this sprite change is created
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // T15 Called when something hits the BrickBlock, to handle the collision
    private void OnCollisionEnter2D(Collision2D col)
    {
        // check if the character Manny, hit on the bottom of our BrickBlock
        if(col.contacts[0].point.y < transform.position.y)
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.RockSmash);

            // Change the Block sprite, to explodedBlock
            sr.sprite = explodedBlock;

            // Wait a fraction of a second and then destroy the BrickBlock
            DestroyObject (gameObject, secBeforeSpriteChange);
        }
    }
}

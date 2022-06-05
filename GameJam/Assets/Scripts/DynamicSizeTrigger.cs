using UnityEngine;
using System.Collections;

public class DynamicSizeTrigger : MonoBehaviour {

    Sprite currentSprite;
    BoxCollider2D coll;
    SpriteRenderer spr;

    void Start() {
        coll = GetComponentInChildren<BoxCollider2D>();
        spr = GetComponentInChildren<SpriteRenderer>();
        UpdateCollider();
    }

    void Update()
    {
	    if(currentSprite != spr.sprite)
	    {
		    currentSprite = spr.sprite;
		    UpdateCollider();
	    }
    }

    void UpdateCollider()
    {
	    coll.size = spr.sprite.bounds.size;
	    coll.offset = spr.sprite.bounds.center;

        this.GetComponent<NewPlayerMovement>().width = GetComponent<Collider2D>().bounds.extents.x + 0.001f;
        this.GetComponent<NewPlayerMovement>().height = GetComponent<Collider2D>().bounds.extents.y + 0.001f;
    }
}
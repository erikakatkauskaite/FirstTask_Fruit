using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class BerryControl : MonoBehaviour
{
    private float startPositionX;
    private float startPositionY;
    private bool onHold = false;
    private Vector2 initialPosition;
    private Transform basketPlace;
    Rigidbody2D rb;
    
    private void Start()
    {
        initialPosition = transform.position;
        basketPlace = GameObject.FindGameObjectWithTag("basket").GetComponent<Collider2D>().transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (onHold)
        {
            Vector3 mousePosition;
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            this.gameObject.transform.localPosition = new Vector3(mousePosition.x - startPositionX, mousePosition.y - startPositionY, 0);
        }
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {        
            Vector3 mousePosition;
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            startPositionX = mousePosition.x - this.transform.localPosition.x;
            startPositionY = mousePosition.y - this.transform.localPosition.y;

            onHold = true;
        }        
    }

    private void OnMouseUp()
    {
        if(transform.position.y >0.4)
        {
             if (Mathf.Abs(transform.position.x - basketPlace.position.x) <= 0.7f && (Mathf.Abs(transform.position.y - basketPlace.position.y) <= 3f))
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                transform.position = new Vector2(initialPosition.x, initialPosition.y);
            }
        }
        
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y); 
        }
        onHold = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("basket"))
        {
            Destroy(this.gameObject);
        }     
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InstantiateBerries : MonoBehaviour
{
    public GameObject berryPrefab;
    public Sprite[] berrySprites;
    private float respawnTime = 2f;
    private Vector3 screenBoundaries;
    private bool collectedAll;
    private int randomBerryIndex;
    private Vector3 berrySpriteBounds;

    private void Start()
    {

        berrySpriteBounds = new Vector3(0.5f,0.5f,0 );
        
        randomBerryIndex = Random.Range(0, 8);
        collectedAll = false;

        screenBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(SpawnBerryObject());
    }

    private void InstantiateBerryObject()
    {
        GameObject newBerry = Instantiate(berryPrefab) as GameObject;
        newBerry.GetComponent<SpriteRenderer>().transform.localScale = berrySpriteBounds;
        
        newBerry.GetComponent<SpriteRenderer>().sprite = berrySprites[randomBerryIndex];
        newBerry.transform.position = new Vector2(Random.Range(-screenBoundaries.x + 0.5f, screenBoundaries.x - 0.5f), Random.Range(0, screenBoundaries.y - 1.0f));     
    }

    private void Update()
    {
        screenBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    public void StopSpawning()
    {
        collectedAll = true;
    }

    IEnumerator SpawnBerryObject()
    {
        while(!collectedAll)
        {
            yield return new WaitForSeconds(respawnTime);
            randomBerryIndex = Random.Range(0, 8);
            InstantiateBerryObject();
        }       
    }  
}

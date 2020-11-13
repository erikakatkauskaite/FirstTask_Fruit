using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectBerry : MonoBehaviour
{
    public int currentBerriesCount;
    public int maxBerriesCount;

    public SetProgressBar progressBar;
    public InstantiateBerries instantiateBerry;

    public AudioSource berrySound;
    public AudioSource winSound;
    public AudioSource backgroundSound;

    private bool winMusicPlayed;

    public GameObject sooGood;
    public Animator sooGoodAnimation;


    private void Start()
    {
        sooGoodAnimation.SetBool("sooGoodAnim", false);
        sooGood.SetActive(false);
        winMusicPlayed = false;
        backgroundSound.Play();
        maxBerriesCount = 10;
        progressBar.SetMaxBerries(maxBerriesCount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        berrySound.Play();
        if(currentBerriesCount +1< maxBerriesCount)
        {
            currentBerriesCount++;
            progressBar.SetProgress(currentBerriesCount);
            Debug.Log(currentBerriesCount);
        }
        else
        {       
            if(!winMusicPlayed)
            {
                sooGood.SetActive(true);
                sooGoodAnimation.SetBool("soGoodAnim", true);
                winMusicPlayed = true;
                Invoke("StopMainMusic", 1f);
                currentBerriesCount++;
                progressBar.SetProgress(currentBerriesCount);
                winSound.Play();
                instantiateBerry.StopSpawning();
                Debug.Log("Win");
            }         
        }
    }

    private void StopMainMusic()
    {
        backgroundSound.Stop();
    }
}

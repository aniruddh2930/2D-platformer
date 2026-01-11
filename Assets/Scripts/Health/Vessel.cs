using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Vessel : MonoBehaviour
{
    [SerializeField] private int TotalRepawns;
    [SerializeField] private Image currentVessel;
    [SerializeField] private Image totalVessel;
    [SerializeField] private PlayerRespawn playerRespawn;
    private UIManager uiManager;
    private float fill=1;
    private float originalOpacity;
    private float alpha;

    private void Start()
    {
        uiManager = GetComponentInParent<UIManager>();
        originalOpacity = currentVessel.color.a;
    }

    public void loseLife()
    {
        alpha = 0;
        totalVessel.gameObject.SetActive(true);
        currentVessel.gameObject.SetActive(true);
        StartCoroutine(EnableImage());
    }

    private IEnumerator DisableImage()
    {
        for (int i = 0; i < 100; i++)
        {
            alpha -= alpha / 100;
            currentVessel.color = new Color(0.8584906f, 0.1093361f, 0.1093361f,alpha);
            totalVessel.color=new Color(0.2830189f, 0.2095942f, 0.2095942f, alpha);
            yield return new WaitForEndOfFrame();
        }
        currentVessel.gameObject.SetActive(false);
        totalVessel.gameObject.SetActive(false);
    }

    private IEnumerator EnableImage()
    {
        for (int i = 0; i < 50; i++)
        {
            alpha += originalOpacity / 50;
            currentVessel.color = new Color(0.8584906f, 0.1093361f, 0.1093361f, alpha);
            totalVessel.color = new Color(0.2830189f, 0.2095942f, 0.2095942f, alpha);
            yield return new WaitForSeconds(1/50);
        }
        StartCoroutine(Fill());
    }

    private IEnumerator Fill()
    {
        for (int i = 0; i < 50; i++)
        {
            fill = fill - (1f / ((TotalRepawns+1) * 50));
            currentVessel.fillAmount = fill;
            yield return new WaitForSeconds(1/100);
        }
        //rounding error if 0
        if (fill <= 0.1)
        {
            playerRespawn.enabled = false;
            uiManager.GameOver(); 
        }
        StartCoroutine(DisableImage());
    }
}

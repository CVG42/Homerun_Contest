using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnusController : MonoBehaviour
{
    public Image magnusBar;
    public Image leftMagnusBar;
    public Hit launch;
    public GameObject text;
    public float fillSpeed = 0.01f;
    private float maxCoefficient = 4f;
    public float currentCoefficient;
    private bool isIncreasing;
    public bool magnusBarOn;

    void Start()
    {
        launch = GetComponent<Hit>();
        currentCoefficient = 0;
        isIncreasing = false;
        magnusBarOn = true;
        StartCoroutine(MagnusFill());
    }

    IEnumerator MagnusFill()
    {
        while(magnusBarOn == true)
        {
            if(!isIncreasing)
            {
                currentCoefficient -= fillSpeed;
                if(currentCoefficient <= 0)
                {
                    isIncreasing = true;
                }
            }

            if(isIncreasing)
            {
                currentCoefficient += fillSpeed;
                if(currentCoefficient >= maxCoefficient)
                {
                    isIncreasing = false;
                }
            }

            //currentCoefficient -= fillSpeed;
            float fill = currentCoefficient / maxCoefficient;
            magnusBar.fillAmount = fill;
            leftMagnusBar.fillAmount = fill;
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }

    public void StopRightBar()
    {
        magnusBarOn = false;
        text.SetActive(true);
    }

    public void StopLeftBar()
    {
        magnusBarOn = false;
        currentCoefficient *= -1f;
        text.SetActive(true);
    }
}

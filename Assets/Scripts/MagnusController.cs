using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnusController : MonoBehaviour
{
    public Image magnusBar;
    public float fillSpeed = 0.1f;
    private float maxCoefficient = 8;
    private float currentCoefficient;
    private bool isIncreasing;
    private bool magnusBarOn;

    void Start()
    {
        currentCoefficient = maxCoefficient;
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
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }

    public void StopRightBar()
    {
        magnusBarOn = false;
    }
}

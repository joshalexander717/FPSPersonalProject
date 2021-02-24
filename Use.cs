using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Use : MonoBehaviour
{
    public float TimeToUse;
    public bool used;
    float defTime;
    float sameTime;
    Image fillImage;

    public GameObject AccessItem;

    delegate void UseMethod();
    UseMethod UseAction;

    public bool lightStand;
    public bool chest;

    // Start is called before the first frame update
    void Start()
    {
        defTime = TimeToUse;
        used = false;
        fillImage = GameObject.Find("UseHUD").GetComponent<Image>();
        if(lightStand)
        {
            UseAction = UseLightStand;
        }
        else if(chest)
        {
            UseAction = UseChest;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!used)
        {
            if(sameTime == TimeToUse)
            {
                fillImage.fillAmount = 0;
                ResetTimer();
            }
            else
            {
                fillImage.fillAmount = 1 - (TimeToUse / defTime);
            }
            sameTime = TimeToUse;
        }
    }

    public void ResetTimer()
    {
        TimeToUse = defTime;
    }

    public void UseObject()
    {
        fillImage.fillAmount = 0;
        used = true;
        
        UseAction();
    }


    private void UseLightStand()
    {
        AccessItem.GetComponent<Light>().intensity = 30;
    }

    private void UseChest()
    {
        GetComponent<Chest>().isOpen = true;
    }
}

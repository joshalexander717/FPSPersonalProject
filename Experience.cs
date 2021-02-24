using UnityEngine;
using System.Collections;
using TMPro;

public class Experience : MonoBehaviour
{

    //current level
    public int vLevel = 1;
    //current exp amount
    public int vCurrExp = 0;
    //exp amount needed for lvl 1
    public int vExpBase = 10;
    //exp amount left to next levelup
    public int vExpLeft = 10;
    //modifier that increases needed exp each level
    public float vExpMod = 1.15f;

    TextMeshProUGUI LevelText;

    private void Start()
    {
        SimpleHealthBar.UpdateBar("PlayerExperience", vCurrExp, vCurrExp + vExpLeft);
        LevelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        LevelText.text = vLevel + "";
    }

    //leveling methods
    public void GainExp(int e)
    {
        vCurrExp += e;
        if (vCurrExp >= vExpLeft)
        {
            LvlUp();
        }
        SimpleHealthBar.UpdateBar("PlayerExperience", vCurrExp, vCurrExp + vExpLeft);
    }
    void LvlUp()
    {
        vCurrExp -= vExpLeft;
        vLevel++;
        float t = Mathf.Pow(vExpMod, vLevel);
        vExpLeft = (int)Mathf.Floor(vExpBase * t);
        LevelText.text = vLevel + "";
    }
}
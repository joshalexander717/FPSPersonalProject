using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hitmarker : MonoBehaviour
{
    public float alpha;
    Image hitmark;
    AudioSource As;

    // Start is called before the first frame update
    void Start()
    {
        alpha = 0;
        hitmark = GetComponent<Image>();
        As = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        alpha = Mathf.Lerp(alpha, 0, 5 * Time.deltaTime);
        hitmark.color = new Color(1f, 1f, 1f, alpha);
    }

    public void Sethitmarker()
    {
        alpha = 1f;
        //play sound
    }

    public void PlayHitMarkerNoise()
    {
        As.Play();
    }
}

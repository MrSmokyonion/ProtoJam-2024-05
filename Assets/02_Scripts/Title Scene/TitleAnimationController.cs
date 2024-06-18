using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimationController : MonoBehaviour
{
    enum TitleImage
    {
        Background,
        Crocodile,
        Duck,
        Player,
        WaterSplash,
        Poop,
        Title
    }

    public float[] timing = {0.5f, 0.3f, 0.4f, 0.5f, 0.7f, 1.0f};
    public Vector2[] defualtScale;


    Image[] images;

    private void Awake()
    {
        images = new Image[7];
        defualtScale = new Vector2[images.Length];

        images[0] = GetComponent<Image>();
        defualtScale[0] = images[0].rectTransform.sizeDelta;


        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            images[i + 1] = child.GetComponent<Image>();

            defualtScale[i + 1] = images[i+1].rectTransform.sizeDelta;
        }

        foreach (var item in images)
        {
            item.color = Color.clear;
        }

        images[1].color = Color.white;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timing());
    }

    IEnumerator Timing()
    {
        images[0].DOColor(Color.white, 0.5f);       // 배경
        yield return new WaitForSeconds(timing[0]);
        images[(int)TitleImage.WaterSplash].DOColor(Color.white, 0.5f);       // 물 뿌리기 효과
        yield return new WaitForSeconds(timing[1]);
        images[(int)TitleImage.Poop].DOColor(Color.white, 0.5f);       // 똥
        yield return new WaitForSeconds(timing[2]);
        images[(int)TitleImage.Player].DOColor(Color.white, 0.5f);       // 플레이어
        yield return new WaitForSeconds(timing[3]);
        images[(int)TitleImage.Duck].DOColor(Color.white, 0.5f);       // 오리
        yield return new WaitForSeconds(timing[4]);
        yield return new WaitForSeconds(timing[5]);
        images[(int)TitleImage.Title].DOColor(Color.white, 2f);       // 타이틀 씬
    }
}

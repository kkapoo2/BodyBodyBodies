using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    UIManager uiManager;
    public CanvasGroup fadeCanvasGroup; // 검은 화면
    public float fadeSpeed = 1.5f; // 페이드 속도

    private void Start()
    {
        fadeCanvasGroup.alpha = 0f;
    }

    // 게임 시작 버튼 클릭 시 실행
    public void StartGame()
    {
        StartCoroutine(FadeInOut());
    }

    // 페이드 인 & 씬 전환 후 페이드 아웃
    IEnumerator FadeInOut()
    {
        yield return StartCoroutine(FadeIn());  // 페이드 인 (화면 검게)
        uiManager.StartGame();     // 1-1 스테이지로 이동
        yield return StartCoroutine(FadeOut()); // 페이드 아웃 (화면 밝게)
    }

    // 페이드 인 (화면을 점점 검게)
    IEnumerator FadeIn()
    {
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeCanvasGroup.alpha = alpha;
            yield return null;
        }
    }

    // 페이드 아웃 (화면을 점점 밝게)
    IEnumerator FadeOut()
    {
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            fadeCanvasGroup.alpha = alpha;
            yield return null;
        }
    }
}
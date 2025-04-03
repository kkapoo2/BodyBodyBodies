using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    UIManager uiManager;
    public CanvasGroup fadeCanvasGroup; // ���� ȭ��
    public float fadeSpeed = 1.5f; // ���̵� �ӵ�

    private void Start()
    {
        fadeCanvasGroup.alpha = 0f;
    }

    // ���� ���� ��ư Ŭ�� �� ����
    public void StartGame()
    {
        StartCoroutine(FadeInOut());
    }

    // ���̵� �� & �� ��ȯ �� ���̵� �ƿ�
    IEnumerator FadeInOut()
    {
        yield return StartCoroutine(FadeIn());  // ���̵� �� (ȭ�� �˰�)
        uiManager.StartGame();     // 1-1 ���������� �̵�
        yield return StartCoroutine(FadeOut()); // ���̵� �ƿ� (ȭ�� ���)
    }

    // ���̵� �� (ȭ���� ���� �˰�)
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

    // ���̵� �ƿ� (ȭ���� ���� ���)
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
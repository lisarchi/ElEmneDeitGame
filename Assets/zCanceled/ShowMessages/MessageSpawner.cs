//using System.Collections;
//using UnityEngine;

//public class MessageSpawner : MonoBehaviour
//{
//    [SerializeField] internal SpriteRenderer displayRenderer;
//    [SerializeField] private float _displayTime = 5f;
//    private float _fadeTime = 0.5f;

//    private Coroutine currentRoutine = null;

//    public void ShowMessage(Sprite pic)
//    {
//        if (pic == null) return;

//        displayRenderer.sprite = pic;

//        // если картинка уже показываетс€ Ч перезапускаем эффект
//        if (currentRoutine != null)
//            StopCoroutine(currentRoutine);

//        currentRoutine = StartCoroutine(ShowRoutine());
//    }

//    private IEnumerator ShowRoutine()
//    {
//        // 1) ѕлавное по€вление
//        yield return StartCoroutine(Fade(0f, 1f, _fadeTime));

//        // 2) ќжидание
//        yield return new WaitForSeconds(_displayTime);

//        // 3) ѕлавное исчезновение
//        yield return StartCoroutine(Fade(1f, 0f, _fadeTime));

//        displayRenderer.sprite = null;
//        currentRoutine = null;
//    }

//    private IEnumerator Fade(float start, float end, float time)
//    {
//        float t = 0f;
//        Color c = displayRenderer.color;

//        while (t < time)
//        {
//            t += Time.deltaTime;
//            float a = Mathf.Lerp(start, end, t / time);
//            displayRenderer.color = new Color(c.r, c.g, c.b, a);
//            yield return null;
//        }

//        // гарантируем точное значение в конце
//        displayRenderer.color = new Color(c.r, c.g, c.b, end);
//    }
//}

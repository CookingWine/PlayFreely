using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace PlayFreely.HotfixRuntime
{
    /// <summary>
    /// UI扩展
    /// </summary>
    public static class UIExtension
    {
        /// <summary>
        /// 淡出
        /// </summary>
        /// <param name="canvasGroup"></param>
        /// <param name="alpha">透明度</param>
        /// <param name="duration">持续时间</param>
        /// <returns></returns>
        public static IEnumerator FadeToAlpha(this CanvasGroup canvasGroup , float alpha , float duration)
        {
            float time = 0f;
            float originalAlpha = canvasGroup.alpha;
            while(time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(originalAlpha , alpha , time / duration);
                yield return new WaitForEndOfFrame( );
            }

            canvasGroup.alpha = alpha;
        }

        public static IEnumerator SmoothValue(this Slider slider , float value , float duration)
        {
            float time = 0f;
            float originalValue = slider.value;
            while(time < duration)
            {
                time += Time.deltaTime;
                slider.value = Mathf.Lerp(originalValue , value , time / duration);
                yield return new WaitForEndOfFrame( );
            }

            slider.value = value;
        }
    }
}

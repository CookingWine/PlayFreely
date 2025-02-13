using UnityEngine;
using UnityEngine.UI;

namespace PlayFreely.BuiltinRuntime
{
    /// <summary>
    /// 非热更主界面
    /// </summary>
    public class BuiltinRuntimeInterface:MonoBehaviour
    {
        [SerializeField]
        private Text m_LoadingProgresText;

        [SerializeField]
        private Text m_LoadingText;

        [SerializeField]
        private Image m_LoadingCircle;

        [SerializeField]
        private Image m_LoadingFill;

        /// <summary>
        /// 设置加载进度
        /// </summary>
        /// <param name="content">显示文本</param>
        /// <param name="progress">进度0~1</param>
        public void SetLoadingProgress(string content , float progress)
        {
            m_LoadingProgresText.text = $"{(int)( progress * 100 )}%";
            m_LoadingText.text = content;
            m_LoadingCircle.fillAmount = progress;
            m_LoadingFill.fillAmount = progress;
        }

        /// <summary>
        /// 打开弹窗
        /// </summary>
        public void OpenPopUpWindows( )
        {

        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class ScreenAdaptation:MonoBehaviour
{
    public RectTransform m_UIRoot;

    [ReadOnly]
    public Vector2 m_DefalutWorH = new Vector2(1080 , 1920);

    public bool m_UseDefalutSize = true;
    public CanvasScaler.ScaleMode m_ScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

    public CanvasScaler.ScreenMatchMode m_ScreenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;

    private void Update( )
    {

        if(Input.GetMouseButtonDown(0))
        {
            RefreshScreen( );
        }
    }

    public void RefreshScreen( )
    {
        int width = Screen.width;
        int height = Screen.height;

        Debug.Log($"Éè±¸ÆÁÄ»¿í->{width}ÆÁÄ»¸ß{height}");

        CanvasScaler scaler = m_UIRoot.GetComponent<CanvasScaler>( );
        scaler.uiScaleMode = m_ScaleMode;
        if(m_UseDefalutSize)
            scaler.referenceResolution = m_DefalutWorH;
        else
            scaler.referenceResolution = new Vector2(width , height);
        scaler.matchWidthOrHeight = 0.5f;
        scaler.screenMatchMode = m_ScreenMatchMode;
    }


}



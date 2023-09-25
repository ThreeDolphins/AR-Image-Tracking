using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public static bool Button(Rect bounds, string caption)
    {
        bool bClick = false;

        Texture2D temp = GUI.skin.button.normal.background;
        Texture2D tempA = GUI.skin.button.active.background;
        Texture2D tempB = GUI.skin.button.hover.background;

        GUI.skin.button.normal.background = TextureManager.GetUITexture(TextureManager.UITexEnum.ButtonNormal);
        GUI.skin.button.active.background = TextureManager.GetUITexture(TextureManager.UITexEnum.ButtonActive);
        GUI.skin.button.hover.background = TextureManager.GetUITexture(TextureManager.UITexEnum.ButtonHover);

        GUI.skin.button.fontSize = (int)(bounds.height * 0.6f);

        if (GUI.Button(bounds, caption))
        {
            bClick = true;
        }

        GUI.skin.button.normal.background = temp;
        GUI.skin.button.active.background = tempA;
        GUI.skin.button.hover.background = tempB;

        return bClick;
    }

}

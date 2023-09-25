using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager 
{
    public static TextureManager mInstance = null;
    List<Texture2D> uiTextureList = new List<Texture2D>();

    public enum UITexEnum
    {
        ButtonActive,
        ButtonHover,
        ButtonNormal,           
        _MaxUITex
    };
    
    public static Texture2D GetUITexture(UITexEnum mEnum)
    {
        if (mInstance == null
            || mInstance.uiTextureList.Count <= (int)mEnum)
            return null;

        return mInstance.uiTextureList[(int)mEnum];
    }


    public void InitTextures()
    {
        mInstance = this;
        CleanTextures();        
        for (int i = 0; i < (int)UITexEnum._MaxUITex; i++)
        {
            Texture2D mNewTexture = (Texture2D)Resources.Load("Texture/" + (UITexEnum)i);
            if (mNewTexture != null)
            {
                uiTextureList.Add(mNewTexture);
            }
            else
            {
                uiTextureList.Add(null);
            }
        }
    }

    // Update is called once per frame
    public void ShutDown()
    {
        CleanTextures();
        mInstance = null;
       
    }
    void CleanTextures()
    {
        for (int i = 0; i < uiTextureList.Count; i++)
        {
            Resources.UnloadAsset(uiTextureList[i]);
        }
        uiTextureList.Clear();
    }
}

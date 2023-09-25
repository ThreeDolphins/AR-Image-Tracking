using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : SingletonObj<AppManager>
{

    TextureManager  gTextureManager = null;

    // Start is called before the first frame update
    void Start()
    {
        if (gTextureManager == null)
        {
            gTextureManager = new TextureManager();
            gTextureManager.InitTextures();
        }
        
    }
    private void OnDestroy()
    {
        if (gTextureManager != null) 
        {
            gTextureManager.ShutDown();            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnGUI()
    {
        if (IsExitPressed())
        {
            return;
        }
    }


    #region UI
    public bool IsExitPressed()
    {
        if (UIManager.Button(new Rect(Screen.width * 0.4f, Screen.height * 0.96f, Screen.width * 0.2f, Screen.height * 0.03f), "Exit"))
        {
            Application.Quit();
            return true;
        }
        return false;
    }

    #endregion
}

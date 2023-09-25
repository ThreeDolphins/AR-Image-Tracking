This is an AR project assigned by Arthrex for AR technical interview. 

Installation Steps:
1. Download and install Unity3D 2022.3.5f1
2. Download AR Foundation 5.0.7 from Package Manager. 
3. Download Google AR Core XR Plugin 5.0.7 from Package Manager.
4. Clone this repo and build it.

Run APK/Build FIles:
1. AR.APK file is included in "Build" folder.
2. Copy APK file to your android device and install it.
3. Make sure you have "Installation from unknown source" is selected and your android device have AR feature available.

3rd Party Library:
This project uses Unity and AR Foundation which is provided by Unity3D. No other library is used for this project. 

Solution Aproach:
1. Used Unity AR foundation->Image Tracking feature to track image in realtime. Trackable image resolution is 256x256 due to ARFoundation limitations/error (Which need to be explored).
2. Getting color/pixel information from trackable image is done through Unity Texture2D GetPixel.
3. Particle Manager class hold list of Particle object, which handles animation, position etc. for each Particle object. Unity Default Quad mesh is used with custom shader and animation done through script. Color for each particle is received from trackable image pixel and set to mesh->vertex color, which is consumed in shader to render different color. no Library used for Particle system.
4. To have modular aproach, AppManager, TextureManager, UIManager class are used. However there are not enough data availabe for each manager class. Where AppManager class is a Singleton.   

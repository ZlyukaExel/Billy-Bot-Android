using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveCanvas : MonoBehaviour
{
    public Transform canvas;
    public Vector2 canvasHeight;

    private void Start()
    {
        canvasHeight = canvas.position;
    }

    void LateUpdate()
    {
        if (TouchScreenKeyboard.visible)
            canvas.position = new Vector2(canvasHeight.x,canvasHeight.y+GetKeyboardSize());
        else
            canvas.position = canvasHeight;
    }

    public int GetKeyboardSize()
    {
        using (AndroidJavaClass UnityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject View = UnityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView");

            using (AndroidJavaObject Rct = new AndroidJavaObject("android.graphics.Rect"))
            {
                View.Call("getWindowVisibleDisplayFrame", Rct);

                return Screen.height - Rct.Call<int>("height");
            }
        }
    }
}

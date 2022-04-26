using System;
using System.Collections.Generic;
using UnityEngine;
using Tobii.StreamEngine;

public class EyeTrackerControllerv2 : MonoBehaviour
{
    private static IntPtr apiContext;
    private static IntPtr deviceContext;
    private static tobii_error_t result;

    private static bool deviceReady = false;
    private static bool recordBlinking = false;
    private static int leftBlinkStatusCount = 0;
    private static int rightBlinkStatusCount = 0;
    private static int bothBlinkStatusCount = 0;
    private static int validBlinkDuration = 50;
    private static BlinkStatus blinkStatus = new BlinkStatus();
    private static Vector2 currentGazePoint = new Vector2(-1, -1);
    private static Vector2 blinkPoint = new Vector2(-1, -1);
    private bool singleClick = false;
    
    static float xresolution;
    static float yresolution;

    //----------------------------------
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

    public const int MOUSEEVENTF_LEFTDOWN = 0x02;
    public const int MOUSEEVENTF_LEFTUP = 0x04;

    public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
    public const int MOUSEEVENTF_RIGHTUP = 0x10;
    public static void LeftMouseClick(int xpos, int ypos)
    {
        mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
        mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
    }

    public static void RightMouseClick(int xpos, int ypos)
    {
        mouse_event(MOUSEEVENTF_RIGHTDOWN, xpos, ypos, 0, 0);
        mouse_event(MOUSEEVENTF_RIGHTUP, xpos, ypos, 0, 0);
    }
    //--------------------------------------


    // Start se llama una vez al iniciar el juego
    void Start()
    {
        result = Interop.tobii_api_create(out apiContext, null);
        bool apiCreated = result == tobii_error_t.TOBII_ERROR_NO_ERROR;
        //Debug.Log("Inicializando API: " + apiCreated);

        List<string> urls;
        result = Interop.tobii_enumerate_local_device_urls(apiContext, out urls);
        bool deviceFound = (result == tobii_error_t.TOBII_ERROR_NO_ERROR) && (urls.Count > 0);
        //Debug.Log("Dispositivo: " + deviceFound);

        if (deviceFound)
        {
            //IntPtr deviceContext;
            result = Interop.tobii_device_create(apiContext, urls[0], Interop.tobii_field_of_use_t.TOBII_FIELD_OF_USE_INTERACTIVE, out deviceContext);
            bool deviceCreated = result == tobii_error_t.TOBII_ERROR_NO_ERROR;
            //Debug.Log("Create Device: " + deviceCreated);

            tobii_gaze_point_callback_t gpCallback = new tobii_gaze_point_callback_t(OnGazePoint);
            result = Interop.tobii_gaze_point_subscribe(deviceContext, gpCallback);
            bool gazePointSubscribed = result == tobii_error_t.TOBII_ERROR_NO_ERROR;
            //Debug.Log("Subscribing Gaze Point Callback: " + gazePointSubscribed);

            xresolution = Screen.currentResolution.width;
            yresolution = Screen.currentResolution.height;

            tobii_gaze_origin_callback_t goCallback = new tobii_gaze_origin_callback_t(OnGazeOrigin);
            result = Interop.tobii_gaze_origin_subscribe(deviceContext, goCallback);
            bool gazeOriginSubscribed = result == tobii_error_t.TOBII_ERROR_NO_ERROR;
            //Debug.Log("Subscribing Gaze Origin Callback: " + gazeOriginSubscribed);
            if (apiCreated && deviceFound && deviceCreated && gazePointSubscribed && gazeOriginSubscribed)
            {
                deviceReady = true;
                recordBlinking = true;
                Debug.Log("-----Eye Tracker conectado correctamente-----");
            }
        }
    }

    void Update()
    {
        if (deviceReady)
        {
            Debug.Log(deviceReady);
            result = Interop.tobii_device_process_callbacks(deviceContext);
            
                if (blinkStatus.validOneEyedBlink == true)
                {
                    Vector3 mousePos = Input.mousePosition;
                    SetCursorPos((int)currentGazePoint.x, (int)(currentGazePoint.y));
                    if (singleClick == false)
                    {
                        if (blinkStatus.right == true)
                        {
                            RightMouseClick((int)currentGazePoint.x, (int)(currentGazePoint.y));
                            singleClick = true;
                        }
                        else
                        {
                            LeftMouseClick((int)currentGazePoint.x, (int)(currentGazePoint.y));
                            singleClick = true;
                        }
                    }
                }
                if (blinkStatus.validOneEyedBlink == false && blinkStatus.validTwoEyedBlink == false)
                {
                    Vector3 mousePos = Input.mousePosition;
                    SetCursorPos((int)currentGazePoint.x, (int)(currentGazePoint.y));
                    singleClick = false;
                }
        }
    }

    private static void OnGazePoint(ref tobii_gaze_point_t gazePoint, IntPtr userData)
    {
        float x = gazePoint.position.x * xresolution;
        float y = gazePoint.position.y * yresolution;
        currentGazePoint.x = (int)x;
        currentGazePoint.y = (int)y;
    }

    private static void OnGazeOrigin(ref tobii_gaze_origin_t gazeOrigin, IntPtr userData)
    {
        if (recordBlinking)
        {
            bool left = gazeOrigin.left_validity == tobii_validity_t.TOBII_VALIDITY_VALID;
            bool right = gazeOrigin.right_validity == tobii_validity_t.TOBII_VALIDITY_VALID;
            if (!left && right)
            {
                blinkStatus.oneEyedBlink = true;
                leftBlinkStatusCount++;
                rightBlinkStatusCount = 0;
                bothBlinkStatusCount = 0;
                if (leftBlinkStatusCount == validBlinkDuration)
                {
                    blinkStatus.validOneEyedBlink = true;
                    blinkStatus.left = true;
                    blinkStatus.right = false;
                    blinkPoint = currentGazePoint;
                }
            }
            else if (left && !right)
            {
                blinkStatus.oneEyedBlink = true;
                rightBlinkStatusCount++;
                leftBlinkStatusCount = 0;
                bothBlinkStatusCount = 0;
                if (rightBlinkStatusCount == validBlinkDuration)
                {
                    blinkStatus.validOneEyedBlink = true;
                    blinkStatus.left = false;
                    blinkStatus.right = true;
                    blinkPoint = currentGazePoint;
                }
            }
            else if (!left && !right)
            {
                blinkStatus.twoEyedBlink = true;
                bothBlinkStatusCount++;
                leftBlinkStatusCount = 0;
                rightBlinkStatusCount = 0;
                if (bothBlinkStatusCount == validBlinkDuration)
                {
                    blinkStatus.validOneEyedBlink = false;
                    blinkStatus.validTwoEyedBlink = true;
                    blinkStatus.left = false;
                    blinkStatus.right = false;
                    blinkPoint = currentGazePoint;
                    blinkStatus.twoEyedBlink = true;
                }
            }
            else
            {
                leftBlinkStatusCount = 0;
                rightBlinkStatusCount = 0;
                bothBlinkStatusCount = 0;
                blinkStatus.validOneEyedBlink = false;
                blinkStatus.validTwoEyedBlink = false;
            }
        }
    }
}

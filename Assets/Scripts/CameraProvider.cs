using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProvider : MonoBehaviour
{

    //Objects that will be updated by our camera;
    public GameObject Taskbar;


    //Bounds of our screen (including taskbar), given as (max,min). Bound1 = YBounds | Bound2 = XBounds
    public BoundStorage ScreenBounds;

    // Start is called before the first frame update
    void Awake()
    {
        //Initialize dimensional values from camera.
        Camera MyCamera = GetComponent<Camera>();
        float CamVert  = MyCamera.orthographicSize;
        float CamHoriz = CamVert * Screen.width / Screen.height;

        /*-------------------------------------------TASKBAR-----------------------------------------------------*/
        //Record the height of the Taskbar
        Bounds TbBounds = Taskbar.GetComponent<SpriteRenderer>().sprite.bounds;
        float TbHeight = Taskbar.transform.localScale.y*(TbBounds.max.y - TbBounds.min.y);

        //TODO: FIX THE SCALING ISSUE WITH TASKBAR IN GENERAL

        //Assign dimensional values to Taskbar transform!
        Vector3 TbPos = Taskbar.transform.position;
        Vector3 TblS = Taskbar.transform.localScale;
        Taskbar.transform.position = new Vector3(0f, MyCamera.transform.position.y - CamVert + (TbHeight / 2f), TbPos.z);
        TblS = new Vector3(CamHoriz * 2f, TblS.y, TblS.z);

        //Iconbounds will be worked on here.
        
        ScreenBounds.Bound1 = (MyCamera.transform.position.y + CamVert, MyCamera.transform.position.y - CamVert + TbHeight);
        ScreenBounds.Bound2 = (MyCamera.transform.position.x + CamHoriz, MyCamera.transform.position.x - CamHoriz);
        Debug.Log(ScreenBounds.Bound1);
        Debug.Log(ScreenBounds.Bound2);
    }


}

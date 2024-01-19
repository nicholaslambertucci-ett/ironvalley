using Ett.Vdt.NativeApplication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ett.Vdt.NativeApplication.Data;
using Ett.Vdt.NativeApplication.Data.Types;
using Ett.Vdt.NativeApplication.Data.Sheets;
using System;
using Ett.IronValley.Scripts.Data;

public partial class IronHelper : MonoBehaviour
{
    public void BeginGPSScan(UnityAction<IronPOI> onPOIFound)
    {

        ///TODO  NativeAppController doesn't implement this

        /// NativeAppController.RequestGPSStartScanning(locationID => {

        /// When GPS location is found
        //test
        var locationID = 1;

        NativeAppController.RequestPoiDetails(locationID, details =>
        {
            Debug.Log($"received details for poi {locationID} {details.Description} {details.Title}");
        });
    }

    public void EndGPSScan()
    {
        ///TODO 
    }

    private void ManageSheet(Sheet sheet)
    {
        var items = sheet.Items;

        foreach (var item in items)
        {
            switch (item.Type)
            {
                case TypeOut.Unknown:
                    break;
                case TypeOut.Galleries:
                    ManageGallery(item);
                    break;
                case TypeOut.Map:
                    break;
                case TypeOut.PikkartArScene:
                    break;
                case TypeOut.Poi:
                    ManagePoi(item);
                    break;
                case TypeOut.Sheet:
                    var sht = (Sheet)item.Extra;
                    ManageSheet(sht);
                    break;
                case TypeOut.UnitySceneActivator:
                    break;
                case TypeOut.Vr:
                    ManageVR(item);
                    break;
                default:
                    break;
            }
        }



        if (tag.Equals("video") && string.IsNullOrWhiteSpace(sheet.MediaPath))
        {
            ManageVideo(sheet);
        }
    }

    private void ManageVR(SheetItem item)
    {
        throw new NotImplementedException();
    }

    private void ManageGallery(SheetItem item)
    {
        throw new NotImplementedException();
    }

    private void ManagePoi(SheetItem sheet)
    {
        //var tag = sheet.Tag;

        Sheet sht = (Sheet)sheet.Extra;

        var ironPoi = new IronPOI()
        {
            Title = sht.Title,
            Description = sht.Description,
            Tag = sht.Tag,
            VideoMediaPath = sht.MediaPath,

            //    VideoLabel = sheet.
        };

        if (tag.Equals("poi-1") || tag.Equals("poi-2") || tag.Equals("poi-3"))
        {


        }

    }

    private void ManageVideo(Ett.Vdt.NativeApplication.Data.Sheets.Sheet sheet)
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

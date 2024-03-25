using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class MultipleImgTracker : MonoBehaviour
{
    [Serializable]
    public class TrackInfo
    {
        public string name;
        public GameObject prefab;
    }

    [SerializeField] ARTrackedImageManager trackedImageManager;
    [SerializeField] List<TrackInfo> trackInfos;

    private List<GameObject> trackingList = new List<GameObject>();

    private void Awake()
    {
        if (trackedImageManager == null)
        {
            trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        }
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnChanged;
    }

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage newImage in eventArgs.added)
        {
            TrackInfo info = trackInfos.Find((info) => info.name == newImage.referenceImage.name);
            if (info == null)
                continue;

            GameObject instance = Instantiate(info.prefab);
            instance.name = info.name;
            instance.transform.position = newImage.transform.position;
            instance.transform.rotation = newImage.transform.rotation;
            trackingList.Add(instance);
        }

        foreach (ARTrackedImage updatedImage in eventArgs.updated)
        {
            GameObject instance = trackingList.Find((obj) => obj.name == updatedImage.referenceImage.name);
            if (instance == null)
                return;

            instance.transform.position = updatedImage.transform.position;
            instance.transform.rotation = updatedImage.transform.rotation;
        }

        foreach (ARTrackedImage removedImage in eventArgs.removed)
        {
            GameObject instance = trackingList.Find((obj) => obj.name == removedImage.referenceImage.name);
            if (instance == null)
                return;

            trackingList.Remove(instance);
            Destroy(instance);
        }
    }
}

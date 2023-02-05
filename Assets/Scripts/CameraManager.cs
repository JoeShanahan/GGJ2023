using System;
using System.Collections;
using DG.Tweening;
using EasyButtons;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    
    private Vector3 _cameraZoomOutPosition;

    private float _cameraZoomOutProjectionSize;

    [SerializeField]
    private float zoomDuration;

    [SerializeField]
    private float zoomInProjectionSize;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private Vector3 zoomInOffset;
    
    [Button]
    public void ZoomOut()
    {
        DOTween.To((() => camera.orthographicSize), value => camera.orthographicSize = value, _cameraZoomOutProjectionSize, zoomDuration);
        transform.DOMove(_cameraZoomOutPosition, zoomDuration);
    }

    public void UpdateZoomOut(Transform transform) // hacky workaround to pass in a vector3
    {
        _cameraZoomOutPosition = transform.localPosition;
    }
    
    public void UpdateZoomOut(float orthographicSize)
    {
        _cameraZoomOutProjectionSize = orthographicSize;
    }

    [Button]
    public void ZoomIn(TreehouseRoom room)
    {
        var position = room.transform.position + zoomInOffset;
        position.z = transform.position.z;

        DOTween.To((() => camera.orthographicSize), value => camera.orthographicSize = value, zoomInProjectionSize, zoomDuration);
        transform.DOMove(position, zoomDuration);
    }

    public void Set(Vector3 position, float orthographicSize)
    {
        camera.orthographicSize = orthographicSize;
        transform.position = position;
    }
    
    public void Zoom(Vector3 position, float orthographicSize)
    {
        DOTween.To((() => camera.orthographicSize), value => camera.orthographicSize = value, orthographicSize, zoomDuration);
        transform.DOMove(position, zoomDuration);
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Awake()
    {
        Assert.IsNull(Instance);
        Instance = this;

        _cameraZoomOutPosition = transform.position;
        camera = GetComponent<Camera>();
        _cameraZoomOutProjectionSize = camera.orthographicSize;
    }
}

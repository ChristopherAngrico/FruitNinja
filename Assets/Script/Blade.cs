using UnityEngine;
using UnityEngine.EventSystems;

public class Blade : MonoBehaviour
{
    private bool onSlicing;
    private TrailRenderer sliceTrail;
    private Vector3 worldPosition;
    private SphereCollider bladeCollider;
    public float force;

    [HideInInspector] public bool slice;
    [SerializeField] private float minVelocity;

    private void Start()
    {
        sliceTrail = GetComponentInChildren<TrailRenderer>();
        bladeCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartSlicing();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopSlicing();
        }
        else if (onSlicing)
        {
            ContinueSlicing();
        }

    }
    private void StartSlicing()
    {
        //Add new position for avoid bug trail
        Vector3 newPosition = Input.mousePosition;
        newPosition.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(newPosition);
        transform.position = worldPosition;

        sliceTrail.enabled = true;
        bladeCollider.enabled = true;
        onSlicing = true;
        sliceTrail.Clear();

    }
    private void StopSlicing()
    {
        bladeCollider.enabled = false;
        sliceTrail.enabled = false;
        onSlicing = false;
    }
    private void ContinueSlicing()
    {


        Vector3 mousePosition = Input.mousePosition;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0f;

        //Blade collider will enable / disable depend on velocity of blade 
        Vector3 difference = worldPosition - transform.position;
        float velocity = difference.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minVelocity;

        transform.position = worldPosition;
    }

}

using UnityEngine;


public class Fruit : MonoBehaviour
{
    public GameObject g_whole;
    public GameObject g_sliced;
    private Rigidbody[] rbs;
    private Collider fruitCollider;
    private ParticleSystem fruitEffect;
    private int point = 1;
    private void Start()
    {
        fruitCollider = GetComponent<Collider>();
        fruitEffect = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            fruitCollider.enabled = false;
            g_whole.SetActive(false);
            g_sliced.SetActive(true);

            Blade blade = other.GetComponent<Blade>();
            Slice(other, blade.force);
            GameManager.Instance.point += point;
        }
    }
    
    private void Slice(Collider other, float force)
    {
        fruitEffect.Play();
        //Grab player position
        Vector3 playerPosition = other.gameObject.transform.position;

        //Find the angle for slicing the fruit
        Vector2 difference = transform.position - playerPosition;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        g_sliced.transform.eulerAngles = Vector3.forward * angle;

        //Slice force
        float distance = difference.magnitude;
        Vector3 direction = difference / distance;
        rbs = g_sliced.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.AddForceAtPosition(direction * force, other.transform.position,ForceMode.Impulse);
        }
    }
}

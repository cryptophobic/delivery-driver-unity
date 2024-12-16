using Unity.VisualScripting;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] private Color32 hasPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] private Color32 noPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] float destroyDelay = 0.5f;
    GameObject _package = null;
    
    SpriteRenderer _spriteRenderer;
    Driver _driver;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _driver = GetComponent<Driver>();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Destination"))
        {
            Debug.Log("Collision");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("Bye");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Package") && _package == null)
        {
            _package = other.gameObject;
            other.gameObject.SetActive(false);
            _spriteRenderer.color = hasPackageColor;
            _driver.MoveSpeed /= 2;
            _driver.SteerSpeed /= 2;
            Debug.Log("Taken");
        }

        if (other.gameObject.CompareTag("Customer") && _package != null)
        {
            Debug.LogFormat("Delivered {0}", _package.name);
            Destroy(_package, destroyDelay);
            _spriteRenderer.color = noPackageColor;
            _driver.MoveSpeed *= 2;
            _driver.SteerSpeed *= 2;
            // package.SetActive(true);
            _package = null;
        }
    }
}

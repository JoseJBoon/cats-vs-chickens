using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 anglePerSeconds = new Vector3(0.0f, 2.0f, 0.0f);
    
    private void Update()
    {
        transform.Rotate(anglePerSeconds * Time.deltaTime);
    }
}

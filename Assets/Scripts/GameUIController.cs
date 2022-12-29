using UnityEngine;
using UnityEngine.UIElements;

public class GameUIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar(int health)
    {
        gameObject.GetComponentInChildren<UnityEngine.UI.Image> ().transform.localScale = new Vector3((float)health, 1.0f, 1.0f);
    }

    public void ShowGameOver()
    {

    }
}

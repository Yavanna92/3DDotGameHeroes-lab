using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    private GameOverController _gameOverController;
    private CoinCounter _coinCounter;
    private BoomerangUIController _boomerangUIController;

    // Start is called before the first frame update
    void Start()
    {
        _gameOverController = gameObject.GetComponentInChildren<GameOverController>();
        _coinCounter = gameObject.GetComponentInChildren<CoinCounter>();
        _boomerangUIController = gameObject.GetComponentInChildren<BoomerangUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar(int health)
    {
        var images = gameObject.GetComponentsInChildren<Image>().AsEnumerable();
        var image = images.FirstOrDefault(item => item.CompareTag("Health"));
        image.transform.localScale = new Vector3((float)health, 1.0f, 1.0f);
    }

    public void ShowGameOver()
    {
        _gameOverController.Setup();
    }

    public void HideGameOver()
    {
        _gameOverController.Clear();
    }

    public void ActivateBoomerang()
    {
        _boomerangUIController.Activate();
    }

    public void IncrementCoins()
    {
        _coinCounter.AddCoin();
    }

}

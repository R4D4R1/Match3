using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallType Type { get; private set; }
    public Color Color { get; private set; }
    public bool IsIndestructible { get; private set; }

    private Match3Manager match3Manager;

    void Start()
    {
        match3Manager = Match3Manager.Instance;
    }

    public void Initialize(BallTypeConfig config)
    {
        Type = config.Type;
        Color = config.Color;
        IsIndestructible = config.Type == BallType.Indestructible;
        GetComponent<SpriteRenderer>().color = Color;
    }

    void OnMouseDown()
    {
        if (match3Manager != null)
        {
            match3Manager.OnBallClicked(this);
            Debug.Log("Ball clicked: " + gameObject.name);
        }
    }
}

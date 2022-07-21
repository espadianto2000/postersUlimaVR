using UnityEngine;

[CreateAssetMenu(fileName = "New Poster", menuName = "Poster")]
public class Poster : ScriptableObject
{
    public new string name;
    public string url;
    public Texture2D sprite;

    public void Init(string name, string url)
    {
        this.name = name;
        this.url = url;
    }
}
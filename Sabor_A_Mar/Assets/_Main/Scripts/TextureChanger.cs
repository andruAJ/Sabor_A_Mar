using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public Material material;

    public Texture2D texture1;
    public Texture2D texture2;

    private bool isUnblocked = false;

    public void Start()
    {
        isUnblocked = false;
        ChangeTexture();
    }

    public void SetUnblocked(bool unblocked)
    {
        isUnblocked = unblocked;
        ChangeTexture();
    }
    public void ChangeTexture()
    {
        if (isUnblocked)
        {
            material.mainTexture = texture2;
        }
        else
        {
            material.mainTexture = texture1;
        }
    }
    
}

namespace CSLib
{
    class Sprite
    {
        string texture;
        public Sprite(string textName) {
            texture = textName;
        }
        public void Render(Vec2 pos, Camera cam)
        {
            TextureAtlas.GetAtlas("sprite").DrawTexture(texture, pos*cam.pixelsPerUnit);
        }
    }
}
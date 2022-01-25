namespace Powerless
{
    static class Config
    {
        public const int BACKGROUND = 0; // Non-Collision
        public const int MIDGROUND = 1; // Platforms
        public const int FOREGROUND = 2; // Obstacles (saws, lasers)
        public static readonly int[] COLLIDE_LAYERS = new int[] { MIDGROUND, FOREGROUND };
        public const double GRAVITY = 1;
        public const double JUMP_FORCE = 35;
    }
}
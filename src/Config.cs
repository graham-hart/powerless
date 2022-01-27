namespace Powerless
{
    static class Config
    {
        public const int BACKGROUND = 0; // Non-Collision
        public const int MIDGROUND = 1; // Platforms
        public const int FOREGROUND = 2; // Obstacles (saws, lasers)
        public static readonly int[] COLLIDE_LAYERS = new int[] { MIDGROUND, FOREGROUND };
        public const double GRAVITY = 0.1;
        public const double JUMP_FORCE = 1;
        public const double GROUND_FRICTION = .75;
        public const double AIR_RESISTANCE = 0.81;
    }
}
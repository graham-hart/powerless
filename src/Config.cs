namespace CSLib
{
    static class Config
    {
        public static int BACKGROUND = 0; // Non-Collision
        public static int MIDGROUND = 1; // Platforms
        public static int FOREGROUND = 2; // Obstacles (saws, lasers)
        public static int[] COLLIDE_LAYERS = new int[] { MIDGROUND, FOREGROUND };
        public static double GRAVITY = 0.1;
        public static double JUMP_FORCE = 1;
        public static double GROUND_FRICTION = .4;
        public static double AIR_RESISTANCE = 0.4;
        public static double MAX_ENERGY = 4;
    }
}
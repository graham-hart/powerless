namespace CSLib
{
    static class Config
    {
        public static int BACKGROUND = -1; // Non-Collision
        public static int MIDGROUND = 0; // Platforms
        public static int FOREGROUND = 1; // Obstacles (saws, lasers)
        public static int[] COLLIDE_LAYERS = new int[] { MIDGROUND, FOREGROUND };
        public static double GRAVITY = .1;
        public static double JUMP_FORCE = 1;
        public static double GROUND_FRICTION = 0.4;
        public static double AIR_RESISTANCE = .99;
        public static double MAX_ENERGY = 4;
    }
}
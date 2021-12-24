namespace Game.Ship {
    public class PID {
        private float P, I, D;
        private float prevError;

        public float GetOutput(float Kp, float Ki, float Kd, float currentError, float deltaTime) {
            P = currentError;
            I += P * deltaTime;
            D = (P - prevError) / deltaTime;
            prevError = currentError;

            return P * Kp + I * Ki + D * Kd;
        }
    }
}

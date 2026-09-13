namespace JuraFramework.Tk
{
    public class Transform
    {
        public Float3 position;
        public Quaternion rotation;
        public Float3 scale;

        public Transform()
        {
            position = Float3.zero;
            rotation = Quaternion.identity;
            scale = Float3.one;
        }
    }
}

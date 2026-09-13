using JuraFramework.Tk;

namespace JuraFramework.Objects
{
    public class GameObject
    {
        public string name;
        public Transform transform = new Transform();

        public GameObject()
        {
            name = "New Object";
        }

        public GameObject(string name)
        {
            this.name = name;
        }

        public GameObject(string name, Float3 position)
        {
            this.name = name;
            transform.position = position;
        }
    }
}

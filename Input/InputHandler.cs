using System.Collections.Generic;
using Veldrid;
using JuraTK;

namespace JuraFramework.Input
{
    public static class InputHandler
    {
        private static readonly HashSet<Key> keysDownCurrent = [];
        private static readonly HashSet<Key> keysDownPrevious = [];

        private static readonly HashSet<MouseButton> mouseDownCurrent = [];
        private static readonly HashSet<MouseButton> mouseDownPrevious = [];

        public static Float2 MousePosition { get; private set; }
        public static Float2 MouseDelta { get; private set; }
        public static float MouseWheelDelta { get; private set; }

        public static Float2 ArrowKeyDelta
        {
            get
            {
                float u = GetKey(Key.Up) ? 1f : 0f;
                float d = GetKey(Key.Down) ? 1f : 0f;
                float l = GetKey(Key.Left) ? 1f : 0f;
                float r = GetKey(Key.Right) ? 1f : 0f;

                return new Float2(r - l, u - d);
            }
        }

        /// <summary>
        /// Returns true for all frames that the user is holding this <see cref="Key"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetKey(Key key) => keysDownCurrent.Contains(key);

        /// <summary>
        /// Returns true on the frame the user starts pressing this <see cref="Key"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetKeyDown(Key key) => keysDownCurrent.Contains(key) && !keysDownPrevious.Contains(key);

        /// <summary>
        /// Returns true on the frame the user stops pressing the <see cref="Key"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetKeyUp(Key key) => !keysDownCurrent.Contains(key) && keysDownPrevious.Contains(key);

        /// <summary>
        /// Returns true for all frames that the user is holding this <see cref="MouseButton"/>
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool GetMouseButton(MouseButton button) => mouseDownCurrent.Contains(button);

        /// <summary>
        /// Returns true on the frame that the user starts pressing this <see cref="MouseButton"/>
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool GetMouseButtonDown(MouseButton button) => mouseDownCurrent.Contains(button) && !mouseDownPrevious.Contains(button);

        /// <summary>
        /// Returns true on the frame that the user stops pressing this <see cref="MouseButton"/>
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool GetMouseButtonUp(MouseButton button) => !mouseDownCurrent.Contains(button) && mouseDownPrevious.Contains(button);

        public static void Update(InputSnapshot snapshot)
        {
            keysDownPrevious.Clear();
            foreach(Key key in keysDownCurrent) keysDownPrevious.Add(key);

            mouseDownPrevious.Clear();
            foreach(MouseButton mouseButton in mouseDownCurrent) mouseDownPrevious.Add(mouseButton);

            foreach(KeyEvent kEvent in snapshot.KeyEvents)
            {
                if (kEvent.Down)
                    keysDownCurrent.Add(kEvent.Key);
                else
                    keysDownCurrent.Remove(kEvent.Key);
            }

            foreach(MouseEvent mEvent in snapshot.MouseEvents)
            {
                if (mEvent.Down)
                    mouseDownCurrent.Add(mEvent.MouseButton);
                else
                    mouseDownCurrent.Remove(mEvent.MouseButton);
            }

            Float2 nextMousePos = new(snapshot.MousePosition.X, snapshot.MousePosition.Y);
            MouseDelta = nextMousePos - MousePosition;
            MousePosition = nextMousePos;

            MouseWheelDelta = snapshot.WheelDelta;
        }
    }
}

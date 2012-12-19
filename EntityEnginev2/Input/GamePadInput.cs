using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EntityEnginev2.Input
{
    public sealed class GamePadInput : Input
    {
        private Buttons _button;

        public Buttons Button
        {
            get
            {
                return _button;
            }

            set
            {
                _button = value;
                InputHandler.Flush();
            }
        }

        private readonly PlayerIndex _pi;

        public PlayerIndex PlayerIndex
        {
            get
            {
                return _pi;
            }
        } //Read-Only

        public GamePadInput(Buttons button, PlayerIndex pi)
        {
            _button = button;
            _pi = pi;
        }

        public override bool Pressed()
        {
            return InputHandler.ButtonPressed(Button, PlayerIndex);
        }

        public override bool Released()
        {
            return InputHandler.ButtonReleased(Button, PlayerIndex);
        }

        public override bool Down()
        {
            return InputHandler.ButtonDown(Button, PlayerIndex);
        }

        public override bool Up()
        {
            return InputHandler.ButtonUp(Button, PlayerIndex);
        }
    }
}
using Framework;

namespace Player {

    public partial class PlayerVisual : Visual2D {

        public override PlayerState State {
            get {
                return (PlayerState)state;
            }
        }

        protected override void SetName () {
            parent.Name = "PlayerState " + state.ID;
        }

    }
}
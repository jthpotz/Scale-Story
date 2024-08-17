using Framework;

namespace Player {

    public partial class PlayerState : State {

        public override PlayerData Data {
            get {
                return (PlayerData)data;
            }
        }

        public override PlayerVisual Visual {
            get {
                return (PlayerVisual)visual;
            }
        }

        public PlayerState (PlayerData data, PlayerVisual visual, int id) : base(data, visual, id) {
        }

    }
}
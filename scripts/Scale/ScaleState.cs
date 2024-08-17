using Framework;

namespace Scale {

    public partial class ScaleState : State {

        public override ScaleData Data {
            get {
                return (ScaleData)data;
            }
        }

        public override ScaleVisual Visual {
            get {
                return (ScaleVisual)visual;
            }
        }

        public ScaleState (ScaleData data, ScaleVisual visual, int id) : base(data, visual, id) {
        }

    }
}
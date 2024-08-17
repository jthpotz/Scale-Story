using Godot;

namespace Framework {

    /// <summary>
    /// Represents visual information for 2D nodes.
    /// </summary>
    public abstract partial class Visual2D : Visual {

        /// <summary>
        /// Parent Node.
        /// </summary>
        protected Node2D parent;

        /// <summary>
        /// Parent Node.
        /// </summary>
        public Node2D Parent {
            get {
                return parent;
            }
        }

        /// <summary>
        /// Sprite for the node.
        /// </summary>
        protected Sprite2D sprite;

        /// <summary>
        /// On enter tree, get the parent node.
        /// </summary>
        public override void _EnterTree () {
            base._EnterTree();
            parent = GetParent<Node2D>();
        }

        /// <summary>
        /// Initialize the visual.
        /// </summary>
        /// <param name="state"></param>
        public override void Initialize (State state) {
            base.Initialize(state);

            SetTexture();
        }

        /// <summary>
        /// Set the texture for the visual.
        /// </summary>
        private void SetTexture () {
            this.sprite = parent.GetNode<Sprite2D>("Sprite2D");

            ExtendedSetTexture();
        }

        /// <summary>
        /// Set custom texture information.
        /// </summary>
        protected virtual void ExtendedSetTexture () { }
    }
}
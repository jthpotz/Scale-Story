using Framework;

namespace Player {

	public partial class PlayerVisual : Visual2D {

		public override PlayerState State {
			get {
				return (PlayerState)state;
			}
		}

		public override void _Ready () {
			Initialize(
				new PlayerState(
					new PlayerData(
						new Attributes.AttributeData(), 
						new Attributes.AttributeComputer()), 
					this, 0));
		}

		protected override void SetName () {
			parent.Name = "PlayerState " + state.ID;
		}

		public void AddScale(string color) {
			State.Data.AddAttribute(color);
		}

		public bool HasScale(string color) {
			return State.Data.HasAttribute(color);
		}

	}
}

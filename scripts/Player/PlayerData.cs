using Attributes;
using Framework;

namespace Player {

    public partial class PlayerData : Data {

        public PlayerData (AttributeData data, AttributeComputer computer) : base(data, computer) { }

        public void AddAttribute(string attr) {
            baseData.AddAttribute(new Attribute(attr, 0));
        }

    }
}
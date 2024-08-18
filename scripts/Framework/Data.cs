using System.Collections.Generic;
using Attributes;

namespace Framework {

    /// <summary>
    /// Data class.
    /// </summary>
    public abstract class Data {

        /// <summary>
        /// Base data. Reference for starting point after data has combined with other data.
        /// </summary>
        protected readonly AttributeData baseData;

        /// <summary>
        /// Data after the base data has been combined with other data.
        /// </summary>
        protected AttributeData combinedData;

        /// <summary>
        /// AttributeComputer to use for computations.
        /// </summary>
        protected readonly AttributeComputer attributeComputer;

        /// <summary>
        /// Create a new data.
        /// </summary>
        /// <param name="data">Base data.</param>
        /// <param name="attributeComputer">AttributeComputer to use for computations.</param>
        public Data (AttributeData data, AttributeComputer attributeComputer) {
            this.baseData = data;
            this.combinedData = data;
            this.attributeComputer = attributeComputer;
        }

        /// <summary>
        /// Get presence of an attribute.
        /// </summary>
        /// <param name="name">Name of the attribute to look up.</param>
        /// <returns>True if the attribute exists.</returns>
        public bool HasAttribute (string name) {
            return combinedData.HasAttribute(name);
        }

        /// <summary>
        /// Get/compute an attribute.
        /// </summary>
        /// <param name="name">Name of the attribute to look up.</param>
        /// <returns>Value of the attribute.</returns>
        public float GetAttribute (string name) {
            return attributeComputer.GetOrComputeAttribute(name, combinedData);
        }

        /// <summary>
        /// Combine the base data with other data. If <paramref name="otherData" /> is null, it resets to the base data.
        /// </summary>
        /// <param name="otherData">Data to combine with the base data.</param>
        public void CombineData (ICollection<AttributeData> otherData) {
            combinedData = AttributeData.CombineAttributeData(baseData, otherData) ?? baseData;
        }
    }
}
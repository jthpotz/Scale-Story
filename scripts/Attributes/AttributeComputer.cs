using System;
using System.Collections.Generic;

namespace Attributes {

    /// <summary>
    /// Compute attributes based on other attributes.
    /// </summary>
    public class AttributeComputer : IAttributeComputer {

        /// <summary>
        /// Mapping of names of attributes to computations.
        /// </summary>
        private readonly Dictionary<string, IAttributeComputer.AttributeComputation> computations = new();

        /// <inheritdoc />
        public bool RegisterComputation (string computedAttribute, IAttributeComputer.AttributeComputation computation) {
            return computations.TryAdd(computedAttribute, computation);
        }

        /// <inheritdoc />
        public bool UnregisterComputation (string computedAttribute) {
            return computations.Remove(computedAttribute);
        }

        /// <inheritdoc />
        public bool HasComputation (string computedAttribute) {
            return computations.ContainsKey(computedAttribute);
        }

        /// <inheritdoc />
        public float ComputeAttribute (string computedAttribute, AttributeData data) {
            if (!HasComputation(computedAttribute)) throw new ArgumentException($"{computedAttribute} does not have a computation registered for it.");

            return computations[computedAttribute](this, data);
        }

        /// <inheritdoc />
        public float GetOrComputeAttribute (string computedAttribute, AttributeData data) {
            if (HasComputation(computedAttribute)) return ComputeAttribute(computedAttribute, data);

            return data.GetAttribute(computedAttribute).Value;
        }
    }
}

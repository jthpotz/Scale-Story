namespace Attributes {

    public interface IAttributeComputer {

        /// <summary>
        /// Function to calculate a specific attribute based on the provided data.
        /// </summary>
        /// <param name="attributeComputer">Attribute computer to use for further calculated attributes.</param>
        /// <param name="data">Data used to calculate the attribute.</param>
        /// <returns>Calculated attribute.</returns>
        public delegate float AttributeComputation (IAttributeComputer attributeComputer, AttributeData data);

        /// <summary>
        /// Register a computation for the given attribute. There can only be one computation for a given attribute.
        /// </summary>
        /// <param name="computedAttribute">Name of the attribute the computation is for.</param>
        /// <param name="computation">Computation for the attribute.</param>
        /// <returns>True if <paramref name="computation"/> was registered, false otherwise.</returns>
        public bool RegisterComputation (string computedAttribute, AttributeComputation computation);

        /// <summary>
        /// Unregister a computation.
        /// </summary>
        /// <param name="computedAttribute">Name of the attribute the computation should be unregistered for.</param>
        /// <returns>True if the computation for <paramref name="computedAttribute"/> was unregistered, false otherwise.</returns>
        public bool UnregisterComputation (string computedAttribute);

        /// <summary>
        /// Check if <paramref name="computedAttribute"/> has a registered computation.
        /// </summary>
        /// <param name="computedAttribute">Attribute to check for a computation.</param>
        /// <returns>True if:<list type="bullet"><item><paramref name="computedAttribute"/> has a computation.</item></list>False if:<list type="bullet"><item><paramref name="computedAttribute"/> does not have a computation.</item></list></returns>
        public bool HasComputation (string computedAttribute);

        /// <summary>
        /// Compute the <paramref name="computedAttribute"/> using the registered computation and the <paramref name="data"/>.
        /// </summary>
        /// <param name="computedAttribute">Attribute to compute.</param>
        /// <param name="data">Data to use in the computation.</param>
        /// <returns>Value for the <paramref name="computedAttribute"/>.</returns>
        /// <exception cref="ArgumentException">Thrown if there is no registered computation for <paramref name="computedAttribute"/>.</exception>
        public float ComputeAttribute (string computedAttribute, AttributeData data);

        /// <summary>
        /// If possible, compute the value for <paramref name="computedAttribute"/>, otherwise, try to get the value from the <paramref name="data"/>
        /// </summary>
        /// <param name="computedAttribute">Attribute to compute.</param>
        /// <param name="data">Data to use in the computation.</param>
        /// <returns>The first of:<list type="bullet"><item>The computed value for <paramref name="computedAttribute"/></item><item>The value of <paramref name="computedAttribute"/> in <paramref name="data"/></item><item>Float.NAN</item></list></returns>
        public float GetOrComputeAttribute (string computedAttribute, AttributeData data);
    }
}
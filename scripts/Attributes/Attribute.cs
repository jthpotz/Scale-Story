using System;

namespace Attributes {

    /// <summary>
    /// Individual attribute.
    /// </summary>
    public struct Attribute : IEquatable<Attribute> {

        /// <summary>
        /// Name of the attribute.
        /// </summary>
        public string Name;

        /// <summary>
        /// Value of the attribute.
        /// </summary>
        public float Value;

        /// <summary>
        /// Attribute constructor.
        /// </summary>
        /// <param name="name">Name of the attribute.</param>
        /// <param name="value">Value of the attribute.</param>
        public Attribute (string name, float value) {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// String representation of the attribute.
        /// </summary>
        /// <returns>"Attribute(name: value)"</returns>
        public override readonly string ToString () {
            return $"Attribute({Name}: {Value})";
        }

        /// <summary>
        /// Add two attributes with the same name.
        /// </summary>
        /// <param name="a">First attribute.</param>
        /// <param name="b">Second attribute.</param>
        /// <returns>A new attribute with the same name and the values of <paramref name="a"/> and <paramref name="b"/> added.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="a"/> and <paramref name="b"/> have different names.</exception>
        public static Attribute operator + (Attribute a, Attribute b) {
            if (b.Name != a.Name) throw new ArgumentException($"{a} has a different name than {b} so they cannot be added.");

            return new Attribute(a.Name, a.Value + b.Value);
        }

        /// <summary>
        /// Check if the attributes are equal. Equal attributes have the same name and value.
        /// </summary>
        /// <param name="a">First attribute.</param>
        /// <param name="b">Second attribute.</param>
        /// <returns>True if the attributes are equal, false if the attributes are not equal.</returns>
        public static bool operator == (Attribute a, Attribute b) {
            return a.Equals(b);
        }

        /// <summary>
        /// Check if the attributes are not equal. Equal attributes have the same name and value.
        /// </summary>
        /// <param name="a">First attribute.</param>
        /// <param name="b">Second attribute.</param>
        /// <returns>True if the attributes are not equal and false if the attributes are equal.</returns>
        public static bool operator != (Attribute a, Attribute b) {
            return !a.Equals(b);
        }

        /// <summary>
        /// Check if two attributes are equal. Equal attributes have the same name and value.
        /// </summary>
        /// <param name="other">Attribute to compare this attribute to.</param>
        /// <returns>True if the attributes are equal, false otherwise.</returns>
        public readonly bool Equals (Attribute other) {
            return other != default && this.Name == other.Name && this.Value == other.Value;
        }

        /// <summary>
        /// Check if two attribute are equal. Equal attributes have the same name and value.
        /// </summary>
        /// <param name="obj">Object to compare the attribute to.</param>
        /// <returns>True if the attributes are equal, false otherwise.</returns>
        public override readonly bool Equals (object obj) {
            return obj is Attribute attribute && Equals(attribute);
        }

        /// <summary>
        /// Get a hash code for the attribute.
        /// </summary>
        /// <returns>Hash code for the attribute.</returns>
        public override readonly int GetHashCode () {
            return (Name + Value).GetHashCode();
        }
    }
}
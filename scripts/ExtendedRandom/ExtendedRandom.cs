using Godot;

namespace ExtendedRandom {

    /// <summary>
    /// Static class that uses Unity's Random number generator to more easily create Unity data structs with random values.
    /// For example creating a random Vector2, Vector3, or Color.
    /// </summary>
    public static class ExtendedRandom {

        /// <summary>
        /// Create a new RandomNumberGenerator.
        /// </summary>
        /// <returns>A new RandomNumberGenerator that has been randomized.</returns>
        public static RandomNumberGenerator CreateRNG () {
            RandomNumberGenerator rng = new();
            rng.Randomize();
            return rng;
        }

        /// <summary>
        /// Create a new RandomNumberGenerator with the given <paramref name="seed" />.
        /// </summary>
        /// <param name="seed">Seed for the RandomNumberGenerator.</param>
        /// <returns>A new RandomNumberGenerator with the given <paramref name="seed" />.</returns>
        public static RandomNumberGenerator CreateRNG (int seed) {
            RandomNumberGenerator rng = new();
            rng.Seed = (ulong)seed;
            return rng;
        }

        /// <summary>
        /// Return a Vector2 between the min and max values.
        /// </summary>
        /// <param name="min">Minimum values (inclusive).</param>
        /// <param name="max">Maximum values (inclusive).</param>
        /// <param name="rng">RandomNumberGenerator to use. If null, one will be created.</param>
        /// <returns>A Vector2 with random values within constraints.</returns>
        public static Vector2 Vector2 (Vector2 min, Vector2 max, RandomNumberGenerator rng = null) {
            return ExtendedRandom.Vector2(min.X, min.Y, max.X, max.Y, rng ?? CreateRNG());
        }

        /// <summary>
        /// Return a Vector3 between the min and max values.
        /// </summary>
        /// <param name="min">Minimum values (inclusive).</param>
        /// <param name="max">Maximum values (inclusive).</param>
        /// <param name="rng">RandomNumberGenerator to use. If null, one will be created.</param>
        /// <returns>A Vector3 with random values within constraints.</returns>
        public static Vector3 Vector3 (Vector3 min, Vector3 max, RandomNumberGenerator rng = null) {
            return ExtendedRandom.Vector3(min.X, min.Y, min.Z, max.X, max.Y, max.Z, rng ?? CreateRNG());
        }

        /// <summary>
        /// Return a Vector4 between the min and max values.
        /// </summary>
        /// <param name="min">Minimum values (inclusive).</param>
        /// <param name="max">Maximum values (inclusive).</param>
        /// <param name="rng">RandomNumberGenerator to use. If null, one will be created.</param>
        /// <returns>A Vector4 with random values within constraints.</returns>
        public static Vector4 Vector4 (Vector4 min, Vector4 max, RandomNumberGenerator rng = null) {
            return ExtendedRandom.Vector4(min.X, min.Y, min.Z, min.W, max.X, max.Y, max.Z, max.W, rng ?? CreateRNG());
        }

        /// <summary>
        /// Return a Vector2 with values within the given ranges.
        /// </summary>
        /// <param name="minX">Minimum x value (inclusive).</param>
        /// <param name="minY">Minimum y value (inclusive).</param>
        /// <param name="maxX">Maximum x value (inclusive).</param>
        /// <param name="maxY">Maximum y value (inclusive).</param>
        /// <param name="rng">RandomNumberGenerator to use. If null, one will be created.</param>
        /// <returns>A Vector2 with random values within constraints.</returns>
        public static Vector2 Vector2 (float minX, float minY, float maxX, float maxY, RandomNumberGenerator rng = null) {
            RandomNumberGenerator randomNumberGenerator = rng ?? CreateRNG();
            float x = randomNumberGenerator.RandfRange(minX, maxX);
            float y = randomNumberGenerator.RandfRange(minY, maxY);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Return a Vector3 with values within the given ranges.
        /// </summary>
        /// <param name="minX">Minimum x value (inclusive).</param>
        /// <param name="minY">Minimum y value (inclusive).</param>
        /// <param name="minZ">Minimum z value (inclusive).</param>
        /// <param name="maxX">Maximum x value (inclusive).</param>
        /// <param name="maxY">Maximum y value (inclusive).</param>
        /// <param name="maxZ">Maximum z value (inclusive).</param>
        /// <param name="rng">RandomNumberGenerator to use. If null, one will be created.</param>
        /// <returns>A Vector3 with random values within constraints.</returns>
        public static Vector3 Vector3 (float minX, float minY, float minZ, float maxX, float maxY, float maxZ, RandomNumberGenerator rng = null) {
            RandomNumberGenerator randomNumberGenerator = rng ?? CreateRNG();
            float x = randomNumberGenerator.RandfRange(minX, maxX);
            float y = randomNumberGenerator.RandfRange(minY, maxY);
            float z = randomNumberGenerator.RandfRange(minZ, maxZ);
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Return a Vector4 with values within the given ranges.
        /// </summary>
        /// <param name="minX">Minimum x value (inclusive).</param>
        /// <param name="minY">Minimum y value (inclusive).</param>
        /// <param name="minZ">Minimum z value (inclusive).</param>
        /// <param name="minW">Minimum w value (inclusive).</param>
        /// <param name="maxX">Maximum x value (inclusive).</param>
        /// <param name="maxY">Maximum y value (inclusive).</param>
        /// <param name="maxZ">Maximum z value (inclusive).</param>
        /// <param name="maxW">Maximum w value (inclusive).</param>
        /// <param name="rng">RandomNumberGenerator to use. If null, one will be created.</param>
        /// <returns>A Vector4 with random values within constraints.</returns>
        public static Vector4 Vector4 (float minX, float minY, float minZ, float minW, float maxX, float maxY, float maxZ, float maxW, RandomNumberGenerator rng = null) {
            RandomNumberGenerator randomNumberGenerator = rng ?? CreateRNG();
            float x = randomNumberGenerator.RandfRange(minX, maxX);
            float y = randomNumberGenerator.RandfRange(minY, maxY);
            float z = randomNumberGenerator.RandfRange(minZ, maxZ);
            float w = randomNumberGenerator.RandfRange(minW, maxW);
            return new Vector4(x, y, z, w);
        }

        /// <summary>
        /// Return a random Color within the given ranges.
        /// </summary>
        /// <param name="min">Min values for the color. (inclusive)</param>
        /// <param name="max">Max values for the color. (inclusive)</param>
        /// <param name="rng">RandomNumberGenerator to use. If null, one will be created.</param>
        /// <returns>A random Color between the given values.</returns>
        public static Color Color (Color min, Color max, RandomNumberGenerator rng = null) {
            return ExtendedRandom.Color(min.R, min.G, min.B, min.A, max.R, max.G, max.B, max.A, rng ?? CreateRNG());
        }

        /// <summary>
        /// Return a random Color within the given range.
        /// </summary>
        /// <param name="minR">Min R value. (inclusive)</param>
        /// <param name="minG">Min G value. (inclusive)</param>
        /// <param name="minB">Min B value. (inclusive)</param>
        /// <param name="minA">Min A value. (inclusive)</param>
        /// <param name="maxR">Max R value. (inclusive)</param>
        /// <param name="maxG">Max G value. (inclusive)</param>
        /// <param name="maxB">Max B value. (inclusive)</param>
        /// <param name="maxA">Max A value. (inclusive)</param>
        /// <param name="rng">RandomNumberGenerator to use. If null, one will be created.</param>
        /// <returns>A random color between the given values.</returns>
        public static Color Color (
            float minR = 0f,
            float minG = 0f,
            float minB = 0f,
            float minA = 0f,
            float maxR = 1f,
            float maxG = 1f,
            float maxB = 1f,
            float maxA = 1f,
            RandomNumberGenerator rng = null) {
            RandomNumberGenerator randomNumberGenerator = rng ?? CreateRNG();
            float r = Mathf.Clamp(randomNumberGenerator.RandfRange(minR, maxR), 0, 1);
            float g = Mathf.Clamp(randomNumberGenerator.RandfRange(minG, maxG), 0, 1);
            float b = Mathf.Clamp(randomNumberGenerator.RandfRange(minB, maxB), 0, 1);
            float a = Mathf.Clamp(randomNumberGenerator.RandfRange(minA, maxA), 0, 1);
            return new Color(r, g, b, a);
        }

        /// <summary>
        /// Return a random greyscale Color (r == g == b) between the given values with an alpha of 1.
        /// </summary>
        /// <param name="min">Min value. (inclusive)</param>
        /// <param name="max">Max value. (inclusive)</param>
        /// <param name="rng">RandomNumberGenerator to use. If null, one will be created.</param>
        /// <returns>A random greyscale Color between the given values.</returns>
        public static Color GreyscaleColor (float min = 0f, float max = 1f, RandomNumberGenerator rng = null) {
            RandomNumberGenerator randomNumberGenerator = rng ?? CreateRNG();
            float greyScaleValue = Mathf.Clamp(randomNumberGenerator.RandfRange(min, max), 0, 1);
            return new Color(greyScaleValue, greyScaleValue, greyScaleValue, 1f);
        }
    }
}
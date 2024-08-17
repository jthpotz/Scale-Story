using System.Collections.Generic;
using FullTesting;
using Godot;

namespace Framework {

    /// <summary>
    /// Instantiate Godot nodes
    /// </summary>
    /// <typeparam name="T">State associated with the node.</typeparam>
    /// <typeparam name="U">Data associated with the node.</typeparam>
    /// <typeparam name="V">DataLoader used for the data.</typeparam>
    /// <typeparam name="W">Type of Node being created.</typeparam>
    public abstract partial class Factory<T, U, V, W> : Node where T : State where U : Data where V : DataLoader<U> where W : Node {

        /// <summary>
        /// Scene to instantiate.
        /// </summary>
        [Export]
        protected PackedScene objectToSpawn;

        /// <summary>
        /// Parent object for the instantiated scene.
        /// </summary>
        [Export]
        protected Node2D objectParent; // Note add new generic when fix for exporting generics is fixed

        /// <summary>
        /// Data manager.
        /// </summary>
        protected DataManager dataManager;

        /// <summary>
        /// How many objects have been instantiated.
        /// </summary>
        protected int count = 0;

        /// <summary>
        /// List of all instantiated objects.
        /// </summary>
        protected readonly List<T> objects = new();

        /// <summary>
        /// On tree enter, register a callback to initialize the factory.
        /// </summary>
        public override void _EnterTree () {
            base._EnterTree();
            DataManager.RegisterInitializedFinishedAction(InitializeFactory);
        }

        /// <summary>
        /// On tree exit, unregister the callback to initialize the factory.
        /// </summary>
        public override void _ExitTree () {
            base._ExitTree();
            DataManager.UnRegisterInitializedFinishedAction(InitializeFactory);
        }

        /// <summary>
        /// Initialize the factory.
        /// </summary>
        /// <param name="dataManager">Data manager.</param>
        private void InitializeFactory (DataManager dataManager) {
            this.dataManager = dataManager;
        }

        /// <summary>
        /// Instantiate an object.
        /// </summary>
        /// <param name="dataLoader">DataLoader to use to get the data.</param>
        /// <returns>True if the object was created, false if the object could not be created.</returns>
        public bool Create (V dataLoader) {
            if (!Guards(dataLoader)) return false;

            W node = objectToSpawn.Instantiate<W>();
            objectParent.AddChild(node);

            T state = ExtendedCreate(dataLoader, node);

            if (state == null) return false;

            objects.Add(state);
            count++;

            return true;
        }

        /// <summary>
        /// Add needed specifics to created object. 
        /// </summary>
        /// <param name="dataLoader">DataLoader to use.</param>
        /// <param name="node">Node for the instantiated scene.</param>
        /// <returns>Created state.</returns>
        protected abstract T ExtendedCreate (V dataLoader, W node);

        /// <summary>
        /// Check to insure general needed data is present.
        /// </summary>
        /// <param name="dataLoader">DataLoader to use.</param>
        /// <returns>True if needed data is present; false otherwise.</returns>
        private bool Guards (V dataLoader) {
            if (dataManager == null || dataLoader == null) return false;

            if (!dataLoader.Guards()) return false;

            return ExtendedGuards(dataLoader);
        }

        /// <summary>
        /// Check to insure needed specific needed data is present.
        /// </summary>
        /// <param name="dataLoader">DataLoader to use.</param>
        /// <returns>True if needed data is present, false otherwise.</returns>
        protected abstract bool ExtendedGuards (V dataLoader);

    }
}
using System;
using System.Collections.Generic;

namespace DesignPatterns.Composite
{
    /// <summary>
    /// Contrato base que comparten hojas y compuestos.
    /// </summary>
    public interface IComponent<T>
    {
        T Operation(T parameters);
        bool IsComposite();
    }

    /// <summary>
    /// Implementación base para una hoja del patrón composite.
    /// </summary>
    public abstract class Leaf<T> : IComponent<T>
    {
        protected Leaf(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        protected string Id { get; }

        public T Operation(T parameters)
        {
            return DoOperation(parameters);
        }

        protected abstract T DoOperation(T parameters);

        public bool IsComposite()
        {
            return false;
        }
    }

    /// <summary>
    /// Base para nodos compuestos que coordinan un conjunto de hijos.
    /// </summary>
    public abstract class Composite<T> : IComponent<T>
    {
        private readonly List<IComponent<T>> _children = new List<IComponent<T>>();

        protected Composite(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        protected string Id { get; }

        public void Add(IComponent<T> component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            _children.Add(component);
        }

        public void Remove(IComponent<T> component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            _children.Remove(component);
        }

        public IReadOnlyCollection<IComponent<T>> GetChildren()
        {
            return _children.AsReadOnly();
        }

        public T Operation(T parameters)
        {
            var results = new List<T>(_children.Count);

            foreach (var child in _children)
            {
                var result = child.Operation(parameters);
                results.Add(result);
            }

            return Aggregate(results);
        }

        protected abstract T Aggregate(IReadOnlyList<T> results);

        public bool IsComposite()
        {
            return true;
        }
    }
}

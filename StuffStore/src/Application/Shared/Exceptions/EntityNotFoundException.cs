using System;

namespace Application.Shared.Exceptions
{
    [Serializable]
    public class EntityNotFoundException<T> : EntityNotFoundException
    {
        public EntityNotFoundException(int id)
            : base($"Unable to find {typeof(T).Name} with ID {id}")
        {

        }
    }

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
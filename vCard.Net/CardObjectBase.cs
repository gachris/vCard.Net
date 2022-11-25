﻿using System;

namespace vCard.Net
{
    public class CardObjectBase : ICopyable, ILoadable
    {
        private bool _mIsLoaded;

        public CardObjectBase() => _mIsLoaded = true;

        /// <summary>
        /// Copies values from the target object to the
        /// current object.
        /// </summary>
        public virtual void CopyFrom(ICopyable c) { }

        /// <summary>
        /// Creates a copy of the object.
        /// </summary>
        /// <returns>The copy of the object.</returns>
        public virtual T Copy<T>()
        {
            var type = GetType();
            var obj = Activator.CreateInstance(type) as ICopyable;

            // Duplicate our values
            if (obj is T t)
            {
                obj.CopyFrom(this);
                return t;
            }
            return default;
        }

        public virtual bool IsLoaded => _mIsLoaded;

        public event EventHandler Loaded;

        public virtual void OnLoaded()
        {
            _mIsLoaded = true;
            Loaded?.Invoke(this, EventArgs.Empty);
        }
    }
}
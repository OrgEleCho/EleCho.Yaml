using System;
using System.Collections;
using System.Collections.Generic;

namespace EleCho.Compiler
{
    public class ParsingResult
    {
        private ISyntax[] _storage = [];
        private int _count;

        public int Capacity => _storage.Length;

        public int Count => _count;

        private void EnsureCapacity(int requiredCapacity)
        {
            if (requiredCapacity <= _storage.Length)
            {
                return;
            }

            var newCapacity = _storage.Length * 2;
            if (newCapacity == 0)
            {
                newCapacity = 4;
            }

            while (newCapacity < requiredCapacity)
            {
                newCapacity *= 2;
            }

            var newStorage = new ISyntax[newCapacity];
            Array.Copy(_storage, newStorage, _storage.Length);
            Array.Clear(_storage, 0, _storage.Length);

            _storage = newStorage;
        }

        public void Add(ISyntax syntax)
        {
            EnsureCapacity(_count + 1);

            _storage[_count++] = syntax;
        }

        public void RemoveTail()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException();
            }

            _count--;
            _storage[_count] = default!;
        }

        public void RemoveTails(int count)
        {
            if (_count - count < 0)
            {
                throw new InvalidOperationException();
            }

            while (count > 0)
            {
                _count--;
                _storage[_count] = default!;
            }
        }

        public ISyntax GetTail(int index)
        {
            if (index < 0 || 
                index > _count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return _storage[_count - 1 - index];
        }

        public ReadOnlySpan<ISyntax> GetTails(int count)
        {
            if (count == 0)
            {
                return default;
            }

            if (count < 0 ||
                count > _count)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            int startIndex = _count - count;
            return _storage.AsSpan().Slice(startIndex, count);
        }
    }
}
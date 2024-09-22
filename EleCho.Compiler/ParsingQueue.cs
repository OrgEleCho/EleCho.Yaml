using System;
using System.Collections;
using System.Collections.Generic;

namespace EleCho.Compiling
{
    public class ParsingQueue : IEnumerable<ISyntax>
    {
        private ISyntax[] _storage = [];
        private int _count;

        public int Capacity => _storage.Length;
        public int Count => _count;

        public bool EndOfFile => Count > 0 && GetTail(0) is EndOfFile;

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
                count--;
            }
        }

        public ISyntax GetTail(int index)
        {
            if (index < 0 || 
                index >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return _storage[_count - 1 - index];
        }

        public ReadOnlyMemory<ISyntax> GetTails(int count, int offset)
        {
            if (count == 0)
            {
                return default;
            }

            if (count < 0 ||
                count + offset > _count)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            int startIndex = _count - count - offset;
            return _storage.AsMemory().Slice(startIndex, count);
        }

        public ReadOnlyMemory<ISyntax> GetTails(int count)
        {
            return GetTails(count, 0);
        }

        public IEnumerator<ISyntax> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _storage[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
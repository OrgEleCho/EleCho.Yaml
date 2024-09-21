using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;

namespace EleCho.Yaml.Internals
{
    internal ref struct CharacterBuffer
    {
        private char[]? _storage;
        private int _capacity;
        private int _count;

        [MemberNotNull(nameof(_storage))]
        private void Grow()
        {
            var newCapacity = _capacity * 2;
            if (newCapacity == 0)
            {
                newCapacity = 4;
            }

            var newStorage = ArrayPool<char>.Shared.Rent(newCapacity);
            CopyTo(newStorage, 0);

            if (_storage is not null)
            {
                ArrayPool<char>.Shared.Return(_storage);
            }

            _storage = newStorage;
            _capacity = _storage.Length;
        }

        public void CopyTo(char[] array, int arrayIndex)
        {
            if (_count == 0)
            {
                return;
            }

            if (array.Length - arrayIndex < _count)
            {
                throw new ArgumentException("Array is too small", nameof(array));
            }

            Array.Copy(_storage!, 0, array, arrayIndex, _count);
        }

        public void Append(char c)
        {
            if (_count == _capacity)
            {
                Grow();
            }

            _storage![_count] = c;
            _count++;
        }

        public void Clear()
        {
            _count = 0;
        }

        public ReadOnlyMemory<char> Slice(int start, int length)
        {
            if (start + length >= _count)
            {
                throw new InvalidOperationException("Range is too large");
            }

            return new ReadOnlyMemory<char>(_storage!, start, length);
        }

        public void Dispose()
        {
            if (_storage is not null)
            {
                ArrayPool<char>.Shared.Return(_storage);
                _storage = null;
            }
        }
    }
}

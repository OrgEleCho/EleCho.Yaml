using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace EleCho.Yaml.Internals
{
    internal ref struct IndexableQueue<T>
    {
        private T[]? _storage;
        private int _capacity;
        private int _count;
        private int _head;
        private int _tail;

        public int Count => _count;

        [MemberNotNull(nameof(_storage))]
        private void Grow()
        {
            int newCapacity = _capacity * 2;
            if (newCapacity == 0)
            {
                newCapacity = 4;
            }

            T[] newStorage = ArrayPool<T>.Shared.Rent(newCapacity);
            CopyTo(newStorage, 0);

            if (_storage is not null)
            {
                ArrayPool<T>.Shared.Return(_storage, true);
            }

            _storage = newStorage;
            _capacity = _storage.Length;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (_count == 0)
            {
                return;
            }

            if (array.Length - arrayIndex < _count)
            {
                throw new ArgumentException("Array is too small", nameof(array));
            }

            if (_tail >= _head)
            {
                Array.Copy(_storage, _head, array, arrayIndex, _count);
            }
            else
            {
                var firstSegmentSize = _capacity - _head;
                Array.Copy(_storage, _head, array, arrayIndex, firstSegmentSize);
                Array.Copy(_storage, 0, array, arrayIndex + firstSegmentSize, _tail);
            }
        }

        public void Enqueue(T value)
        {
            if (_count == _capacity)
            {
                Grow();
            }

            _tail = (_tail + 1) % _capacity;
            _count++;
            _storage![_tail] = value;
        }

        public T Dequeue()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException();
            }

            var result = _storage![_head];

            _head = (_head + 1) % _capacity;
            _count--;

            return result;
        }

        public T Peek(int offset)
        {
            if (offset >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            var index = (_head + offset) % _capacity;

            return _storage![index];
        }

        public void Dispose()
        {
            if (_storage is not null)
            {
                ArrayPool<T>.Shared.Return(_storage);
                _storage = null;
            }

            _head = 0;
            _tail = 0;
            _count = 0;
            _capacity = 0;
        }
    }
}

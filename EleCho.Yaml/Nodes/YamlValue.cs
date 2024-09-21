using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace EleCho.Yaml.Nodes
{
    public sealed class YamlValue : YamlNode
    {
        private readonly ReadOnlyMemory<char> _value;
        private readonly bool _forceString;
        private readonly object? _forceValue;

        public YamlValue(ReadOnlyMemory<char> value, bool forceString, object? forceValue)
        {
            _value = value;
            _forceString = forceString;
            _forceValue = forceValue;
        }

        public object? GetValue(Type type)
        {
            if (_forceValue is not null)
            {
                if (!type.IsAssignableFrom(_forceValue.GetType()))
                {
                    throw new InvalidOperationException($"Type must be {type}");
                }

                return _forceValue;
            }

            if (type == typeof(object))
            {
                if (_value.Span.SequenceEqual("null".AsSpan()))
                {
                    return null!;
                }
                else
                {
                    return _value.ToString();
                }
            }
            else if (type == typeof(String))
            {
                return _value.ToString();
            }

            if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (_value.Span.Equals("null".AsSpan(), StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }

                type = type.GetGenericArguments()[0];
            }
            else
            {
                if (_value.Span.Equals("null".AsSpan(), StringComparison.OrdinalIgnoreCase))
                {
                    return Activator.CreateInstance(type);
                }
            }

            if (type == typeof(Boolean))
            {
                if (_value.Span.Equals("true".AsSpan(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (_value.Span.Equals("false".AsSpan(), StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else if (_value.Span.Equals("null".AsSpan(), StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            else if (type == typeof(Char))
            {
                return _value.Span[0];
            }

            #region Values

            #region Basic numbers
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            else if (type == typeof(Byte))
            {
                return Byte.Parse(_value.Span);
            }
            else if (type == typeof(UInt16))
            {
                return UInt16.Parse(_value.Span);
            }
            else if (type == typeof(Int16))
            {
                return Int16.Parse(_value.Span);
            }
            else if (type == typeof(UInt32))
            {
                return UInt32.Parse(_value.Span);
            }
            else if (type == typeof(Int32))
            {
                return Int32.Parse(_value.Span);
            }
            else if (type == typeof(UInt64))
            {
                return UInt64.Parse(_value.Span);
            }
            else if (type == typeof(Int64))
            {
                return Int64.Parse(_value.Span);
            }
            else if (type == typeof(Single))
            {
                return Single.Parse(_value.Span);
            }
            else if (type == typeof(Double))
            {
                return Double.Parse(_value.Span);
            }
            else if (type == typeof(Decimal))
            {
                return Decimal.Parse(_value.Span);
            }
            else if (type == typeof(Guid))
            {
                return Guid.Parse(_value.Span);
            }
            else if (type == typeof(DateTime))
            {
                return DateTime.Parse(_value.Span);
            }
            else if (type == typeof(DateTimeOffset))
            {
                return DateTimeOffset.Parse(_value.Span);
            }
            else if (type == typeof(TimeSpan))
            {
                return TimeSpan.Parse(_value.Span);
            }
#else
            else if (type == typeof(Byte))
            {
                return Byte.Parse(_value.ToString());
            }
            else if (type == typeof(UInt16))
            {
                return UInt16.Parse(_value.ToString());
            }
            else if (type == typeof(Int16))
            {
                return Int16.Parse(_value.ToString());
            }
            else if (type == typeof(UInt32))
            {
                return UInt32.Parse(_value.ToString());
            }
            else if (type == typeof(Int32))
            {
                return Int32.Parse(_value.ToString());
            }
            else if (type == typeof(UInt64))
            {
                return UInt64.Parse(_value.ToString());
            }
            else if (type == typeof(Int64))
            {
                return Int64.Parse(_value.ToString());
            }
            else if (type == typeof(Single))
            {
                return Single.Parse(_value.ToString());
            }
            else if (type == typeof(Double))
            {
                return Double.Parse(_value.ToString());
            }
            else if (type == typeof(Decimal))
            {
                return Decimal.Parse(_value.ToString());
            }
            else if (type == typeof(Guid))
            {
                return Guid.Parse(_value.ToString());
            }
            else if (type == typeof(DateTime))
            {
                return DateTime.Parse(_value.ToString());
            }
            else if (type == typeof(DateTimeOffset))
            {
                return DateTimeOffset.Parse(_value.ToString());
            }
            else if (type == typeof(TimeSpan))
            {
                return TimeSpan.Parse(_value.ToString());
            }
#endif
            #endregion

#if NET5_0_OR_GREATER
            else if (type == typeof(System.Half))
            {
                return System.Half.Parse(_value.Span);
            }
#endif

#if NET6_0_OR_GREATER
            else if (type == typeof(nuint))
            {
                return (nuint)nuint.Parse(_value.Span);
            }
            else if (type == typeof(nint))
            {
                return nint.Parse(_value.Span);
            }
            else if (type == typeof(TimeOnly))
            {
                return TimeOnly.Parse(_value.Span);
            }
#else
            else if (type == typeof(nuint))
            {
                return (nuint)ulong.Parse(_value.ToString());
            }
            else if (type == typeof(nint))
            {
                return (nint)long.Parse(_value.ToString());
            }
#endif

#if NET7_0_OR_GREATER
            else if (type == typeof(UInt128))
            {
                return UInt128.Parse(_value.Span);
            }
            else if (type == typeof(Int128))
            {
                return Int128.Parse(_value.Span);
            }
#endif
            #endregion

            else
            {
                throw new InvalidOperationException("Unsupported type");
            }
        }

        public T? GetValue<T>()
        {
            return (T?)GetValue(typeof(T));
        }
    }
}

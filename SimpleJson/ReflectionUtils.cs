using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace SpotifyAdsMuting.SimpleJson
{
    [GeneratedCode("reflection-utils", "1.0.0")]

    internal

        class ReflectionUtils
    {
        private static readonly object[] EmptyObjects = new object[] { };

        public delegate object GetDelegate(object source);
        public delegate void SetDelegate(object source, object value);
        public delegate object ConstructorDelegate(params object[] args);

        public delegate TValue ThreadSafeDictionaryValueFactory<TKey, TValue>(TKey key);

        public static Type GetTypeInfo(Type type)
        {
            return type;
        }

        public static Attribute GetAttribute(MemberInfo info, Type type)
        {

            if (info == null || type == null || !Attribute.IsDefined(info, type))
                return null;
            return Attribute.GetCustomAttribute(info, type);

        }

        public static Type GetGenericListElementType(Type type)
        {
            IEnumerable<Type> interfaces;

            interfaces = type.GetInterfaces();

            foreach (Type implementedInterface in interfaces)
            {
                if (IsTypeGeneric(implementedInterface) &&
                    implementedInterface.GetGenericTypeDefinition() == typeof(IList<>))
                {
                    return GetGenericTypeArguments(implementedInterface)[0];
                }
            }
            return GetGenericTypeArguments(type)[0];
        }

        public static Attribute GetAttribute(Type objectType, Type attributeType)
        {


            if (objectType == null || attributeType == null || !Attribute.IsDefined(objectType, attributeType))
                return null;
            return Attribute.GetCustomAttribute(objectType, attributeType);

        }

        public static Type[] GetGenericTypeArguments(Type type)
        {

            return type.GetGenericArguments();

        }

        public static bool IsTypeGeneric(Type type)
        {
            return GetTypeInfo(type).IsGenericType;
        }

        public static bool IsTypeGenericeCollectionInterface(Type type)
        {
            if (!IsTypeGeneric(type))
                return false;

            Type genericDefinition = type.GetGenericTypeDefinition();

            return (genericDefinition == typeof(IList<>)
                    || genericDefinition == typeof(ICollection<>)
                    || genericDefinition == typeof(IEnumerable<>)

                );
        }

        public static bool IsAssignableFrom(Type type1, Type type2)
        {
            return GetTypeInfo(type1).IsAssignableFrom(GetTypeInfo(type2));
        }

        public static bool IsTypeDictionary(Type type)
        {
#if SIMPLE_JSON_TYPEINFO
                if (typeof(IDictionary<,>).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
                    return true;
#else
            if (typeof(System.Collections.IDictionary).IsAssignableFrom(type))
                return true;
#endif
            if (!GetTypeInfo(type).IsGenericType)
                return false;

            Type genericDefinition = type.GetGenericTypeDefinition();
            return genericDefinition == typeof(IDictionary<,>);
        }

        public static bool IsNullableType(Type type)
        {
            return GetTypeInfo(type).IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static object ToNullableType(object obj, Type nullableType)
        {
            return obj == null ? null : Convert.ChangeType(obj, Nullable.GetUnderlyingType(nullableType), CultureInfo.InvariantCulture);
        }

        public static bool IsValueType(Type type)
        {
            return GetTypeInfo(type).IsValueType;
        }

        public static IEnumerable<ConstructorInfo> GetConstructors(Type type)
        {

            return type.GetConstructors();

        }

        public static ConstructorInfo GetConstructorInfo(Type type, params Type[] argsType)
        {
            IEnumerable<ConstructorInfo> constructorInfos = GetConstructors(type);
            int i;
            bool matches;
            foreach (ConstructorInfo constructorInfo in constructorInfos)
            {
                ParameterInfo[] parameters = constructorInfo.GetParameters();
                if (argsType.Length != parameters.Length)
                    continue;

                i = 0;
                matches = true;
                foreach (ParameterInfo parameterInfo in constructorInfo.GetParameters())
                {
                    if (parameterInfo.ParameterType != argsType[i])
                    {
                        matches = false;
                        break;
                    }
                }

                if (matches)
                    return constructorInfo;
            }

            return null;
        }

        public static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
#if SIMPLE_JSON_TYPEINFO
            return type.GetTypeInfo().DeclaredProperties;
#else
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
#endif
        }

        public static IEnumerable<FieldInfo> GetFields(Type type)
        {
#if SIMPLE_JSON_TYPEINFO
            return type.GetTypeInfo().DeclaredFields;
#else
            return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
#endif
        }

        public static MethodInfo GetGetterMethodInfo(PropertyInfo propertyInfo)
        {
#if SIMPLE_JSON_TYPEINFO
            return propertyInfo.GetGetMethod(true);
#else
            return propertyInfo.GetGetMethod(true);
#endif
        }

        public static MethodInfo GetSetterMethodInfo(PropertyInfo propertyInfo)
        {
#if SIMPLE_JSON_TYPEINFO
            return propertyInfo.GetSetMethod(true);
#else
            return propertyInfo.GetSetMethod(true);
#endif
        }

        public static ConstructorDelegate GetContructor(ConstructorInfo constructorInfo)
        {
#if SIMPLE_JSON_NO_LINQ_EXPRESSION
                return GetConstructorByReflection(constructorInfo);
#else
            return GetConstructorByExpression(constructorInfo);
#endif
        }

        public static ConstructorDelegate GetContructor(Type type, params Type[] argsType)
        {
#if SIMPLE_JSON_NO_LINQ_EXPRESSION
                return GetConstructorByReflection(type, argsType);
#else
            return GetConstructorByExpression(type, argsType);
#endif
        }

        public static ConstructorDelegate GetConstructorByReflection(ConstructorInfo constructorInfo)
        {
            return delegate (object[] args) { return constructorInfo.Invoke(args); };
        }

        public static ConstructorDelegate GetConstructorByReflection(Type type, params Type[] argsType)
        {
            ConstructorInfo constructorInfo = GetConstructorInfo(type, argsType);
            return constructorInfo == null ? null : GetConstructorByReflection(constructorInfo);
        }

#if !SIMPLE_JSON_NO_LINQ_EXPRESSION

        public static ConstructorDelegate GetConstructorByExpression(ConstructorInfo constructorInfo)
        {
            ParameterInfo[] paramsInfo = constructorInfo.GetParameters();
            ParameterExpression param = Expression.Parameter(typeof(object[]), "args");
            Expression[] argsExp = new Expression[paramsInfo.Length];
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type paramType = paramsInfo[i].ParameterType;
                Expression paramAccessorExp = Expression.ArrayIndex(param, index);
                Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);
                argsExp[i] = paramCastExp;
            }
            NewExpression newExp = Expression.New(constructorInfo, argsExp);
            Expression<Func<object[], object>> lambda = Expression.Lambda<Func<object[], object>>(newExp, param);
            Func<object[], object> compiledLambda = lambda.Compile();
            return delegate (object[] args) { return compiledLambda(args); };
        }

        public static ConstructorDelegate GetConstructorByExpression(Type type, params Type[] argsType)
        {
            ConstructorInfo constructorInfo = GetConstructorInfo(type, argsType);
            return constructorInfo == null ? null : GetConstructorByExpression(constructorInfo);
        }

#endif

        public static GetDelegate GetGetMethod(PropertyInfo propertyInfo)
        {
#if SIMPLE_JSON_NO_LINQ_EXPRESSION
                return GetGetMethodByReflection(propertyInfo);
#else
            return GetGetMethodByExpression(propertyInfo);
#endif
        }

        public static GetDelegate GetGetMethod(FieldInfo fieldInfo)
        {
#if SIMPLE_JSON_NO_LINQ_EXPRESSION
                return GetGetMethodByReflection(fieldInfo);
#else
            return GetGetMethodByExpression(fieldInfo);
#endif
        }

        public static GetDelegate GetGetMethodByReflection(PropertyInfo propertyInfo)
        {
            MethodInfo methodInfo = GetGetterMethodInfo(propertyInfo);
            return delegate (object source) { return methodInfo.Invoke(source, EmptyObjects); };
        }

        public static GetDelegate GetGetMethodByReflection(FieldInfo fieldInfo)
        {
            return delegate (object source) { return fieldInfo.GetValue(source); };
        }

#if !SIMPLE_JSON_NO_LINQ_EXPRESSION

        public static GetDelegate GetGetMethodByExpression(PropertyInfo propertyInfo)
        {
            MethodInfo getMethodInfo = GetGetterMethodInfo(propertyInfo);
            ParameterExpression instance = Expression.Parameter(typeof(object), "instance");
            UnaryExpression instanceCast = (!IsValueType(propertyInfo.DeclaringType)) ? Expression.TypeAs(instance, propertyInfo.DeclaringType) : Expression.Convert(instance, propertyInfo.DeclaringType);
            Func<object, object> compiled = Expression.Lambda<Func<object, object>>(Expression.TypeAs(Expression.Call(instanceCast, getMethodInfo), typeof(object)), instance).Compile();
            return delegate (object source) { return compiled(source); };
        }

        public static GetDelegate GetGetMethodByExpression(FieldInfo fieldInfo)
        {
            ParameterExpression instance = Expression.Parameter(typeof(object), "instance");
            MemberExpression member = Expression.Field(Expression.Convert(instance, fieldInfo.DeclaringType), fieldInfo);
            GetDelegate compiled = Expression.Lambda<GetDelegate>(Expression.Convert(member, typeof(object)), instance).Compile();
            return delegate (object source) { return compiled(source); };
        }

#endif

        public static SetDelegate GetSetMethod(PropertyInfo propertyInfo)
        {
#if SIMPLE_JSON_NO_LINQ_EXPRESSION
                return GetSetMethodByReflection(propertyInfo);
#else
            return GetSetMethodByExpression(propertyInfo);
#endif
        }

        public static SetDelegate GetSetMethod(FieldInfo fieldInfo)
        {
#if SIMPLE_JSON_NO_LINQ_EXPRESSION
                return GetSetMethodByReflection(fieldInfo);
#else
            return GetSetMethodByExpression(fieldInfo);
#endif
        }

        public static SetDelegate GetSetMethodByReflection(PropertyInfo propertyInfo)
        {
            MethodInfo methodInfo = GetSetterMethodInfo(propertyInfo);
            return delegate (object source, object value) { methodInfo.Invoke(source, new object[] { value }); };
        }

        public static SetDelegate GetSetMethodByReflection(FieldInfo fieldInfo)
        {
            return delegate (object source, object value) { fieldInfo.SetValue(source, value); };
        }

#if !SIMPLE_JSON_NO_LINQ_EXPRESSION

        public static SetDelegate GetSetMethodByExpression(PropertyInfo propertyInfo)
        {
            MethodInfo setMethodInfo = GetSetterMethodInfo(propertyInfo);
            ParameterExpression instance = Expression.Parameter(typeof(object), "instance");
            ParameterExpression value = Expression.Parameter(typeof(object), "value");
            UnaryExpression instanceCast = (!IsValueType(propertyInfo.DeclaringType)) ? Expression.TypeAs(instance, propertyInfo.DeclaringType) : Expression.Convert(instance, propertyInfo.DeclaringType);
            UnaryExpression valueCast = (!IsValueType(propertyInfo.PropertyType)) ? Expression.TypeAs(value, propertyInfo.PropertyType) : Expression.Convert(value, propertyInfo.PropertyType);
            MethodCallExpression callExpression = Expression.Call(instanceCast, setMethodInfo, valueCast);
            var parameterExpressions = new ParameterExpression[] { instance, value };
            Action<object, object> compiled = Expression.Lambda<Action<object, object>>(callExpression, parameterExpressions).Compile();
            return delegate (object source, object val) { compiled(source, val); };
        }

        public static SetDelegate GetSetMethodByExpression(FieldInfo fieldInfo)
        {
            ParameterExpression instance = Expression.Parameter(typeof(object), "instance");
            ParameterExpression value = Expression.Parameter(typeof(object), "value");
            Action<object, object> compiled = Expression.Lambda<Action<object, object>>(
                Assign(Expression.Field(Expression.Convert(instance, fieldInfo.DeclaringType), fieldInfo), Expression.Convert(value, fieldInfo.FieldType)), instance, value).Compile();
            return delegate (object source, object val) { compiled(source, val); };
        }

        public static BinaryExpression Assign(Expression left, Expression right)
        {
#if SIMPLE_JSON_TYPEINFO
                return Expression.Assign(left, right);
#else
            MethodInfo assign = typeof(Assigner<>).MakeGenericType(left.Type).GetMethod("Assign");
            BinaryExpression assignExpr = Expression.Add(left, right, assign);
            return assignExpr;
#endif
        }

        private static class Assigner<T>
        {
            public static T Assign(ref T left, T right)
            {
                return (left = right);
            }
        }

#endif

        public sealed class ThreadSafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>
        {
            private readonly object _lock = new object();
            private readonly ThreadSafeDictionaryValueFactory<TKey, TValue> _valueFactory;
            private Dictionary<TKey, TValue> _dictionary;

            public ThreadSafeDictionary(ThreadSafeDictionaryValueFactory<TKey, TValue> valueFactory)
            {
                _valueFactory = valueFactory;
            }

            private TValue Get(TKey key)
            {
                if (_dictionary == null)
                    return AddValue(key);
                TValue value;
                if (!_dictionary.TryGetValue(key, out value))
                    return AddValue(key);
                return value;
            }

            private TValue AddValue(TKey key)
            {
                TValue value = _valueFactory(key);
                lock (_lock)
                {
                    if (_dictionary == null)
                    {
                        _dictionary = new Dictionary<TKey, TValue>();
                        _dictionary[key] = value;
                    }
                    else
                    {
                        TValue val;
                        if (_dictionary.TryGetValue(key, out val))
                            return val;
                        Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>(_dictionary);
                        dict[key] = value;
                        _dictionary = dict;
                    }
                }
                return value;
            }

            public void Add(TKey key, TValue value)
            {
                throw new NotImplementedException();
            }

            public bool ContainsKey(TKey key)
            {
                return _dictionary.ContainsKey(key);
            }

            public ICollection<TKey> Keys => _dictionary.Keys;

            public bool Remove(TKey key)
            {
                throw new NotImplementedException();
            }

            public bool TryGetValue(TKey key, out TValue value)
            {
                value = this[key];
                return true;
            }

            public ICollection<TValue> Values => _dictionary.Values;

            public TValue this[TKey key]
            {
                get => Get(key);
                set => throw new NotImplementedException();
            }

            public void Add(KeyValuePair<TKey, TValue> item)
            {
                throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains(KeyValuePair<TKey, TValue> item)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public int Count => _dictionary.Count;

            public bool IsReadOnly => throw new NotImplementedException();

            public bool Remove(KeyValuePair<TKey, TValue> item)
            {
                throw new NotImplementedException();
            }

            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            {
                return _dictionary.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return _dictionary.GetEnumerator();
            }
        }

    }
}
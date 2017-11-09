using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace RiverSharp.Mvvm
{
    /// <summary>
    /// 对象的基类, 其属性必须是可观察的。
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged接口实现

        /// <summary>
        /// 在属性值更改后发生。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 提供对派生类的 <see cref="PropertyChanged" /> 事件处理程序的访问。
        /// </summary>
        protected PropertyChangedEventHandler PropertyChangedHandler { get { return PropertyChanged; } }

        #endregion

        /// <summary>
        /// 验证此 ViewModel 中是否存在名称是 <paramref name="propertyName" /> 的属性。
        /// </summary>
        /// <remarks>
        /// 此方法可以在使用属性之前调用, 例如在调用 RaisePropertyChanged 之前。
        /// 它避免了当属性名称被改变，一些地方被遗漏修改的错误。
        /// 此方法仅在DEBUG模式下有效。
        /// </remarks>
        /// <param name="propertyName">要检查的属性的名称。</param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            TypeInfo info = GetType().GetTypeInfo();

            if (!string.IsNullOrEmpty(propertyName) && info.GetDeclaredProperty(propertyName) == null)
            {
                var found = false;

                while (info.BaseType != typeof(Object))
                {
                    info = info.BaseType.GetTypeInfo();

                    if (info.GetDeclaredProperty(propertyName) != null)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    throw new ArgumentException("Property not found", propertyName);
                }
            }
        }

        /// <summary>
        /// 如果需要引发 <see cref="PropertyChanged"/> 事件。
        /// </summary>
        /// <remarks>如果属性参数与当前类上的现有属性不对应, 则只在DEBUG模式中引发异常。</remarks>
        /// <param name="propertyName">（可选）已更改的属性的名称。</param>
        public virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            VerifyPropertyName(propertyName);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 如果需要引发 <see cref="PropertyChanged"/> 事件。
        /// </summary>
        /// <typeparam name="T">所更改的属性的类型。</typeparam>
        /// <param name="propertyExpression">标识更改的属性的表达式。</param>
        public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                var propertyName = GetPropertyName(propertyExpression);

                if (!string.IsNullOrEmpty(propertyName))
                {
                    RaisePropertyChanged(propertyName);
                }
            }
        }

        /// <summary>
        /// 从表达式中提取属性的名称。
        /// </summary>
        /// <typeparam name="T">属性的类型。</typeparam>
        /// <param name="propertyExpression">一个返回属性名称的表达式。</param>
        /// <returns>表达式返回的属性的名称。</returns>
        /// <exception cref="ArgumentNullException">如果表达式为空。</exception>
        /// <exception cref="ArgumentException">如果表达式不代表一个属性。</exception>
        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var body = propertyExpression.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException("Invalid argument", "propertyExpression");
            }

            var property = body.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException("Argument is not a property", "propertyExpression");
            }

            return property.Name;
        }

        /// <summary>
        /// 为属性分配新值。然后, 在需要时引发 <see cref="PropertyChanged"/> 事件。
        /// </summary>
        /// <typeparam name="T">所更改的属性的类型。</typeparam>
        /// <param name="propertyExpression">标识更改的属性的表达式。</param>
        /// <param name="field">存储该属性值的字段。</param>
        /// <param name="newValue">发生更改后的属性值。</param>
        /// <returns>如果  <see cref="PropertyChanged"/>  事件已引发, 则为 True, 否则为 false。如果旧值等于新值, 则不引发该事件。</returns>
        protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }
            field = newValue;
            RaisePropertyChanged(propertyExpression);
            return true;
        }

        /// <summary>
        /// 为属性分配新值。然后, 在需要时引发  <see cref="PropertyChanged"/>  事件。
        /// </summary>
        /// <typeparam name="T">所更改的属性的类型。</typeparam>
        /// <param name="propertyName">更改的属性的名称。</param>
        /// <param name="field">存储该属性值的字段。</param>
        /// <param name="newValue">发生更改后的属性值。</param>
        /// <returns>如果  <see cref="PropertyChanged"/>  事件已引发, 则为 True, 否则为 false。如果旧值等于新值, 则不引发该事件。</returns>
        protected bool Set<T>(string propertyName,ref T field,T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// 为属性分配新值。然后, 在需要时引发  <see cref="PropertyChanged"/>  事件。
        /// </summary>
        /// <typeparam name="T">所更改的属性的类型。</typeparam>
        /// <param name="field">存储该属性值的字段。</param>
        /// <param name="newValue">发生更改后的属性值。</param>
        /// <param name="propertyName">（可选）已更改的属性的名称。</param>
        /// <returns>如果  <see cref="PropertyChanged"/>  事件已引发, 则为 True, 否则为 false。如果旧值等于新值, 则不引发该事件。</returns>
        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            return Set(propertyName, ref field, newValue);
        }
    }
}

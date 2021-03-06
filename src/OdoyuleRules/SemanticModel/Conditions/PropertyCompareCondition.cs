// Copyright 2011 Chris Patterson
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace OdoyuleRules.SemanticModel
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using OdoyuleRules.RuntimeModel.Comparators;
    using OdoyuleRules.RuntimeModel.Values;


    public class PropertyCompareCondition<T, TProperty> :
        PropertyCondition<T, TProperty>,
        RuleCondition<T>,
        IEquatable<PropertyCompareCondition<T, TProperty>>
        where T : class
    {
        readonly Comparator<TProperty, TProperty> _comparator;
        readonly Value<TProperty> _value;

        public PropertyCompareCondition(PropertyInfo propertyInfo,
                                        Expression<Func<T, TProperty>> propertyExpression,
                                        Comparator<TProperty, TProperty> comparator,
                                        Value<TProperty> value)
            : base(propertyInfo, propertyExpression)
        {
            _comparator = comparator;
            _value = value;
        }

        public bool Equals(PropertyCompareCondition<T, TProperty> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other._comparator, _comparator) && Equals(other._value, _value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as PropertyCompareCondition<T, TProperty>);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = base.GetHashCode();
                result = (result*397) ^ (_comparator != null ? _comparator.GetHashCode() : 0);
                result = (result*397) ^ (_value != null ? _value.GetHashCode() : 0);
                return result;
            }
        }

        public bool Accept(SemanticVisitor visitor)
        {
            return visitor.Visit(this, x => true);
        }

        public static bool operator ==(
            PropertyCompareCondition<T, TProperty> left, PropertyCompareCondition<T, TProperty> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(
            PropertyCompareCondition<T, TProperty> left, PropertyCompareCondition<T, TProperty> right)
        {
            return !Equals(left, right);
        }
    }
}
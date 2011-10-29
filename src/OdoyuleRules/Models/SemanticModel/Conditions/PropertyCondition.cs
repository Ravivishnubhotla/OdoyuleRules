﻿// Copyright 2011 Chris Patterson
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
namespace OdoyuleRules.Models.SemanticModel
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public abstract class PropertyCondition<T, TProperty> :
        IEquatable<PropertyCondition<T, TProperty>>
        where T : class
    {
        readonly Expression<Func<T, TProperty>> _propertyExpression;
        readonly PropertyInfo _propertyInfo;

        protected PropertyCondition(PropertyInfo propertyInfo, Expression<Func<T, TProperty>> propertyExpression)
        {
            _propertyInfo = propertyInfo;
            _propertyExpression = propertyExpression;
        }

        public Expression<Func<T, TProperty>> PropertyExpression
        {
            get { return _propertyExpression; }
        }

        public PropertyInfo PropertyInfo
        {
            get { return _propertyInfo; }
        }

        public bool Equals(PropertyCondition<T, TProperty> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._propertyInfo, _propertyInfo);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (PropertyCondition<T, TProperty>)) return false;
            return Equals((PropertyCondition<T, TProperty>) obj);
        }

        public override int GetHashCode()
        {
            return (_propertyInfo != null ? _propertyInfo.GetHashCode() : 0);
        }

        public static bool operator ==(PropertyCondition<T, TProperty> left, PropertyCondition<T, TProperty> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PropertyCondition<T, TProperty> left, PropertyCondition<T, TProperty> right)
        {
            return !Equals(left, right);
        }
    }
}
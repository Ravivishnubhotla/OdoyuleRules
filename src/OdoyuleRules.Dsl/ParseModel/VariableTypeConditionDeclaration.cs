// Copyright 2011-2012 Chris Patterson
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
// with the License. You may obtain a copy of the License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
// License for the specific language governing permissions and limitations under the License.
namespace OdoyuleRules.Dsl.ParseModel
{
    public class VariableTypeConditionDeclaration :
        ConditionDeclaration
    {
        public VariableTypeConditionDeclaration(VariableDeclaration variable, TypeConditionDeclaration typeCondition)
            : base(DeclarationType.VariableTypeCondition)
        {
            Variable = variable;
            TypeCondition = typeCondition;
        }

        public VariableDeclaration Variable { get; private set; }
        public TypeConditionDeclaration TypeCondition { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Variable, TypeCondition);
        }
    }
}
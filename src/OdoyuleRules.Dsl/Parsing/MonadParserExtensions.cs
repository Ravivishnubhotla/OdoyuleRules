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
namespace OdoyuleRules.Dsl.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public static class MonadParserExtensions
    {
        public static Parser<TInput, TValue> Where<TInput, TValue>(this Parser<TInput, TValue> parser,
                                                                   Func<TValue, bool> pred)
        {
            return input =>
                {
                    Result<TValue> result = parser(input);
                    if (result == null || !pred(result.Value))
                        return null;

                    return result;
                };
        }

        public static Parser<TInput, TSelect> Select<TInput, TValue, TSelect>(this Parser<TInput, TValue> parser,
                                                                              Func<TValue, TSelect> selector)
        {
            return input =>
                {
                    Result<TValue> result = parser(input);
                    if (result == null)
                        return null;

                    return new Success<TSelect>(selector(result.Value), result.Rest);
                };
        }

        public static Parser<string, TSelect> SelectMany<TValue, TIntermediate, TSelect>(
            this Parser<string, TValue> parser,
            Func<TValue, Parser<string, TIntermediate>> selector,
            Func<TValue, TIntermediate, TSelect> projector)
        {
            return input =>
                {
                    Result<TValue> result = parser(input);
                    if (result == null)
                        return null;

                    TValue val = result.Value;
                    Result<TIntermediate> nextResult = selector(val)(result.Rest);
                    if (nextResult == null)
                        return null;

                    return new Success<TSelect>(projector(val, nextResult.Value), nextResult.Rest);
                };
        }

        public static Parser<string, TValue> Or<TValue>(this Parser<string, TValue> first,
                                                                Parser<string, TValue> second)
        {
            return input => first(input) ?? second(input);
        }

        public static Parser<TInput, TValue> FirstMatch<TInput, TValue>(this IEnumerable<Parser<TInput, TValue>> options)
        {
            return input =>
                {
                    return options
                        .Select(option => option(input))
                        .Where(result => result != null)
                        .FirstOrDefault();
                };
        }

        public static Parser<string, TSecondValue> And<TFirstValue, TSecondValue>(
            this Parser<string, TFirstValue> first,
            Parser<string, TSecondValue> second)
        {
            return input =>
                {
                    var firstResult = first(input);
                    if (firstResult == null)
                        return null;

                    return second(firstResult.Rest);
                };
        }
    }
}
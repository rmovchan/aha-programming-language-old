﻿//
// Package: Aha! Base Library 
// Author: Roman Movchan
// Created: 2014-04-01

using System;
using System.Collections.Generic;
using AhaCore;
using Collections;

namespace BaseLibrary
{
//doc
//    Title:   "Collections"
//    Purpose: "Generic collections: dynamic arrays, stacks, queues"
//    Package: "Aha! Base Library"
//    Author:  "Roman Movchan"
//    Created: "2010-10-14"
//end

//type Item: arbitrary "collection item"

//export Types:
//    type DynamicArray:
//        obj [Item]
//            add(Item) "add new item"
//            replace([ at: integer item: Item ]) "replace item at index"
//            exchange([ first: integer second: integer ]) "swap two items"
//            move([ from: integer to: integer ]) "move item to new position"
//            insert([ at: integer item: Item ]) "insert item at index"
//            delete(integer) "delete item at index"
//        end "a dynamic array"

//    type DynamicSequence:
//        obj Item
//            push(Item) "add an item"
//            pop "remove an item"
//        end "a dynamic sequence"

//export Constructors:
//    the DynamicArray: DynamicArray "zero-length dynamic array"
//    the Stack: DynamicSequence "empty stack"
//    the Queue: DynamicSequence "empty queue"
//    the Storage: RandomStorage "empty random storage"
//end
    public class Collections<Item> : AhaModule
    {
        public IDynamicArray<Item> DynamicArray() { return new Collections.DynamicArray<Item>(); }
        public IDynamicSequence<Item> Stack() { return new Collections.Stack<Item>(); }
        public IDynamicSequence<Item> Queue() { return new Collections.Queue<Item>(); }
    }

//doc
//    Title:   "Rationals"
//    Package: "Aha! Base Library"
//    Purpose: "Rational numbers"
//    Author:  "Roman Movchan, Melbourne, Australia"
//    Created: "2012-06-02"
//end

//export Types:
//    type Rational: opaque "a rational number"
//    type RatioStruc:
//        [
//            num: integer "numerator"
//            den: integer "denominator"
//        ] "rational as composite"
//end

//export Operators:
//    (integer / integer): { integer, integer -> Rational } "divide integers to get Rational"
//    (~struc Rational): { Rational -> RatioStruc } "convert Rational to RatioStruc"
//    (Rational + Rational): { Rational, Rational -> Rational } "sum of two rationals"
//    (Rational - Rational): { Rational, Rational -> Rational } "difference between two rationals"
//    (Rational * Rational): { Rational, Rational -> Rational } "product of two rationals"
//    (Rational / Rational): { Rational, Rational -> Rational } "quotient of two rationals"
//    (Rational < Rational): { Rational, Rational } "is first rational less than second?"
//    (Rational <= Rational): { Rational, Rational } "is first rational less than or equal to second?"
//    (Rational = Rational): { Rational, Rational } "is first rational equal to second?"
//    (Rational /= Rational): { Rational, Rational } "is first rational not equal to second?"
//    (Rational >= Rational): { Rational, Rational } "is first rational greater than or equal to second?"
//    (Rational > Rational): { Rational, Rational } "is first rational greater than second?"
//end
    public class Rational
    {
        public Int64 num;
        public Int64 den;
    }

    public interface IRatioStruc
    {
        Int64 num();
        Int64 den();
    }

    public class Rationals : AhaModule
    {
        class RatioStruc : IRatioStruc
        {
            private Int64 fnum;
            private Int64 fden;
            public RatioStruc(Int64 n, Int64 d) { fnum = n; fden = d; }
            public Int64 num() { return fnum; }
            public Int64 den() { return fden; }
        }

        public Rational Ratio(Int64 num, Int64 den) { return new Rational { num = num, den = den }; }
        public IRatioStruc Struc(Rational x) { return new RatioStruc(x.num, x.den); }
        public Rational RationalSum(Rational a, Rational b) { return new Rational { num = a.num * b.den + a.den * b.num, den = a.den * b.den }; }
        public bool RationalLess(Rational a, Rational b) { return a.num * b.den < a.den * b.num; }
    }
//doc 
//    Title:   "Math"
//    Purpose: "Floating-point numbers and matrices"
//    Package: "Aha! Base Library"
//    Author:  "Roman Movchan"
//    Created: "2010-10-16"
//end

//use Rational: Base/Rational
//import Rational(Types)

//export Types:
//    type Float: opaque "a floating-point number"
//    type FormatParams:
//        [
//            general:
//                [
//                    period: character "character for decimal period"
//                ] "general format" |
//            fixed:
//                [
//                    period: character "character for decimal period"
//                    decimals: integer "number of decimals"
//                ] "fixed format" |
//            exponent: 
//                [
//                    period: character "character for decimal period"
//                ] "exponential format"
//        ] "number formatting parameters"
//end

//export Operators:
//    (Float + Float): { Float, Float -> Float } "the sum of two floats"
//    (Float - Float): { Float, Float -> Float } "the difference between two floats"
//    (Float * Float): { Float, Float -> Float } "the product of two floats"
//    (Float / Float): { Float, Float -> Float } "the ratio of two floats"
//    (Float < Float): { Float, Float } "is first float less than second?"
//    (Float <= Float): { Float, Float } "is first float less than or equal to second?"
//    (Float = Float): { Float, Float } "is first float equal to second?"
//    (Float /= Float): { Float, Float } "is first float not equal to second?"
//    (Float >= Float): { Float, Float } "is first float greater than or equal to second?"
//    (Float > Float): { Float, Float } "is first float greater than second?"
//    (~float integer): { integer -> Float } "convert integer to Float"
//    (~float Rational): { Rational -> Float } "convert Rational to Float"
//end

//export Functions:
//    the sin: { Float -> Float } "the sine function"
//    the cos: { Float -> Float } "the cosine function"
//    the exp: { Float -> Float } "the exponent function"
//    the log: { Float -> Float } "the logarithm function"
//    the tan: { Float -> Float } "the tangent function"
//    the Pi: Float "the pi number"
//    the Infinity: Float "+infinity"
//    the NegInfinity: Float "-infinity"
//    the Trunc: { Float -> integer } "truncate Float to integer"
//    the Round: { Float, integer -> Rational } "round Float to given number of decimals after decimal point"
//    the FloatToString: { Float, FormatParams -> [character] } "convert Float to string"
//    the StringToFloat: { [character], FormatParams -> Float } "convert string to Float"
//end

//export MatrixAlgebra:
//    type Scalar: Float "alias for floating-point numbers"
//    type Matrix: opaque "a matrix"
//    the Size: { Matrix -> [ rows: integer columns: integer ] } "matrix dimensions"
//    (Matrix @ [ row: integer col: integer ]): { Matrix, [ row: integer col: integer ] -> Scalar } "matrix element with given coordinates"
//    (~id integer): { integer -> Matrix } "identity matrix of given size"
//    (~rows [[integer]]): { [[integer]] -> Matrix } "build matrix from rows of integers (must be of same size)"
//    (~columns [[integer]]): { [[integer]] -> Matrix } "build matrix from columns of integers (must be of same size)"
//    (~rows [[Rational]]): { [[Rational]] -> Matrix } "build matrix from rows of rationals (must be of same size)"
//    (~columns [[Rational]]): { [[Rational]] -> Matrix } "build matrix from columns of rationals (must be of same size)"
//    (~rows [[Scalar]]): { [[Scalar]] -> Matrix } "build matrix from rows of scalars (must be of same size)"
//    (~columns [[Scalar]]): { [[Scalar]] -> Matrix } "build matrix from columns of scalars (must be of same size)"
//    (Scalar * Matrix): { Scalar, Matrix -> Matrix } "multiply matrix by scalar"
//    (Matrix + Matrix): { Matrix, Matrix -> Matrix } "sum of matrices"
//    (Matrix - Matrix): { Matrix, Matrix -> Matrix } "difference of matrices"
//    (Matrix * Matrix): { Matrix, Matrix -> Matrix } "product of matrices"
//    the Det: { Matrix -> Scalar } "determinant"
//    (~inv Matrix): { Matrix -> Matrix } "inverse matrix"
//    (~tr Matrix): { Matrix -> Matrix } "transpose matrix"
//end
    public class Float
    {
        public double value;
    }

    public interface IGeneralFormatParams
    {
        char period();
    }

    public interface IFixedFormatParams
    {
        char period();
        Int64 decimals();
    }

    public interface IExponentFormatParams
    {
        char period();
    }

    public interface FormatParams
    {
        IGeneralFormatParams general();
        IFixedFormatParams fixed_();
        IExponentFormatParams exponent();
    }

    public class Matrix
    {
        public double[,] value;
    }

    public class Math : AhaModule
    {
        public Float FloatFromInt(Int64 x) { return new Float { value = x }; }
        public Float FloatFromRational(Rational x) { return new Float { value = x.num / x.den }; }
        public Float FloatSum(Float a, Float b) { return new Float { value = a.value + b.value }; }
        public bool FloatLess(Float a, Float b) { return a.value < b.value; }
        public Float sin(Float a) { return new Float { value = System.Math.Sin(a.value) }; }
    }

//doc
//    Title:   "StrUtils"
//    Package: "Aha! Base Library"
//    Purpose: "String utilities"
//    Author:  "Roman Movchan, Melbourne, Australia"
//    Created: "2012-06-03"
//end

//export Types:
//    type String: [character] "character string"
//    type Substring:
//        [
//            index: integer "first character index"
//            length: integer "substring length"
//        ] "identifies a substring inside a string"
//    type RegEx: opaque "a regular expression"
//    type Pattern: 
//        [
//            string: String "exact search string" |
//            regEx: RegEx "regular expression" |
//            equality: { character, character } "character-wise equality relation"
//        ] "search pattern"
//    type SearchParams:
//        [
//            for: Pattern "search pattern"
//             in: String "where to search"
//        ] "search parameters"
//    type CharCompare: { character, character -> integer } "character comparison function: negative - <, zero - =, positive - >"
//    type StringCompare: { String, String -> integer } "string comparison function: negative - <, zero - =, positive - >"
//end

//export Utils:
//    the Substr: { String, Substring -> String } "extract substring from string"
//    the RegEx: { String -> RegEx } "construct regular expression from string"
//    the Search: { SearchParams -> Substring* } "return all occurrences of search pattern in string as a sequence"    
//    the StringBuilder:
//        obj String
//            add(character) "add a single character"
//            put([ at: integer char: character ]) "replace character at given index"
//            append(String) "append string to the end"
//            replace([ substr: [Substring] with: String ]) "simultaneously replace multiple non-overlapping substrings with string"
//            padLeft([ with: character to: integer ]) "pad with given character from left to given length"
//            padRight([ with: character to: integer ]) "pad with given character from right to given length"
//            extract(Substring) "extract substring"
//            trimSpaces "trim spaces"
//            apply({ character -> character }) "convert all characters using provided function"
//        end "the string builder"
//    the StringHashFunc: { String -> integer } "standard hash function for strings"
//    the StringCompare: { CharCompare -> StringCompare } "string comparison function from character comparison function"
//end

//export Operators:
//    (String = String): { String, String } "compare strings"
//end
    
    public interface ISubstring
    {
        Int64 index();
        Int64 length();
    }

    public class RegEx
    {
        public string value;
    }

    public delegate bool Equality(char a, char b);

    public interface IPattern
    {
        IahaArray<char> string_();
        RegEx regEx();
        Equality equality();
    }

    public interface ISearchParams
    {
        IPattern for_();
        IahaArray<char> in_();
    }

    public interface IStringBuilder : IahaObject<IahaArray<char>>
    {
        void add(char ch);
        void append(IahaArray<char> str);
        void extract(ISubstring sub);
        void trimSpaces();
    }

    public class StrUtils : AhaModule
    {
        class SearchSeq : IahaSequence<ISubstring>
        {
            struct substring : ISubstring
            {
                public int idx;
                private int len;
                public Int64 index() { return idx; }
                public Int64 length() { return len; }
                public substring(int i, int l) { idx = i; len = l; }
            }

            private string str;
            private string sub;
            private int index;
            public SearchSeq(string s, string ss) { str = s; sub = ss; index = s.IndexOf(ss); }
            public ISubstring state() { return new substring(index, sub.Length); }
            public IahaObject<ISubstring> copy() { return new SearchSeq(str, sub) { index = index }; }
            public void skip() { index = str.IndexOf(sub, index + 1); if (index == -1) throw Failure.One; }
            public ISubstring first(Predicate<ISubstring> that, Int64 max) { Int64 j = 0; substring substr = new substring(index, sub.Length); while (index != -1) { if (j == max) break; substr.idx = index; if (that(substr)) return substr; index = str.IndexOf(sub, index + 1); j++; } throw Failure.One; }
        }

        class stringBuilder : IStringBuilder
        {
            private System.Text.StringBuilder sb;
            public stringBuilder(string init) { sb = new System.Text.StringBuilder(); sb.Append(init); }
            public IahaArray<char> state() { return new AhaString(sb.ToString()); }
            public IahaObject<IahaArray<char>> copy() { return new stringBuilder(sb.ToString()); }
            public void add(char ch) { sb.Append(ch); }
            public void append(IahaArray<char> str) { sb.Append(str); }
            public void extract(ISubstring sub) { sb = new System.Text.StringBuilder(sb.ToString().Substring((int)sub.index(), (int)sub.length())); }
            public void trimSpaces() { sb = new System.Text.StringBuilder(sb.ToString().Trim()); }
        }

        class fastBuilder : IStringBuilder
        {
            const int block = 1024;
            private List<char[]> list;
            private int count = 0;
            private char[] gather()
            {
                char[] buf = new char[count];
                int j = 0;
                for (int i = 0; i < list.Count - 1; i++) { Array.Copy(list[i], 0, buf, j, block); j += block; }
                if (j != count) Array.Copy(list[list.Count - 1], 0, buf, j, count - j);
                return buf;
            }
            public fastBuilder() { list = new List<char[]>(); }
            public IahaArray<char> state() { return new AhaString(gather()); }
            public IahaObject<IahaArray<char>> copy() { fastBuilder fb = new fastBuilder() { count = count }; for (int i = 0; i < count; i++) { char[] b = new char[block]; list[i].CopyTo(b, 0); fb.list.Add(b); } return fb; }
            public void add(char ch) { if (count == list.Count * block) list.Add(new char[block]); list[count / block][count % block] = ch; }
            public void append(IahaArray<char> str) 
            { 
                char[] temp = str.get();
                int j = list.Count * block - count; //positions in last block
                if (j > temp.Length) j = temp.Length; //characters to copy into last block
                if (j != 0) Array.Copy(temp, 0, list[list.Count - 1], count % block, j);
                char[] b;
                while (j + block < temp.Length)
                {
                    b = new char[block];
                    Array.Copy(temp, j, b, 0, block);
                    list.Add(b); 
                    j += block; // j = number of chars copied
                }
                if (j != temp.Length)
                {
                    b = new char[block];
                    Array.Copy(temp, j, b, 0, temp.Length - j); //copy remaining chars
                    list.Add(b);
                }
            }
            public void extract(ISubstring sub)
            {
                list.RemoveRange(0, (int)(sub.index() / block)); //remove unused blocks
                int j = (int)(sub.index() % block); //shift by
                int l = (int)(sub.length() / block); //number of full blocks
                if (j != 0)
                {
                    for (int i = 0; i < l - 1; i++) //shift characters in full blocks
                    {
                        Array.Copy(list[i], j, list[i], 0, block - j); //shift end of block to beginning
                        Array.Copy(list[i + 1], 0, list[i], block - j, j); //fill the rest of block from next one
                    }
                    Array.Copy(list[l - 1], j, list[l - 1], 0, block - j);
                }
                int k = (int)(sub.length() % block); //chars in last block
                if (k != 0)
                {
                    Array.Copy(list[l + 1], 0, list[l], block - j, k); //fill the rest of block from next one
                    list.RemoveRange(l + 1, list.Count - l - 1); //remove unused blocks
                }
                else list.RemoveRange(l, list.Count - l); //remove unused blocks
            }
            public void trimSpaces() { sb = new System.Text.StringBuilder(sb.ToString().Trim()); }
        }

        public IahaArray<char> Substr(IahaArray<char> s, ISubstring ss) { char[] items = new char[ss.length()]; Array.Copy(s.get(), ss.index(), items, 0, ss.length()); return new AhaString(items); }
        public RegEx RegEx(IahaArray<char> s) { return new RegEx { value = new string(s.get()) }; }
        public IahaSequence<ISubstring> Search(ISearchParams param)
        {
            IahaArray<char> sub = param.for_().string_();
            string temp1 = new string(sub.get());
            string temp2 = new string(param.in_().get());
            return new SearchSeq(temp2, temp1);
        }
        public IStringBuilder StringBuilder() { return new stringBuilder(""); }
        public Int64 StringHashFunc(IahaArray<char> s) { return s.get().GetHashCode(); }
    }

}

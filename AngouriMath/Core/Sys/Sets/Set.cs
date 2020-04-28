﻿
/* Copyright (c) 2019-2020 Angourisoft
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software
 * is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
 * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AngouriMath.Core.Sys.Sets
{
    /// <summary>
    /// Class defines true mathematical sets
    /// It can be empty, it can contain numbers, it can contain intervals etc.
    /// It supports intersection (with & operator), union (with | operator), subtracting (with - operator)
    /// TODO: To make sets work faster
    /// </summary>
    public class Set : IEnumerable
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public SetEnumerator GetEnumerator()
            => new SetEnumerator(pieces.ToArray());

        private readonly List<Piece> pieces = new List<Piece>();

        internal void AddPiece(Piece piece)
        {
            throw new NotImplementedException();
        }

        public static Set operator &(Set A, Set B)
        {
            throw new NotImplementedException();
        }

        public static Set operator |(Set A, Set B)
        {
            throw new NotImplementedException();
        }

        public static Set operator -(Set A, Set B)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Set set)
        {
            foreach (var p in set)
                if (!this.Contains(p))
                    return false;
            return true;
        }

        public bool Contains(Piece piece)
            => pieces.Any(p => p.Contains(piece));

        public Set()
        {
            
        }

        /// <summary>
        /// Adds a set of numbers to the set
        /// </summary>
        /// <param name="elements"></param>
        public void AddElements(params Entity[] elements)
        {
            foreach(var el in elements)
                AddPiece(Piece.Element(el));
        }


        /// <summary>
        /// Creates a closed interval
        /// Modify it with setters, for example,
        /// myset.AddInterval(3, 4).SetLeftClosed(true).SetRightClosed(false);
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public IntervalPiece AddInterval(Number a, Number b)
        {
            var piece = Piece.Interval(a, b);
            AddPiece(piece);
            return piece; // so we could modify it later
        }
    }

    public class SetEnumerator : IEnumerator
    {
        private Piece[] pieces;
        private int position = -1;

        public SetEnumerator(Piece[] pieces)
            => this.pieces = pieces;

        public bool MoveNext()
        {
            position++;
            return position < pieces.Length;
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose()
        {
            // do nothing if disposed
        }

        object IEnumerator.Current
        {
            get => Current;
        }

        public Piece Current
        {
            get => pieces[position];
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Engine.Logic.Mapping.Tiling
{
    internal class TileCollection : IDictionary<(int row, int col), Tile>
    {
        public Tile this[(int row, int col) key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICollection<(int row, int col)> Keys => throw new NotImplementedException();

        public ICollection<Tile> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add((int row, int col) key, Tile value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<(int row, int col), Tile> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<(int row, int col), Tile> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey((int row, int col) key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<(int row, int col), Tile>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<(int row, int col), Tile>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove((int row, int col) key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<(int row, int col), Tile> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue((int row, int col) key, [MaybeNullWhen(false)] out Tile value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

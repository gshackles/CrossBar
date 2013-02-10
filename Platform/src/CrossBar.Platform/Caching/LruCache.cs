//
// A simple LRU cache, based on the one found in MonoTouch.Dialog
// TODO: add time-based expirations
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 

using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossBar.Platform.Caching
{
    public class LruCache<TKey, TValue> where TValue : class
    {
        private readonly Dictionary<TKey, LinkedListNode<TValue>> _dict;
        private readonly Dictionary<LinkedListNode<TValue>, TKey> _revdict;
        private readonly LinkedList<TValue> _list;
        private readonly int _entryLimit;
        private readonly int _sizeLimit;
        private int _currentSize;
        private readonly Func<TValue, int> _slotSizeFunc;

        public LruCache(int entryLimit)
            : this(entryLimit, 0, null)
        {
        }

        public LruCache(int entryLimit, int sizeLimit, Func<TValue, int> slotSizer)
        {
            _list = new LinkedList<TValue>();
            _dict = new Dictionary<TKey, LinkedListNode<TValue>>();
            _revdict = new Dictionary<LinkedListNode<TValue>, TKey>();

            if (sizeLimit != 0 && slotSizer == null)
                throw new ArgumentNullException("If sizeLimit is set, the slotSizer must be provided");

            _entryLimit = entryLimit;
            _sizeLimit = sizeLimit;
            _slotSizeFunc = slotSizer;
        }

        private void evict()
        {
            var last = _list.Last;
            var key = _revdict[last];

            if (_sizeLimit > 0)
            {
                int size = _slotSizeFunc(last.Value);
                _currentSize -= size;
            }

            _dict.Remove(key);
            _revdict.Remove(last);
            _list.RemoveLast();

            var disposable = last.Value as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }

        public void Purge()
        {
            foreach (var element in _list.OfType<IDisposable>())
                element.Dispose();

            _dict.Clear();
            _revdict.Clear();
            _list.Clear();
            _currentSize = 0;
        }

        public TValue this[TKey key]
        {
            get
            {
                LinkedListNode<TValue> node;

                if (_dict.TryGetValue(key, out node))
                {
                    _list.Remove(node);
                    _list.AddFirst(node);

                    return node.Value;
                }
                return null;
            }

            set
            {
                LinkedListNode<TValue> node;
                int size = _sizeLimit > 0 ? _slotSizeFunc(value) : 0;

                if (_dict.TryGetValue(key, out node))
                {
                    if (_sizeLimit > 0 && node.Value != null)
                    {
                        int repSize = _slotSizeFunc(node.Value);
                        _currentSize -= repSize;
                        _currentSize += size;
                    }

                    // If we already have a key, move it to the front
                    _list.Remove(node);
                    _list.AddFirst(node);

                    // Remove the old value
                    if (node.Value != null)
                    {
                        var disposable = node.Value as IDisposable;
                        if (disposable != null)
                            disposable.Dispose();
                    }

                    node.Value = value;
                    while (_sizeLimit > 0 && _currentSize > _sizeLimit && _list.Count > 1)
                        evict();
                    return;
                }
                if (_sizeLimit > 0)
                {
                    while (_sizeLimit > 0 && _currentSize + size > _sizeLimit && _list.Count > 0)
                        evict();
                }
                if (_dict.Count >= _entryLimit)
                    evict();
                // Adding new node
                node = new LinkedListNode<TValue>(value);
                _list.AddFirst(node);
                _dict[key] = node;
                _revdict[node] = key;
                _currentSize += size;
            }
        }

        public override string ToString()
        {
            return "LRUCache dict={0} revdict={1} list={2}";
        }
    }
}
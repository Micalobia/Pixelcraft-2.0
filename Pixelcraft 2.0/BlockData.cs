using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixelcraft_2
{
    internal class BlockData
    {
        public byte id { get; set; }
        public byte data { get; set; }
        public string name { get; set; }
        public string fileName { get; set; }
        public BlockData(byte id, byte data, string name, string fileName)
        {
            this.id = id;
            this.data = data;
            this.name = name;
            this.fileName = fileName;
        }

        public BlockData(byte id, Enum e, string name, string fileName)
        {
            this.id = id;
            data = Convert.ToByte(e);
            this.name = name;
            this.fileName = fileName;
        }

        public override string ToString()
        {
            return string.Format("Item:{0}, Damage:{1}", name, data);
        }
    }

    internal class BlockEntity
    {
        private BlockData _block;
        private bool _enabled;

        public BlockEntity(BlockData block)
        {
            _block = block;
            _enabled = true;
        }

        public BlockEntity(BlockData block, bool enabled)
        {
            _block = block;
            _enabled = enabled;
        }

        public bool Enabled { get => _enabled; set => _enabled = value; }
        public byte ID { get => _block.id; set => _block.id = value; }
        public byte Data { get => _block.data; set => _block.data = value; }
        public string Name { get => _block.name; set => _block.name = value; }
        public string FileName { get => _block.fileName; set => _block.fileName = value; }
        public BlockData Block { get => _block; set => _block = value; }

        public override string ToString()
        {
            return string.Format("{0}, Enabled:{1}", base.ToString(), _enabled);
        }
    }

    internal class BlockDataCollection
    {
        private Dictionary<string, List<BlockEntity>> _collections;
        private bool _changed = false;

        public BlockDataCollection()
        {
            _collections = new Dictionary<string, List<BlockEntity>>();
        }

        public void Add(BlockData block)
        {
            if (!_collections.ContainsKey("Misc"))
            {
                _collections.Add("Misc", new List<BlockEntity>());
            }
            _collections["Misc"].Add(new BlockEntity(block));
        }

        public void Enable(string category, int index)
        {
            _collections[category][index].Enabled = true;
        }

        public void Enable(string category, string blockname)
        {
            _collections[category].FirstOrDefault(x => x.Name == blockname).Enabled = true;
        }

        public void Enable(string category)
        {
            foreach (BlockEntity b in _collections[category])
            {
                b.Enabled = true;
            }
        }

        public void Disable(string category, int index)
        {
            _collections[category][index].Enabled = false;
        }

        public void Disable(string category, string blockname)
        {
            _collections[category].FirstOrDefault(x => x.Name == blockname).Enabled = false;
        }

        public void Disable(string category)
        {
            foreach (BlockEntity b in _collections[category])
            {
                b.Enabled = false;
            }
        }

        public void Add(BlockData block, string category)
        {
            if (!_collections.ContainsKey(category))
            {
                _collections.Add(category, new List<BlockEntity>());
            }
            _collections[category].Add(new BlockEntity(block));
        }


        public BlockData this[string category, int index] => _collections[category][index].Block;

        public BlockData this[string category, string blockname] => _collections[category].FirstOrDefault(x => x.Name == blockname).Block;

        public BlockData this[string category, byte id] => _collections[category].FirstOrDefault(x => x.ID == id).Block;

        public List<BlockData> this[string category] => _collections[category].Extract();

        public List<BlockData> All
        {
            get
            {
                List<BlockData> list = new List<BlockData>();
                foreach (KeyValuePair<string, List<BlockEntity>> k in _collections)
                {
                    list.AddRange(k.Value.Extract());
                }
                return list;
            }
        }

        public List<BlockEntity> AllFlagged
        {
            get
            {
                List<BlockEntity> list = new List<BlockEntity>();
                foreach (KeyValuePair<string, List<BlockEntity>> k in _collections)
                {
                    list.AddRange(k.Value);
                }
                return list;
            }
        }

        public List<BlockData> Enabled
        {
            get
            {
                List<BlockData> list = new List<BlockData>();
                foreach (KeyValuePair<string, List<BlockEntity>> k in _collections)
                {
                    for (int i = 0; i < k.Value.Count; ++i)
                    {
                        if (k.Value[i].Enabled) list.Add(k.Value[i].Block);
                    }
                }
                return list;
            }
        }

        public List<BlockData> Disabled
        {
            get
            {
                List<BlockData> list = new List<BlockData>();
                foreach (KeyValuePair<string, List<BlockEntity>> k in _collections)
                {
                    for (int i = 0; i < k.Value.Count; ++i)
                    {
                        if (!k.Value[i].Enabled) list.Add(k.Value[i].Block);
                    }
                }
                return list;
            }
        }

        public bool Changed { get => _changed; private set => _changed = value; }
        internal Dictionary<string, List<BlockEntity>> Collections { get => _collections; set => _collections = value; }
    }
}

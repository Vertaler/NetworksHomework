using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworksHomework.Algorithms.CompressionAlgorithms
{
    public class LZW : ICompressionAlgorithm
    {
        public string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@!";

        public Dictionary<string, byte?> _codeTable;


        public void _Init()
        {
            _codeTable = new Dictionary<string, byte?>();

            for (int i = 0; i < alphabet.Length; i++)
            {
                _codeTable.Add(alphabet[i].ToString(), (byte) i);
            }
        }

        public string DecodeByte(byte? b)
        {
            return _codeTable.First(x => x.Value == b).Key;
        }

        public byte[] Compress(byte[] message)
        {
            _Init();
            var code = new List<byte>();
            var input = "";

            var messageString = Encoding.ASCII.GetString(message);
            foreach (var chr in messageString)
            {
                if (_codeTable.ContainsKey(input + chr))
                {
                    input += chr;
                }
                else
                {
                    code.Add((byte) _codeTable[input]);
                    _codeTable.Add(input + chr, (byte) (_codeTable.Count + 1));
                    input = chr.ToString();
                }
            }
            if (input != "")
            {
                code.Add((byte) _codeTable[input]);
            }
            return code.ToArray();
        }

        public byte[] Decompress(byte[] message)
        {
            _Init();
            var outString = new StringBuilder();
            byte? lcode = null;
            foreach (var code in message)
            {
                if (_codeTable.ContainsValue(code))
                {
                    outString.Append(DecodeByte(code));
                    if (lcode != null)
                    {
                        _codeTable.Add(DecodeByte(lcode) + DecodeByte(code)[0], (byte) (_codeTable.Count + 1));
                    }
                    lcode = code;
                }
                else
                {
                    var chain = DecodeByte(lcode) + DecodeByte(lcode)[0];
                    outString.Append(chain);
                    _codeTable.Add(chain, (byte) (_codeTable.Count + 1));
                    lcode = code;
                }
            }
            return Encoding.ASCII.GetBytes(outString.ToString());
        }
    }
}
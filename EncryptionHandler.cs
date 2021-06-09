using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Password_Manager
{
    /// <summary>
    /// C-Sharp implimentation of my PDEA Encryption algorithm
    /// </summary>

    class EncryptionHandler
    {
        public byte block_size_bytes = 72;
        static ulong rand_seed;

        struct key_struct
        {
            public List<byte[]> bitKey128;
            public int[] permutationAmount;
            public List<byte[]> bitKeyGates;
        }

        int[,] matrices = new int[32, 4]{
            {8 ,2,3,1},{12,2,3,-1},{9 ,2,-3,1},{13,2,-3,-1},{10,-2,3,1},{14,-2,3,-1},{11,-2,-3,1},{15,-2,-3,-1},
            {0 ,3,1,2},{2 ,3,1,-2},{4 ,3,-1,2},{6 ,3,-1,-2},{1 ,-3,1,2},{3 ,-3,1,-2},{5 ,-3,-1,2},{7 ,-3,-1,-2},
            {16,1,3,2},{18,1,3,-2},{17,1,-3,2},{19,1,-3,-2},{20,-1,3,2},{22,-1,3,-2},{21,-1,-3,2},{23,-1,-3,-2},
            {24,2,1,3},{25,2,1,-3},{28,2,-1,3},{29,2,-1,-3},{26,-2,1,3},{27,-2,1,-3},{30,-2,-1,3},{31,-2,-1,-3}
        };

        byte[] substitutionTable = new byte[]
        {
            28,210,143,56,96,252,36,25,125,64,16,10,187,179,97,232,
            211,15,164,231,234,220,100,208,93,147,159,81,173,185,83,78,
            41,98,57,22,79,114,201,193,178,88,105,249,171,223,119,236,
            157,23,66,29,168,137,6,117,113,150,199,87,31,162,216,160,
            198,238,227,115,200,14,18,194,163,226,75,54,186,76,110,224,
            8,205,2,254,80,248,42,27,139,72,218,104,33,149,212,242,
            235,175,133,176,161,207,82,191,55,77,158,156,246,124,3,217,
            197,190,180,177,44,170,189,65,247,35,144,46,132,13,99,12,
            182,32,251,37,153,135,148,108,60,61,151,38,39,112,213,106,
            122,62,68,134,118,183,17,202,67,206,243,253,181,84,85,47,
            53,69,95,215,229,233,244,204,109,221,51,172,50,48,165,24,
            146,237,188,43,141,136,89,255,101,4,102,222,1,174,228,92,
            127,116,126,155,239,145,138,166,250,0,111,142,52,240,214,196,
            219,245,71,130,90,241,120,21,192,20,225,63,203,49,5,45,
            19,70,9,209,230,58,128,11,140,86,74,73,195,167,103,131,
            152,107,34,94,26,30,184,7,123,91,129,40,169,154,121,59
        };

        byte[] inverseSubstitutionTable = new byte[]
        {
            201,188,82,110,185,222,54,247,80,226,11,231,127,125,69,17,
            10,150,70,224,217,215,35,49,175,7,244,87,0,51,245,60,
            129,92,242,121,6,131,139,140,251,32,86,179,116,223,123,159,
            173,221,172,170,204,160,75,104,3,34,229,255,136,137,145,219,
            9,119,50,152,146,161,225,210,89,235,234,74,77,105,31,36,
            84,27,102,30,157,158,233,59,41,182,212,249,191,24,243,162,
            4,14,33,126,22,184,186,238,91,42,143,241,135,168,78,202,
            141,56,37,67,193,55,148,46,214,254,144,248,109,8,194,192,
            230,250,211,239,124,98,147,133,181,53,198,88,232,180,203,2,
            122,197,176,25,134,93,57,138,240,132,253,195,107,48,106,26,
            63,100,61,72,18,174,199,237,52,252,117,44,171,28,189,97,
            99,115,40,13,114,156,128,149,246,29,76,12,178,118,113,103,
            216,39,71,236,207,112,64,58,68,38,151,220,167,81,153,101,
            23,227,1,16,94,142,206,163,62,111,90,208,21,169,187,45,
            79,218,73,66,190,164,228,19,15,165,20,96,47,177,65,196,
            205,213,95,154,166,209,108,120,85,43,200,130,5,155,83,183
        };

        ulong nextRand()
        {
            ulong z = (rand_seed += 0x9e3779b97f4a7c15);
            z = (z ^ (z >> 30)) * 0xbf58476d1ce4e5b9;
            z = (z ^ (z >> 27)) * 0x94d049bb133111eb;
            return z ^ (z >> 31);
        }

        byte[] GenerateIV()
        {
            rand_seed = (ulong)DateTime.UnixEpoch.Ticks;
            byte[] IV = new byte[16];
            for (int i = 0; i < 16; i++)
                IV[i] = (byte)(nextRand() % 256);
            return IV;
        }

        byte[] CalcEncodedIV(byte[] IV, string password)
        {
            var sha512 = SHA512.Create();
            byte[] HashedIV = sha512.ComputeHash(IV);
            XOR(HashedIV, sha512.ComputeHash(Encoding.ASCII.GetBytes(password)), 64, 0);
            Array.Copy(HashedIV, IV, 16);
            return IV;
        }

        void XOR(byte[] arr1, byte[] arr2, int mod, int amount)
        {
            for (int i = 0; i < arr1.Length; i++)
                arr1[i] ^= arr2[Modulo(i + amount, mod)];
        }

        int Modulo(int val, int m)
        {
            int mod = val % m;
            if (mod < 0)
                mod += m;
            return mod;
        }

        key_struct GetGates(byte[] a)
        {
            key_struct keys = new key_struct();

            keys.bitKey128 = new List<byte[]>();
            keys.permutationAmount = new int[4];
            keys.bitKeyGates = new List<byte[]>();

            for (int i = 0; i < 4; i++)
            {
                keys.bitKey128.Add(new byte[16]);
                Array.Copy(a, (i * 16), keys.bitKey128[i], 0, 16);
                keys.permutationAmount[i] = keys.bitKey128[i][15];
                keys.bitKeyGates.Add(GetGateIndex(keys.bitKey128[i]));
            }
            return keys;
        }

        byte[] GetGateIndex(byte[] a)
        {
            string binaryData = "";
            List<byte> returnArray = new List<byte>();
            for (int i = 0; i < a.Length; i++)
                binaryData += Convert.ToString(a[i], 2).PadLeft(8, '0');
            for (int i = 0; i < (binaryData.Length / 5) - 1; i++)
                returnArray.Add(Convert.ToByte(binaryData.Substring(i * 5, 5), 2));
            return returnArray.ToArray();
        }

        byte[] GenerateLongHash(List<byte[]> inputKeys)
        {
            byte[] outputArray = new byte[256];
            var sha512 = SHA512.Create();
            for (int i = 0; i < 4; i++)
            {
                Array.Copy(sha512.ComputeHash(inputKeys[i]), 0, outputArray, (i * 64), 64);
            }

            return outputArray;
        }

        byte[] substitutionData = new byte[72];
        byte[] Substitution(byte[] dataBytes)
        {
            for (int i = 0; i < block_size_bytes; i++)
            {
                substitutionData[i] = substitutionTable[dataBytes[i]];
            }
            return substitutionData;
        }
        byte[] InverseSubstitution(byte[] dataBytes)
        {
            for (int i = 0; i < block_size_bytes; i++)
            {
                substitutionData[i] = inverseSubstitutionTable[dataBytes[i]];
            }
            return substitutionData;
        }


        byte[] MatrixManipulation(byte[] gates, byte[] dataBytes)
        {
            List<byte[]> dataBytesSeparated = new List<byte[]>();
            List<byte[]> newDataBytesSeparated = new List<byte[]>();

            for (int i = 0; i < 24; i++)
            {
                dataBytesSeparated.Add(new byte[] { dataBytes[(i * 3) + 0], dataBytes[(i * 3) + 1], dataBytes[(i * 3) + 2] });
                newDataBytesSeparated.Add(new byte[] { 0, 0, 0 });
                int[] matrix = new int[] { matrices[gates[i], 0], matrices[gates[i], 1], matrices[gates[i], 2], matrices[gates[i], 3] };
                for (int j = 0; j < 3; j++)
                {
                    newDataBytesSeparated[i][Math.Abs(matrix[j + 1]) - 1] = (byte)(dataBytesSeparated[i][j] * (-1 + (matrix[j + 1] >= 0 ? 1 : 0) * 2));
                }
            }

            byte[] returnBytes = new byte[72];
            for (int i = 0; i < 24; i++)
            {
                returnBytes[(i * 3) + 0] = newDataBytesSeparated[i][0];
                returnBytes[(i * 3) + 1] = newDataBytesSeparated[i][1];
                returnBytes[(i * 3) + 2] = newDataBytesSeparated[i][2];
            }

            return returnBytes;
        }

        byte[] InverseMatrixManipulation(byte[] gates, byte[] dataBytes)
        {
            List<byte[]> dataBytesSeparated = new List<byte[]>();
            List<byte[]> newDataBytesSeparated = new List<byte[]>();

            for (int i = 0; i < 24; i++)
            {
                dataBytesSeparated.Add(new byte[] { dataBytes[(i * 3) + 0], dataBytes[(i * 3) + 1], dataBytes[(i * 3) + 2] });
                newDataBytesSeparated.Add(new byte[] { 0, 0, 0 });
                int[] matrix = new int[] { matrices[matrices[gates[i], 0], 0], matrices[matrices[gates[i], 0], 1], matrices[matrices[gates[i], 0], 2], matrices[matrices[gates[i], 0], 3] };
                for (int j = 0; j < 3; j++)
                {
                    newDataBytesSeparated[i][Math.Abs(matrix[j + 1]) - 1] = (byte)(dataBytesSeparated[i][j] * (-1 + (matrix[j + 1] >= 0 ? 1 : 0) * 2));
                }
            }

            byte[] returnBytes = new byte[72];
            for (int i = 0; i < 24; i++)
            {
                returnBytes[(i * 3) + 0] = newDataBytesSeparated[i][0];
                returnBytes[(i * 3) + 1] = newDataBytesSeparated[i][1];
                returnBytes[(i * 3) + 2] = newDataBytesSeparated[i][2];
            }

            return returnBytes;
        }

        byte[] RotateLeft(byte[] data, int amount)
        {
            byte[] returnBytes = new byte[block_size_bytes];
            int byteshift = amount / 8;
            int bitshift = amount % 8;
            for (int i = 0; i < block_size_bytes; i++)
            {
                byte byte1 = data[(i + byteshift) % block_size_bytes];
                byte byte2 = data[(i + byteshift + 1) % block_size_bytes];
                byte shift1 = (byte)(byte1 << bitshift);
                byte shift2 = (byte)(byte2 >> (8 - bitshift));
                returnBytes[i] = (byte)(shift1 | shift2);
            }
            return returnBytes;
        }

        byte[] RotateRight(byte[] data, int amount)
        {
            byte[] returnBytes = new byte[block_size_bytes];
            int byteshift = amount / 8;
            int bitshift = amount % 8;
            for (int i = 0; i < block_size_bytes; i++)
            {
                byte byte1 = data[(block_size_bytes + (i - byteshift)) % block_size_bytes];
                byte byte2 = data[(block_size_bytes + (i - byteshift - 1)) % block_size_bytes];
                byte shift1 = (byte)(byte1 >> bitshift);
                byte shift2 = (byte)(byte2 << (8 - bitshift));
                returnBytes[i] = (byte)(shift1 | shift2);
            }
            return returnBytes;
        }

        void Encrypt(byte[] data, byte[] lastData, byte[] sentence, byte[] lastSentence, byte[] IV, byte securityLevel, int blockAmount, key_struct keys, byte[] lHash)
        {
            Array.Copy(sentence, data, block_size_bytes);
            if (blockAmount == 0)
            {
                XOR(sentence, IV, 16, 0);
            }
            else
            {
                XOR(lastData, lastSentence, block_size_bytes, blockAmount);
                XOR(sentence, lastData, block_size_bytes, blockAmount);
            }
            //Block Cypher part
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < Math.Floor(Math.Max(1 << (securityLevel - 1), keys.permutationAmount[i] % (1 << securityLevel)) / Math.Max(1, (float)(securityLevel >> 1))); j++)
                {
                    Array.Copy(Substitution(sentence), sentence, block_size_bytes);
                    Array.Copy(MatrixManipulation(keys.bitKeyGates[i], sentence), sentence, block_size_bytes);
                    Array.Copy(RotateLeft(sentence, (2 * i) + 2 + lHash[blockAmount % 256]), sentence, block_size_bytes);
                }
            }

            Array.Copy(sentence, lastSentence, block_size_bytes);
            Array.Copy(data, lastData, block_size_bytes);
        }

        void Decrypt(byte[] data, byte[] lastData, byte[] sentence, byte[] lastSentence, byte[] IV, byte securityLevel, int blockAmount, key_struct keys, byte[] lHash)
        {
            Array.Copy(sentence, data, block_size_bytes);
            //Block Cypher part
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 0; j < Math.Floor(Math.Max(1 << (securityLevel - 1), keys.permutationAmount[i] % (1 << securityLevel)) / Math.Max(1, (float)(securityLevel >> 1))); j++)
                {
                    Array.Copy(RotateRight(sentence, (2 * i) + 2 + lHash[blockAmount % 256]), sentence, block_size_bytes);
                    Array.Copy(InverseMatrixManipulation(keys.bitKeyGates[i], sentence), sentence, block_size_bytes);
                    Array.Copy(InverseSubstitution(sentence), sentence, block_size_bytes);
                }
            }

            if (blockAmount == 0)
            {
                XOR(sentence, IV, 16, 0);
            }
            else
            {
                XOR(lastData, lastSentence, block_size_bytes, blockAmount);
                XOR(sentence, lastData, block_size_bytes, blockAmount);
            }

            Array.Copy(data, lastSentence, block_size_bytes);
            Array.Copy(sentence, lastData, block_size_bytes);
        }

        public byte[] mainLoop(bool encrypting, string password, byte security_level, byte[] byte_array)
        {
            var sha512 = SHA512.Create();

            //TODO - FIX extra data amoun issue

            int blockAmount = -1;
            int bytesRead = 0;
            int totalBlocks = encrypting ? (int)Math.Ceiling((byte_array.Length + 1) / (float)block_size_bytes) : ((byte_array.Length - 80) / block_size_bytes);
            byte extraData = encrypting ? (byte)(block_size_bytes - (byte_array.Length % block_size_bytes)) : 0;
            int bufferSize = Math.Min(65536, totalBlocks) * block_size_bytes;
            byte[] buffer = new byte[bufferSize];
            byte[] sentence = new byte[block_size_bytes];
            byte[] outputArray = new byte[totalBlocks * block_size_bytes + (encrypting ? 80 : 0)];

            key_struct keys;

            byte[] IV = GenerateIV();
            byte[] CheckHash = new byte[64];

            if (encrypting)
                Array.Copy(IV, 0, outputArray, 0, 16);
            else
            {
                Array.Copy(byte_array, 0, IV, 0, 16);
                Array.Copy(byte_array, 16, CheckHash, 0, 64);
            }

            IV = CalcEncodedIV(IV, password);

            keys = GetGates(sha512.ComputeHash(Encoding.ASCII.GetBytes(password)));
            byte[] lHash = GenerateLongHash(keys.bitKey128);

            bytesRead = 0;
            byte[] data = new byte[block_size_bytes];
            byte[] lastSentence = new byte[block_size_bytes];
            byte[] lastData = new byte[block_size_bytes];
            Array.Copy(byte_array, (encrypting ? 0 : 80), buffer, 0, Math.Min(bufferSize, byte_array.Length));

            while (blockAmount < (totalBlocks - 1))
            {

                if (blockAmount == -1 && encrypting)
                {
                    data[0] = extraData;
                    Array.Copy(buffer, 0, data, 1, 71);
                    Array.Copy(data, sentence, block_size_bytes);
                    var holder = new ArraySegment<byte>(data, 1, block_size_bytes - 1);
                    Array.Copy(sha512.ComputeHash(holder.ToArray()), 0, outputArray, 16, 64);
                    bytesRead += 71;
                }
                else
                {
                    Array.Copy(buffer, bytesRead, sentence, 0, block_size_bytes);
                    if ((bytesRead + block_size_bytes) - byte_array.Length < block_size_bytes && (bytesRead + block_size_bytes) - byte_array.Length > 0)
                    {
                        int num = (bytesRead + block_size_bytes) - byte_array.Length;
                        for (int i = block_size_bytes - num; i < block_size_bytes; i++)
                        {
                            sentence[i] = extraData;
                        }
                    }
                    bytesRead += block_size_bytes;
                }

                blockAmount++;

                if (encrypting)
                {
                    //TODO impliment
                    Encrypt(data, lastData, sentence, lastSentence, IV, security_level, blockAmount, keys, lHash);
                }
                else
                {
                    //TODO impliment
                    Decrypt(data, lastData, sentence, lastSentence, IV, security_level, blockAmount, keys, lHash);
                }

                //END
                if (!encrypting && blockAmount == 0)
                {
                    extraData = sentence[0];
                    //Check if CheckHash matches
                    //if (std::memcmp(CheckHash, sw::sha512::calculate(std::string(sentence + 1, sentence + block_size_bytes)).c_str(), 64) != 0)
                    //{
                    //    std::cout << "Incorrect Details" << std::endl;
                    //    return NULL;
                    //}

                    if (!encrypting && blockAmount == totalBlocks - 1)
                    {
                        Array.Copy(sentence, 1, outputArray, (block_size_bytes * blockAmount), Math.Min(block_size_bytes, block_size_bytes - (totalBlocks == 1 ? extraData : extraData - 1)));
                    }
                    else
                    {
                        Array.Copy(sentence, 1, outputArray, (block_size_bytes * blockAmount), 71);
                    }
                }
                else if (!encrypting && blockAmount == totalBlocks - 1)
                {
                    Array.Copy(sentence, 0, outputArray, (block_size_bytes * blockAmount) - 1, Math.Min(block_size_bytes, block_size_bytes - (extraData - 1)));
                }
                else
                {
                    Array.Copy(sentence, 0, outputArray, (encrypting ? 80 : -1) + (block_size_bytes * blockAmount), block_size_bytes);
                }
            }

            return outputArray;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.FiveM
{
    public static class Five
    {
        public static readonly string[] KnownPackets =
        {
            "msgArrayUpdate",
            "msgCloneAcks",
            "msgCloneRemove",
            "msgConVars",
            "msgConfirm",
            "msgEnd",
            "msgEntityCreate",
            "msgFrame",
            "msgHeHost",
            "msgIHost",
            "msgIQuit",
            "msgNetEvent",
            "msgNetGameEvent",
            "msgObjectIds",
            "msgPackedAcks",
            "msgPackedClones",
            "msgPaymentRequest",
            "msgReassembledEvent",
            "msgRequestObjectIds",
            "msgResStart",
            "msgResStop",
            "msgRoute",
            "msgRpcEntityCreation",
            "msgRpcNative",
            "msgServerCommand",
            "msgServerEvent",
            "msgStateBag",
            "msgTimeSync",
            "msgTimeSyncReq",
            "msgWorldGrid",
            "msgWorldGrid3",

            // manual list that doesn't start with 'msg'
            "gameStateAck",
            "gameStateNAck",
		};

        public static readonly Dictionary<string, uint> MapPacketHash = new ();
        public static readonly Dictionary<uint, string> MapHashPacket = new ();

        static Five()
        {
            foreach (var packet in KnownPackets)
            {
                uint hash = HashRageString(packet);
                MapPacketHash[packet] = hash;
                MapHashPacket[hash] = packet;
            }
        }

        public static uint HashString(string str)
        {
            return HashRageString(str.ToLowerInvariant());
        }

        public static uint HashRageString(string str)
        {
            uint hash = 0;

            foreach (var c in str)
            {
                hash += c;
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }

            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);

            return hash;
        }

    }
}

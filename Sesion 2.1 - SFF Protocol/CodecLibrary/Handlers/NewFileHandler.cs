<<<<<<< HEAD
﻿using CodecLibrary.Messages;
using CodecLibrary.StateMachine;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary.Handlers
{
    public class NewFileHandler : IPacketHandler
    {
        private Receiver _receiver;

        public NewFileHandler(Receiver receiver)
        {
            _receiver = receiver;
        }

        public void Handle(Packet packet)
        {
            // Deserializar el paquete NewFile
            var newFilePacket = packet as NewFile;

            if (newFilePacket == null)
            {
                Console.WriteLine("Error: Paquete recibido no es un NewFile.");
                return;
            }

            // Mostrar el nombre del archivo recibido
            Console.WriteLine($"Nuevo archivo recibido: {newFilePacket.FileName}");
            var ackNewFilePacket = new AckNewFile(0, new byte[0]); // El ACK puede no necesitar datos adicionales
            _receiver.Send(ackNewFilePacket);
            Console.WriteLine($"ACK enviado para el archivo: {newFilePacket.FileName}");

            
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Text;
using CodecLibrary.Messages;
using CodecLibrary.States;
using CodecLibrary.Networking;


namespace CodecLibrary.Handlers
{
    public class NewFileHandler : IPacketHandler
    {
        private Receiver _receiver;

        public NewFileHandler(Receiver receiver)
        {
            _receiver = receiver;
        }

        public void Handle(Packet packet)
        {
            if (packet is NewFile newFile)
            {
                Console.WriteLine($"📂 Solicitud de transferencia recibida para: {newFile.FileName}");
                _receiver.FileName = newFile.FileName;
                Console.WriteLine("✅ Enviando AckNewFile...");
                _receiver.SendPacket(new AckNewFile(0, new byte[0]));
                _receiver.ChangeState(new ReceivingFileState(_receiver));
            }
        }
    }

}
>>>>>>> 4fe09b8ee82d800eacbfb813cb6fc9571734db42

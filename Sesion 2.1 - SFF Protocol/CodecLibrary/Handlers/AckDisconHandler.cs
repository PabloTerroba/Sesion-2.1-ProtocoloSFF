using CodecLibrary;
using CodecLibrary.Handlers;
using CodecLibrary.Messages;
using CodecLibrary.StateMachine;
using System;

public class AckDisconHandler : IPacketHandler
{
    private Sender _sender;
    private SendingFileState _state;

    public AckDisconHandler(Sender sender, SendingFileState state)
    {
        _sender = sender;
        _state = state;
    }

    public void Handle(Packet packet)
    {
        if (packet is AckDiscon)
        {
            Console.WriteLine("ACK de Discon recibido. Finalizando conexión.");
            _state.AcknowledgeDiscon();
        }
        else
        {
            Console.WriteLine("Paquete inesperado recibido en espera de AckDiscon.");
        }
    }
}
